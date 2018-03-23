using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Axes {

	public enum Action {
		MoveXAlpha, MoveYAlpha,
		CamXAlpha, CamYAlpha,
		JumpAlpha, CarryAlpha, AimAlpha, InteractAlpha,
		Construct, Create, Throw,

		MoveXBeta, MoveYBeta,
		CamXBeta, CamYBeta,
		JumpBeta, CarryBeta, AimBeta, InteractBeta,
		Deconstruct, Destroy
	}

	public static Dictionary<Action, string> toStr = new Dictionary<Action, string>{
		{Action.MoveXAlpha,"Horizontal Alpha"},
		{Action.MoveYAlpha,"Vertical Alpha"},
		{Action.CamXAlpha,"Cam Horizontal XBOX Alpha"},
		{Action.CamYAlpha,"Cam Vertical XBOX Alpha"},
		{Action.JumpAlpha, "Jump XBOX Alpha"},
		{Action.CarryAlpha, "Carry XBOX Alpha"},
		{Action.AimAlpha, "Aim XBOX Alpha"},
		{Action.InteractAlpha, "Interact XBOX Alpha"},
		{Action.Construct, "Construct"},
		{Action.Create, "Create"},
		{Action.Throw, "Throw XBOX"},


		{Action.MoveXBeta,"Horizontal Beta"},
		{Action.MoveYBeta,"Vertical Beta"},
		{Action.CamXBeta,"Cam Horizontal XBOX Beta"},
		{Action.CamYBeta,"Cam Vertical XBOX Beta"},
		{Action.JumpBeta, "Jump XBOX Beta"},
		{Action.CarryBeta, "Carry XBOX Beta"},
		{Action.AimBeta, "Aim XBOX Beta"},
		{Action.InteractBeta, "Interact XBOX Beta"},
		{Action.Deconstruct, "Deconstruct"},
		{Action.Destroy, "Destroy"}
	};

	public static float GetAxis(Action a) {
		return Input.GetAxis(toStr[a]);
	}
}
