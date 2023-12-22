using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Car sound controller, for play car sound effects
/// </summary>

[RequireComponent (typeof (CarControllerPlayer))]
public class CarSoundController :MonoBehaviour
{

	[Header("Engine sounds")]
	[SerializeField] AudioClip EngineIdleClip;
	[SerializeField] AudioClip EngineBackFireClip;
	[SerializeField] float PitchOffset = 0.5f;
	[SerializeField] AudioSource EngineSource;

	[Header("Slip sounds")]
	[SerializeField] AudioSource SlipSource;
	[SerializeField] float MinSlipSound = 0.15f;
	[SerializeField] float MaxSlipForSound = 1f;

	[Header("NOS start")]
	[SerializeField] AudioSource NosSource;
	[SerializeField] AudioClip NosStart;
	private bool sonidoReproducido = false;

	CarControllerPlayer CarControllerPlayer;

	float MaxRPM { get { return CarControllerPlayer.GetMaxRPM; } }
	float EngineRPM { get { return CarControllerPlayer.EngineRPM; } }

	private void Awake ()
	{
		CarControllerPlayer = GetComponent<CarControllerPlayer> ();
		CarControllerPlayer.BackFireAction += PlayBackfire;
	}

	void Update ()
	{

		//Engine PRM sound
		EngineSource.pitch = (EngineRPM / MaxRPM) + PitchOffset;

		//Slip sound logic
		if (CarControllerPlayer.CurrentMaxSlip > MinSlipSound
		)
		{
			if (!SlipSource.isPlaying)
			{
				SlipSource.Play ();
			}
			var slipVolumeProcent = CarControllerPlayer.CurrentMaxSlip / MaxSlipForSound;
			SlipSource.volume = slipVolumeProcent * 0.5f;
			SlipSource.pitch = Mathf.Clamp (slipVolumeProcent, 0.75f, 1);
		}
		else
		{
			SlipSource.Stop ();
		}
		if(CarControllerPlayer.InNitro  && !sonidoReproducido && CarControllerPlayer.CurrentSpeed >1f){
			
                NosSource.Play();
				sonidoReproducido = true;
			
		}
		if (!CarControllerPlayer.InNitro)
        {
            sonidoReproducido = false;
        }
	}

	void PlayBackfire ()
	{
		EngineSource.PlayOneShot (EngineBackFireClip);
	}
}
