using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementarFovSegunVelocidad : MonoBehaviour
{
    public Transform objetoSeguido; // Referencia al objeto cuya velocidad queremos seguir
    public float velocidadMaxima = 1000f; // Velocidad máxima del objeto
    public float fovInicial = 45f; // FOV inicial de la cámara
    public float fovMaximo = 120f; // FOV máximo que queremos alcanzar

    private Camera cam;
    private float fovActual;
    public CarControllerPlayer carControllerPlayer;
    float nitroMultiplier = 1f;

    private void Start()
    {
        cam = GetComponent<Camera>();
        fovActual = fovInicial;
        cam.fieldOfView = fovActual;
    }

    private void Update()
    {
        float velocidadActual = objetoSeguido.GetComponent<Rigidbody>().velocity.magnitude;

        if(carControllerPlayer.InNitro && velocidadActual > 3f){
            nitroMultiplier = 1.25f;
        }
        if(!carControllerPlayer.InNitro){
            nitroMultiplier = 1f;
        }

        // Obtener la velocidad actual del objeto

        // Calcular el FOV objetivo en función de la velocidad
        float fovObjetivo = (fovInicial + (velocidadActual / velocidadMaxima) * (fovMaximo - fovInicial)) * nitroMultiplier;

        // Aplicar la interpolación suave para suavizar el cambio de FOV
        fovActual = Mathf.Lerp(fovActual, fovObjetivo, Time.deltaTime * 5f);

        // Establecer el FOV de la cámara
        cam.fieldOfView = fovActual ;
    }
}
