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

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            carControllerPlayer = other.gameObject.GetComponent<CarControllerPlayer>();
            deform = other.gameObject.GetComponent<Deform>();
            GaugeHealthObject = GameObject.FindGameObjectWithTag("GaugeHealth");
            GaugeNosObject = GameObject.FindGameObjectWithTag("GaugeNos");
            uIGaugeHealth = GaugeHealthObject.GetComponent<UIGauge>();
            uIGaugeNos = GaugeNosObject.GetComponent<UIGauge>();

            carControllerPlayer.CurrentNosLeft = carControllerPlayer.MaxNOSCapacity;
            deform.carHealth = 1000f;
            uIGaugeNos.ApplyCalculation(carControllerPlayer.CurrentNosLeft);
            uIGaugeHealth.ApplyCalculation(1000f);
            Debug.Log("pits");
        }
    }
    
        
    
}
