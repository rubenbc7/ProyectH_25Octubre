using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Mobile input arrows.
/// </summary>
public class MobileControlUI : MonoBehaviour
{

	[SerializeField] CustomButton TurnLeftButton;
	[SerializeField] CustomButton TurnRigthButton;
	[SerializeField] CustomButton AccelerationButton;
	[SerializeField] CustomButton DecelerationButton;
	[SerializeField] CustomButton NitroButton;

	bool LeftPressed { get { return TurnLeftButton.ButtonIsPressed; } }
	bool RightPressed { get { return TurnRigthButton.ButtonIsPressed; } }
	bool AccelerationPressed { get { return AccelerationButton.ButtonIsPressed; } }
	bool DecelerationPressed { get { return DecelerationButton.ButtonIsPressed; } }
	bool NitroPressed {get{return NitroButton.ButtonIsPressed;}}
	public bool ControlInUse { get { return LeftPressed || RightPressed || AccelerationPressed || DecelerationPressed || NitroPressed; } }

	public float GetHorizontalAxis
	{
		get
		{
			if (LeftPressed)
			{
				return -1;
			}
			else if (RightPressed)
			{
				return 1;
			}
			return 0;
		}
	}

	public float GetVerticalAxis
	{
		get
		{
			if (AccelerationPressed)
			{
				return 1;
			}
			else if (DecelerationPressed)
			{
				return -1;
			}
			return 0;
		}
	}
	public bool GetNitroBool
	{
		get
		{
			if(NitroPressed)
			{
				return true;
			}
			else{
				return false;
			}
		}
	}
}
