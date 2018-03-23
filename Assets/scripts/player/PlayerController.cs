using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Work log
//Initial File - Jack Phoebus
//Cleanup for demo - Zak Olyarnik
//Camera/player rotation - Matt and Eric
//Aim - Eric Lee

//[RequireComponent(typeof(CharacterController))]
public abstract class PlayerController : MonoBehaviour {
	public Rigidbody rb;
	public CharacterController cc;

	[Space(10)]
	public Transform carryPos;
	public Camera cam;
	public GameObject black;
	public bool canMove=true, aiming = false;
	public float respawnTimer = 3.0f;
	public GameObject model;
	public Collider col;

	[Space(10)]
	public AudioClip jumpSound;
	public AudioClip dieSound;
	public AudioSource sourcePlayerController;
	public Axes.Action horizontal, vertical, carry, jump, interact, aim;

	[Space(10)]
	public PlayerAnimator animator;
	//TODO: alpha: push animation
	
	public float
		speed=1,
		aimingSpeed=0.5f,
		jumpSpeed=6,//9.3692f,//jumpSpeed = sqrt(2*gravity*jumpHeight)
		secondJumpSpeed,//=6,
		gravity=9.81f;//36;

	//used for accumulating gravity and for jumping

	[Space(10)]
	public int jumps=1;
	protected int jumpsLeft;
	protected bool jumpBtnDown=false;

	[Space(10)]
	public float actionAnimTime;
	public float
		carryAnimTime,
		interactAnimTime;

	[Space(10)]
	public GameObject carryCollider, interactCollider, reticle;

	[Space(10)]
	public GameObject carryObj;
	private bool dead = false;
	void Start() {
	}

	public bool isGrounded() {
		if(cc) {
			return cc.isGrounded;
		}else {
			float groundPos=col.bounds.extents.y;

			RaycastHit hit;
			float r=new Vector3(col.bounds.extents.x,0,col.bounds.extents.z).magnitude;
		
			//return Physics.SphereCast(transform.position,r,Vector3.down,out hit,groundPos);
			return Physics.SphereCast(transform.position+groundPos*Vector3.down,r,Vector3.down,out hit,0);
		}
	}

	public Vector3
		playerMove=Vector3.zero,
		netForce=Vector3.zero,//velocity added for one frame only
		constVelocity=Vector3.zero;//constant velocity added every frame, ex for accumulating gravity/jumping
	public Vector3 velocity {
		get {
			if (rb) {
				return rb.velocity;
			}else if(cc) {
				return cc.velocity;
			}else {
				return playerMove+constVelocity+netForce*Time.deltaTime;
			}
		}

	}

	public void AddVelocity(Vector3 v) {
		constVelocity+=v;
	}
	public void AddForce(Vector3 f) {
		netForce+=f;
	}

	protected virtual void MovePlayer() {
		// Vector3 forward=Vector3.Cross(Vector3.up,cam.transform.right).normalized;
		Vector3 camForward = Vector3.Cross(Vector3.up,cam.transform.right).normalized;

		//this is now the direction that the player's moving
		Vector3 inputVector = new Vector3(Axes.GetAxis(vertical), 0, Axes.GetAxis(horizontal));
		Vector3 playerDirection = inputVector.x * camForward + inputVector.z * cam.transform.right;
		playerDirection.y = 0f;
		//playerDirection.Normalize();
		//playerDirection*=1/Mathf.Sqrt(2);

		bool grounded=isGrounded();
		
		if (aiming) {
			//moveDirection = aimingSpeed * playerDirection + new Vector3(0,moveDirection.y,0);
			playerMove = aimingSpeed * playerDirection;
		}
		else
		{
			if (inputVector.magnitude > .1) {
				float atan2 = Mathf.Atan2(playerDirection.x, playerDirection.z) * Mathf.Rad2Deg; //- 90;
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, (atan2), 0), 10 * Time.deltaTime);
			}

			//moveDirection = speed * playerDirection + new Vector3(0,moveDirection.y,0);
			//float tempSpeed = speed;
			//if (!grounded){
			//	tempSpeed *= .6f;
			//}

			playerMove = speed * playerDirection;
			
