using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAnimator : MonoBehaviour {
	public Animator anim;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*This checks to ensure that both the current state and next state must be any of the given states
	*/
	protected bool checkStateMustBe(string[] names) {
		AnimatorStateInfo info=anim.GetCurrentAnimatorStateInfo(0);
		//AnimatorStateInfo nextinfo=anim.GetNextAnimatorStateInfo(0);
		AnimatorTransitionInfo nextinfo=anim.GetAnimatorTransitionInfo(0);

		bool b=false,nextb=false;
		foreach(string name in names) {
			b|=info.IsName(name);
			nextb|=nextinfo.IsName(name);
		}
		return b||nextb;
	}
	/*this function will take care of setting triggers
	the purpose of this is to check appropriate conditions in which triggers should not be raised
	and so that subclasses may overload it if in the future they have their own set of conditions
	*/
	protected virtual void raiseTrigger(string name) {
		string[] okStates={"Walk","Idle"};
		//if currently in walk or idle, or transitioning to death, triggers are allowed to be raised
		if(name=="Death" || checkStateMustBe(okStates)){
			anim.SetTrigger(name);
		}
	}

	public virtual void jump() {
		raiseTrigger("jump");
	}

	public virtual void setGrounded(bool grounded) {
		anim.SetBool("onGround",grounded);
	}

	public virtual void move(float speed) {
		//Debug.Log("Setting anim velocity to:"+speed);
		anim.SetFloat("speed",speed);
	}

	public virtual void setCarry(bool carry) {
		if(carry && !anim.GetBool("carrying")) {
			raiseTrigger("pickup");
		}
		anim.SetBool("carrying",carry);
	}

	public virtual void activate() {
		raiseTrigger("activate");
	}

	public virtual void die() {
        anim.SetBool("dead", true);
		//raiseTrigger("die");
	}
	public virtual void undie() {
        anim.SetBool("dead", false);
        //raiseTrigger("undie");
	}

	
	public abstract void primary();//construct/deconstruct
	public abstract void secondary();//create/destroy
	//these two dont need to be implemented
	public virtual void releasePrimary() { }
	public virtual void releaseSecondary() { }
}
