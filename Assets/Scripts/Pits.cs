using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pits : MonoBehaviour
{
    public CarControllerPlayer carControllerPlayer;
    public Deform deform;
    public UIGauge uIGaugeNos;
    public UIGauge uIGaugeHealth;
    private GameObject GaugeHealthObject;
    private GameObject GaugeNosObject;
    private GameObject textObject;
    private Animator textAnimator;
    private bool animacionReproducida = false;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            carControllerPlayer = other.gameObject.GetComponent<CarControllerPlayer>();
            deform = other.gameObject.GetComponent<Deform>();
            GaugeHealthObject = GameObject.FindGameObjectWithTag("GaugeHealth");
            GaugeNosObject = GameObject.FindGameObjectWithTag("GaugeNos");
            uIGaugeHealth = GaugeHealthObject.GetComponent<UIGauge>();
            uIGaugeNos = GaugeNosObject.GetComponent<UIGauge>();
            textObject = GameObject.FindGameObjectWithTag("CarRestoredText");
            textAnimator = textObject.GetComponent<Animator>();

            carControllerPlayer.CurrentNosLeft = carControllerPlayer.MaxNOSCapacity;
            deform.carHealth = 2000f;
            deform.RestoreMeshVerticies();

            uIGaugeNos.ApplyCalculation(carControllerPlayer.CurrentNosLeft);
            uIGaugeHealth.ApplyCalculation(2000f);

            if (!animacionReproducida) {
                textAnimator.Play("CarRestoredanim");
                animacionReproducida = true;
            }
            
            
        }
    }
   private void OnTriggerExit(Collider other)
   {
    
        animacionReproducida = false;
            
        
   }
    
        
    
}