			//Debug.Log(gameObject.name+isGrounded());
			//Debug.Log(gameObject.name+addVelocity);
			//Debug.Log(gameObject.name+(addVelocity+playerMove));

		} //Uncomment this line if aiming is reimplemented

		if(grounded && netForce.y<0) {
			netForce.y=0;
		}
		if(grounded && constVelocity.y<0) {
			constVelocity.y=-.1f;
		}

		constVelocity+=netForce*Time.deltaTime;

		playerMove+=constVelocity;
		Vector3 v=playerMove;
		v.y=0;

		if(rb) {
			//rb.AddForce(playerMove - rb.velocity, ForceMode.VelocityChange);
			//rb.velocity+=addVelocity;

			rb.velocity=v+new Vector3(0,rb.velocity.y,0);

			// transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(forward), 5 * Time.deltaTime);

			//animation
		}else if(cc) {
			
			cc.Move(playerMove*Time.deltaTime);
			//cc.SimpleMove(playerMove+addVelocity);
			
			//Debug.Log("playerMove: "+playerMove+" vs "+cc.velocity+" and isGrounded:"+grounded);
			//Debug.Log("playerMove: "+playerMove+" constVelocity: "+constVelocity+" netForce: "+netForce+" cc vel: "+cc.velocity+" and isGrounded:"+grounded);
		}

		if(grounded) {
			constVelocity*=0.6f;
		}else {
			constVelocity=Vector3.Lerp(constVelocity,Vector3.zero,.5f*Time.deltaTime);
		}


		netForce=gravity*Vector3.down;
		/*
		if(playerMove.magnitude>.25f) {
			constVelocity*=0.8f;
		}else {
			constVelocity*=0.1f;
		}//*/
		//netForce=-0.6*constVelocity+gravity*Vector3.down;

		animator.move(v.magnitude);
	}

	protected virtual void Jump() {
		//Debug.Log("isGrounded: " + isGrounded());
		bool grounded=isGrounded();

		//if(!grounded && jumpsLeft==jumps) {
		//	--jumpsLeft;
		//}

		if (!jumpBtnDown && (jumpsLeft>0 || grounded) && Input.GetButtonDown(Axes.toStr[jump])) {
			Debug.Log(jumpsLeft+" "+jumps);
			if(jumpsLeft==jumps) {//first jump
				constVelocity.y=jumpSpeed;
				sourcePlayerController.Play();//.PlayOneShot(jumpSound, 0.8f,);
			}
			else {//other jumps
				Debug.Log("jumped again");
				constVelocity.y=secondJumpSpeed;
				sourcePlayerController.Play();//sourcePlayerController.PlayOneShot(jumpSound, 0.8f);
			}
			
			jumpBtnDown=true;
			--jumpsLeft;

			animator.jump();
		}else if(grounded){
			//Debug.Log("on ground");
			if(this.name == "Beta") {
				sourcePlayerController.Stop(); //Stop jey sound
			}
			jumpsLeft=jumps;
		}
		if(Input.GetButtonUp(Axes.toStr[jump])) {
			jumpBtnDown=false;
		}
		//if(tag=="Beta") { Debug.Log(jumpsLeft); }

		//animation
		animator.setGrounded(grounded);
	}

	protected virtual void FixedUpdate() {
	}
	protected virtual void Update() {
		//move the player
		if (canMove) {
			MovePlayer();
		}

		if (Input.GetButtonDown(Axes.toStr[carry])) {
			Interact();
			PickUpOrDrop();
		}
        if (Axes.GetAxis(aim) > 0)
        { aiming = true; }
        else
        { aiming = false; }
		//if (Input.GetButtonDown(Axes.toStr[aim])) { Aim(); } //Aim
		//if (Input.GetButtonUp(Axes.toStr[aim])) { Invoke("TurnOffReticle", 0.2f); } //Stop aiming

		/*
		if (Input.GetButtonDown(Axes.toStr[interact])){
			Interact();
		}//*/

		animator.setCarry(carryObj);
	}

	//this is bad, TODO: figure out better, non-redundant approach
	IEnumerator TurnOffColliderCoroutine(double time) {
		yield return new WaitForSeconds((float)time);
		TurnOffCarryCollider();
	}
	IEnumerator TurnOffInteractCoroutine(double time) {
		yield return new WaitForSeconds((float)time);
		TurnOffInteractCollider();
	}

	// turn on colliders to pick up object, or drop if already carrying
	protected void PickUpOrDrop()
	{
		
		if (!carryObj){
			//col.GetComponent<CapsuleCollider>().radius = 0.04304592f;
			carryCollider.SetActive(true);
			Invoke("TurnOffCarryCollider", carryAnimTime);
		}else{
			if(col.tag == "Alpha") {
				col.GetComponent<CapsuleCollider>().center = new Vector3(0,6.5f, 1);
			} else if(col.tag == "Beta"){
				col.GetComponent<CharacterController>().center = new Vector3(0, 0.03f, -0.005f);
				col.GetComponent<CharacterController>().radius = 0.03f;
			}
			//col.GetComponent<CapsuleCollider>().radius = 0.04304592f;
			carryObj.transform.SetParent(null);
			if (carryObj.GetComponent<Rigidbody>()){// Buildable
				//this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				carryObj.GetComponent<Rigidbody>().isKinematic = false;
				carryObj.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
			}else {// Beta
				carryObj.GetComponent<BetaController>().canMove = true;
			}

			carryObj = null;
		}
	}

	IEnumerator DelayedActivateAnim() {
		yield return new WaitForSeconds(0.2f);
		if(!carryObj) {
			animator.activate();
		}
	}
	protected void Interact()
	{
		if (!carryObj)
		{
			StartCoroutine(DelayedActivateAnim());
			interactCollider.SetActive(true);
			Invoke("TurnOffInteractCollider", interactAnimTime);
		}
	}

	protected void TurnOffInteractCollider(){
		interactCollider.SetActive(false);
	}
	protected void TurnOffCarryCollider(){
		carryCollider.SetActive(false);
	}

	IEnumerator delayedPickup(GameObject obj,float delay) {
		yield return new WaitForSeconds(delay);
		obj.transform.SetParent(carryPos);
		//center the object on our carry point
		obj.transform.localPosition=new Vector3(0,0,0);
	}
	public void Carry(GameObject obj) {
		if(!carryObj) {
			carryObj=obj;
			if(col.tag == "Alpha") {
				col.GetComponent<CapsuleCollider>().center = new Vector3(0,6.5f, 1.543962f);
				StartCoroutine(delayedPickup(obj,0.5f));
			}else if(col.tag == "Beta"){
				col.GetComponent<CharacterController>().center = new Vector3(0, 0.03f, 0.01f);
				col.GetComponent<CharacterController>().radius = 0.035f;
				StartCoroutine(delayedPickup(obj,0.5f));
			}
		}
	}
	
	protected void Aim() {
		//display targeting reticle
		aiming = true;
		//reticle.SetActive(true);
	}

	protected void TurnOffReticle()
	{
		//reticle.SetActive(false);
		aiming = false;
	}

	public void Die() {
		if (!dead)
		{
			
			dead = true;
			canMove = false;
			netForce = Vector3.zero;
			constVelocity = Vector3.zero;
			playerMove = Vector3.zero;
			sourcePlayerController.PlayOneShot(dieSound);
			
			// reset carried object
			if (carryObj)
			{
				carryObj.transform.SetParent(null);

				if (carryObj.GetComponent<Rigidbody>())     // Buildable
				{
					carryObj.GetComponent<Rigidbody>().isKinematic = false;
				}
				if (carryObj.GetComponent<Buildable>())
				{
					carryObj.GetComponent<Buildable>().Reset();
				}
				carryObj = null;
			}
			animator.die();
			StartCoroutine(Wait());
		}

		//play an animation, then move player to spawn point
		//make sure anything player is carrying gets reset too
		//how does this know where to send the player?  a 'public GameObject spawnPoint' property might be a good idea
	}

	IEnumerator Wait() //wait x seconds before respawn
	{
		black.SetActive(true);
		yield return new WaitForSeconds(2);
		model.SetActive(false);
		yield return new WaitForSeconds(respawnTimer);
		Respawn();
	}
	
	protected virtual void Respawn(){
		black.SetActive(false);
		netForce =Vector3.zero;
		constVelocity=Vector3.zero;

		canMove = true;
		model.SetActive(true);
		dead = false;

		animator.undie();
	}
}
