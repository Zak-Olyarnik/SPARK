using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaAnimator : PlayerAnimator {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
	protected override void raiseTrigger(string name) {
		string[] okStates={"Walk","Idle","Carry Idle","Carry"};
		//if currently in walk or idle, or transitioning to death, triggers are allowed to be raised
		if(name=="Death" || checkStateMustBe(okStates)){
			anim.SetTrigger(name);
		}
	}

	public override void primary() {
		raiseTrigger("construct");
		setCarry(false);
	}

	public override void secondary() {
		anim.SetBool("create",true);
	}

	public override void releaseSecondary() {
		anim.SetBool("create",false);
	}

	public virtual void setPushing(bool push) {
		anim.SetBool("pushing",push);
	}
}
