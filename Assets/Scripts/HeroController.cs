﻿using UnityEngine;
using System.Collections;
using InControl;

public class HeroController : MonoBehaviour
{
	public int PlayerNumber;

	void Start ()
	{
		//string playerNumberString = this.name.Substring(this.name.Length-2, 1);
		//this.PlayerNumber = int.Parse(playerNumberString);
	}

	public InputDevice InputDevice
	{
		get
		{
			if (this.PlayerNumber >= InputManager.Devices.Count)
			{
				return null;
			}
			return InputManager.Devices[this.PlayerNumber];
		}
	}

	public float Rotate
	{
		get
		{
			InputDevice inputDevice = this.InputDevice;
			return (inputDevice != null) ? Mathf.Atan2(inputDevice.RightStickY, inputDevice.RightStickX): (this.PlayerNumber == 1 ? Mathf.Atan2(Input.GetAxis ("Vertical"), Input.GetAxis ("Horizontal")) : 0.0f);
		}
	}

    public bool Moving
    {
        get
        {
            InputDevice inputDevice = this.InputDevice;
            return (inputDevice != null)
                ? new Vector2(inputDevice.RightStickY, inputDevice.RightStickX).magnitude > 0.05f
                : (this.PlayerNumber == 1 &&
                   new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")).magnitude > 0.05f);
        }
    }

	public bool toFire 
	{
		get 
		{
			InputDevice inputDevice = this.InputDevice;
			return (inputDevice != null) ? inputDevice.RightBumper.IsPressed : (this.PlayerNumber == 1 ? Input.GetButton ("Fire1") : false);
		}
	}

	public float HorizontalMovementAxis
	{
		get
		{
			InputDevice inputDevice = this.InputDevice;
			return (inputDevice != null) ? inputDevice.LeftStickX : (this.PlayerNumber == 1 ? Input.GetAxis ("Horizontal") : 0.0f);
		}
	}

	public float VerticalMovementAxis
	{
		get
		{
			InputDevice inputDevice = this.InputDevice;
			return (inputDevice != null) ? inputDevice.LeftStickY : (this.PlayerNumber == 1 ? Input.GetAxis ("Vertical") : 0.0f);
		}
	}

	public bool Jump
	{
		get
		{
			InputDevice inputDevice = this.InputDevice;
			return false;
			//return (inputDevice != null) ? inputDevice.Action1.WasPressed : (this.PlayerNumber == 1 ? Input.GetButtonDown ("Fire1") : false);
		}
	}

	public bool Shooting
	{
		get
		{
			InputDevice inputDevice = this.InputDevice;
			return false;
			//return (inputDevice != null) ? inputDevice.Action3.WasPressed : (this.PlayerNumber == 1 ? Input.GetButtonDown ("Fire3") : false);
		}
	}

	public bool Stomping
	{
		get
		{
			InputDevice inputDevice = this.InputDevice;
			return (inputDevice != null) ? inputDevice.Action1.WasReleased : (this.PlayerNumber == 1 ? Input.GetButtonUp ("Fire1") : false);
		}
	}

	public bool GetBiggerStart
	{
		get
		{
			InputDevice inputDevice = this.InputDevice;
			return (inputDevice != null) ? inputDevice.Action2.WasPressed : (this.PlayerNumber == 1 ? Input.GetButtonDown ("Fire2") : false);
		}
	}

	public bool GetBiggerHold
	{
		get
		{
			InputDevice inputDevice = this.InputDevice;
			return (inputDevice != null) ? inputDevice.Action2.IsPressed : (this.PlayerNumber == 1 ? Input.GetButton ("Fire2") : false);
		}
	}

	public bool GetBiggerEnd
	{
		get
		{
			InputDevice inputDevice = this.InputDevice;
			return (inputDevice != null) ? inputDevice.Action2.WasReleased : (this.PlayerNumber == 1 ? Input.GetButtonUp ("Fire2") : false);
		}
	}

	public bool GetResetGame
	{
		get
		{
			if (this.PlayerNumber == 1)
			{
				return Input.GetButtonUp ("Fire5");
			}
			return false;
		}
	}

}
