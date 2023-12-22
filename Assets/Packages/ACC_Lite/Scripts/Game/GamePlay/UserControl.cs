using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For user multiplatform control.
/// </summary>
[RequireComponent (typeof (CarControllerPlayer))]
public class UserControl :MonoBehaviour
{

	CarControllerPlayer ControlledCar;

	public float Horizontal { get; private set; }
	public float Vertical { get; private set; }
	public bool Brake { get; private set; }
	public bool Nitro { get; private set; }

	public static MobileControlUI CurrentUIControl { get; set; }

	private void Awake ()
	{
		ControlledCar = GetComponent<CarControllerPlayer> ();
		CurrentUIControl = FindObjectOfType<MobileControlUI> ();
	}

	void Update ()
	{
		if (CurrentUIControl != null && CurrentUIControl.ControlInUse)
		{
			//Mobile control.
			Horizontal = CurrentUIControl.GetHorizontalAxis;
			Vertical = CurrentUIControl.GetVerticalAxis;
			Nitro = CurrentUIControl.GetNitroBool;
		}
		else
		{
			//Standart input control (Keyboard or gamepad).
			Horizontal = Input.GetAxis ("Horizontal");
			Vertical = Input.GetAxis ("Vertical");
			Brake = Input.GetButton ("Jump");
			Nitro = Input.GetButton("Fire1");
		}

		//Apply control for controlled car.
		ControlledCar.UpdateControls (Horizontal, Vertical, Brake, Nitro);
	}
}
