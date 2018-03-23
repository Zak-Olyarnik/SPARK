using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckController : MonoBehaviour {

	public GameObject alphaControls, betaControls;
	public Sprite alphaPS4, alphaXBOX, betaPS4, betaXBOX;

	public GameObject alphaCarry, alphaCreate, alphaConstruct;
	public GameObject betaCarry, betaDestroy, betaDeconstruct, betaJump;
	public Sprite alphaCarryPS4, alphaCarryXBOX, alphaCreatePS4, alphaCreateXBOX, alphaConstructPS4, alphaConstructXBOX;
	public Sprite betaCarryPS4, betaCarryXBOX, betaDestroyPS4, betaDestroyXBOX, betaDeconstructPS4, betaDeconstructXBOX, betaJumpPS4, betaJumpXBOX;

	void Update()
	{
		if (Input.GetJoystickNames().Length > 0)
		{
			if (Input.GetJoystickNames()[0] == "Wireless Controller") //Alpha
			{
				Axes.toStr[Axes.Action.CamXAlpha] = "Cam Horizontal PS4 Alpha";
				Axes.toStr[Axes.Action.CamYAlpha] = "Cam Vertical PS4 Alpha";
				Axes.toStr[Axes.Action.JumpAlpha] = "Jump PS4 Alpha";
				Axes.toStr[Axes.Action.CarryAlpha] = "Carry PS4 Alpha";
				Axes.toStr[Axes.Action.AimAlpha] = "Aim PS4 Alpha";
				Axes.toStr[Axes.Action.InteractAlpha] = "Interact PS4 Alpha";
				Axes.toStr[Axes.Action.Throw] = "Throw PS4";
				alphaControls.GetComponent<Image>().sprite = alphaPS4;

				if (alphaCarry)
				{
					alphaCarry.GetComponent<Image>().sprite = alphaCarryPS4;
					alphaCreate.GetComponent<Image>().sprite = alphaCreatePS4;
					alphaConstruct.GetComponent<Image>().sprite = alphaConstructPS4;
				}
			}
			if (Input.GetJoystickNames()[0] == "Controller (Xbox One For Windows)") //Alpha
			{
				Axes.toStr[Axes.Action.CamXAlpha] = "Cam Horizontal XBOX Alpha";
				Axes.toStr[Axes.Action.CamYAlpha] = "Cam Vertical XBOX Alpha";
				Axes.toStr[Axes.Action.JumpAlpha] = "Jump XBOX Alpha";
				Axes.toStr[Axes.Action.CarryAlpha] = "Carry XBOX Alpha";
				Axes.toStr[Axes.Action.AimAlpha] = "Aim XBOX Alpha";
				Axes.toStr[Axes.Action.InteractAlpha] = "Interact XBOX Alpha";
				Axes.toStr[Axes.Action.Throw] = "Throw XBOX";
				alphaControls.GetComponent<Image>().sprite = alphaXBOX;

				if (alphaCarry)
				{
					alphaCarry.GetComponent<Image>().sprite = alphaCarryXBOX;
					alphaCreate.GetComponent<Image>().sprite = alphaCreateXBOX;
					alphaConstruct.GetComponent<Image>().sprite = alphaConstructXBOX;
				}
			}
		}

		if (Input.GetJoystickNames().Length > 1)
		{
			if (Input.GetJoystickNames()[1] == "Wireless Controller") //Beta
			{
				Axes.toStr[Axes.Action.CamXBeta] = "Cam Horizontal PS4 Beta";
				Axes.toStr[Axes.Action.CamYBeta] = "Cam Vertical PS4 Beta";
				Axes.toStr[Axes.Action.JumpBeta] = "Jump PS4 Beta";
				Axes.toStr[Axes.Action.CarryBeta] = "Carry PS4 Beta";
				Axes.toStr[Axes.Action.AimBeta] = "Aim PS4 Beta";
				Axes.toStr[Axes.Action.InteractBeta] = "Interact PS4 Beta";
				betaControls.GetComponent<Image>().sprite = betaPS4;

				if (betaCarry)
				{
					betaCarry.GetComponent<Image>().sprite = betaCarryPS4;
					betaDestroy.GetComponent<Image>().sprite = betaDestroyPS4;
					betaDeconstruct.GetComponent<Image>().sprite = betaDeconstructPS4;
					betaJump.GetComponent<Image>().sprite = betaJumpPS4;
				}
			}

			if (Input.GetJoystickNames()[1] == "Controller (Xbox One For Windows)") //Beta
			{
				Axes.toStr[Axes.Action.CamXBeta] = "Cam Horizontal XBOX Beta";
				Axes.toStr[Axes.Action.CamYBeta] = "Cam Vertical XBOX Beta";
				Axes.toStr[Axes.Action.JumpBeta] = "Jump XBOX Beta";
				Axes.toStr[Axes.Action.CarryBeta] = "Carry XBOX Beta";
				Axes.toStr[Axes.Action.AimBeta] = "Aim XBOX Beta";
				Axes.toStr[Axes.Action.InteractBeta] = "Interact XBOX Beta";
				betaControls.GetComponent<Image>().sprite = betaXBOX;

				if (betaCarry)
				{
					betaCarry.GetComponent<Image>().sprite = betaCarryXBOX;
					betaDestroy.GetComponent<Image>().sprite = betaDestroyXBOX;
					betaDeconstruct.GetComponent<Image>().sprite = betaDeconstructXBOX;
					betaJump.GetComponent<Image>().sprite = betaJumpXBOX;
				}
			}
		}
	}
}
