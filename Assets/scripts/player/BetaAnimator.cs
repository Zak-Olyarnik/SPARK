using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaAnimator : PlayerAnimator {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public override void primary() {
		raiseTrigger("deconstruct");
	}

	public override void secondary() {
		raiseTrigger("destroy");
	}
}
