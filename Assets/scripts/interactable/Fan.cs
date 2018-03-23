using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : Activatable {

	public float bobSpeed, maxDistance, randomOtherNumber;
	public GameObject beta;
	public float yForce;
	Vector3 maxPosition;

	public float posTop, posBot, fanSpeed;
	public bool up;
	public float speed;
    public bool hzFan = false;

	public enum FanType {
		Constant,Linear,Curve
	}

	public FanType blowType;

	public float
		fanStrength=1f,
		rotSpeed=30;
	public Transform blade;

	// Use this for initialization
	void Start () {
		maxPosition = transform.position + new Vector3(0, 15, 0);
		posTop = transform.position.y + posTop;
		posBot = transform.position.y + posBot;
	}


	private void FixedUpdate()
	{
		if (blade && !hzFan)
		{
			blade.Rotate(transform.up, rotSpeed * Time.deltaTime);
		}

        else if (blade && hzFan)
        {
            blade.Rotate(transform.right, rotSpeed * Time.deltaTime);
        }

        if (beta && !hzFan)
			{
				//float distance = Vector3.Distance(new Vector3(beta.transform.position.x, maxPosition.y,
				//	beta.transform.position.z), beta.transform.position);
				//	Debug.Log(distance);
				//	float windSpeed = EaseOutExpo(maxWindSpeed, randomOtherNumber, distance, maxDistance);
				//	Vector3 newPosition = beta.transform.position + maxWindSpeed * transform.up * Time.deltaTime;
				//	beta.transform.position = Vector3.MoveTowards(beta.transform.position,
				//		newPosition, maxWindSpeed * Time.deltaTime);

				//float speed;
				//if (beta.transform.position.y >= posBot) speed = bobSpeed;
				//else speed = fanSpeed;

				Vector3 b = beta.transform.position;
			//speed = bobSpeed;
				if(b.y > posTop - .2)
				{
					up = false;
				if(b.y <= posTop)
				{

					speed = bobSpeed;
				}
				}
				if(b.y < posBot + .2)
				{
					up = true;
			}
			if (b.y < posBot - .2)
			{
				speed = fanSpeed;
			}

				//if (beta.transform.position.y < posBot - .5f) speed = fanSpeed;
				//if (beta.transform.position.y < posTop) beta.GetComponent<PlayerController>().gravity = 0;
				//if (beta.transform.position.y > posTop) beta.GetComponent<PlayerController>().gravity = 9.81f;

				Vector3 newPosition;
				if (up)
				{
					newPosition = new Vector3(beta.transform.position.x, posTop, beta.transform.position.z);
					//Debug.Log(newPosition);
					//if (posTop - beta.transform.position.y < .2)
					//{
					//	up = false;
					//	speed = bobSpeed;
					//}
				}
				else
				{
					newPosition = new Vector3(beta.transform.position.x, posBot, beta.transform.position.z);
					//Debug.Log(newPosition);
					//	if (Mathf.Abs(beta.transform.position.y - posBot) < .2) up = true;
				}
				beta.transform.position = Vector3.MoveTowards(beta.transform.position, newPosition, speed * Time.deltaTime);

				//Vector3 newPosition = new Vector3(beta.transform.position.x, maxPosition.y, beta.transform.position.z);
				//beta.transform.position = Vector3.MoveTowards(beta.transform.position, newPosition, maxWindSpeed * distance * Time.deltaTime);
				//beta.GetComponent<CharacterController>().Move(new Vector3(0, 20.0f, 0) * 0.5f * Time.deltaTime);
		}
	}



	void OnTriggerEnter(Collider c)
	{
		if (!hzFan && c.tag == "Beta")
		{
			beta = c.gameObject;
			//up = true;
			beta.GetComponent<PlayerController>().gravity = 0;
			beta.GetComponent<PlayerController>().netForce = Vector3.zero;
			beta.GetComponent<PlayerController>().constVelocity = Vector3.zero;
			speed = fanSpeed;
			//PlayerController player = c.gameObject.GetComponentInParent<PlayerController>();
			//float distance = Vector3.Distance(transform.position, c.transform.position);
			////float windSpeed = EaseOutExpo(yForce, randomOtherNumber, distance, maxDistance);
			//player.AddVelocity(new Vector3(0, yForce * .01f / distance, 0));
			//float distance = Vector3.Distance(transform.position, c.transform.position);
			//float windSpeed = EaseOutExpo(maxWindSpeed, randomOtherNumber, distance, maxDistance);
			//Vector3 newPosition = c.transform.position + windSpeed * transform.up * Time.deltaTime;
			//c.transform.position = Vector3.MoveTowards(c.transform.position, newPosition, windSpeed * Time.deltaTime);
		}
	}

	private void OnTriggerExit(Collider c)
	{
		if (!hzFan && c.tag == "Beta")
		{
			beta.GetComponent<PlayerController>().gravity = 25.0f;
			beta = null;
			//up = false;
			//float distance = Vector3.Distance(transform.position, c.transform.position);
			//float windSpeed = EaseOutExpo(maxWindSpeed, randomOtherNumber, distance, maxDistance);
			//Vector3 newPosition = c.transform.position + windSpeed * transform.up * Time.deltaTime;
			//c.transform.position = Vector3.MoveTowards(c.transform.position, newPosition, windSpeed * Time.deltaTime);
		}
	}

	/**
     * exponential easing out - decelerating to zero velocity
     */
	static float EaseOutExpo(float start, float distance, float elapsedTime, float duration)
	{
		// clamp elapsedTime to be <= duration
		if (elapsedTime > duration) { elapsedTime = duration; }
		return distance * (-Mathf.Pow(2, -10 * elapsedTime / duration) + 1) + start;
	}

	void OnTriggerStay(Collider col) {
        //if (col.tag == "Beta")
        //{
        //    beta.GetComponent<PlayerController>().gravity = 0;
        //    beta.GetComponent<PlayerController>().netForce = Vector3.zero;
        //    beta.GetComponent<PlayerController>().constVelocity = Vector3.zero;
        //    //speed = fanSpeed;
        //    //PlayerController player = c.gameObject.GetComponentInParent<PlayerController>();
        //    //float distance = Vector3.Distance(transform.position, c.transform.position);
        //    ////float windSpeed = EaseOutExpo(yForce, randomOtherNumber, distance, maxDistance);
        //    //player.AddVelocity(new Vector3(0, yForce * .01f / distance, 0));
        //    //float distance = Vector3.Distance(transform.position, c.transform.position);
        //    //float windSpeed = EaseOutExpo(maxWindSpeed, randomOtherNumber, distance, maxDistance);
        //    //Vector3 newPosition = c.transform.position + windSpeed * transform.up * Time.deltaTime;
        //    //c.transform.position = Vector3.MoveTowards(c.transform.position, newPosition, windSpeed * Time.deltaTime);
        //}

        //	if(col.gameObject.GetComponent<HeavyObject>()) {
        //		return;
        //	}

        if (hzFan)
        {
            Vector3 dir = transform.up;
            Vector3 vel = Vector3.zero;
            float d = Vector3.Project(col.transform.position - transform.position, dir).magnitude;

            switch (blowType)
            {
                case FanType.Constant:
                    vel = fanStrength * dir;
                    break;
                case FanType.Linear:
                    d = Mathf.Max(d, 1);
                    vel = fanStrength / d * dir;
                    break;
                case FanType.Curve:
                    vel = fanStrength / (d * d + 1) * dir;
                    break;
            }

            //PlayerController player = col.gameObject.GetComponentInParent<PlayerController>();
            //float distance = Vector3.Distance(transform.position, col.transform.position);
            //////float windSpeed = EaseOutExpo(yForce, randomOtherNumber, distance, maxDistance);
            //player.AddVelocity(new Vector3(0, yForce * 5 / distance, 0));

            PlayerController player = col.gameObject.GetComponentInParent<PlayerController>();
            //	Rigidbody body=col.gameObject.GetComponent<Rigidbody>();

            Vector3 a = vel / Time.deltaTime;
            //if (player && player.velocity.y <= 0)
            //{
            Debug.Log("I'm here");
                player.AddForce(a);
                //}else if(body && body.velocity.y<=0) {
                //	body.AddForce(a,ForceMode.Acceleration);
            //}
        }
    }
}
