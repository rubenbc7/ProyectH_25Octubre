using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitterController : MonoBehaviour
{
    public Transform targetObject; // El objeto que seguirá
    public ParticleSystem particleSystem; // El sistema de partículas a controlar
    public float minRate = 10f; // Tasa mínima de partículas por segundo
    public float maxRate = 50f; // Tasa máxima de partículas por segundo
    public float maxSpeed = 10f; // Velocidad máxima del objeto a seguir
    public float minRequiredSpeed = 2f; // Velocidad mínima requerida para empezar a aumentar la tasa

    private ParticleSystem.EmissionModule emissionModule;
    private float currentRate = 0f;

    private void Start()
    {
        // Asegurarse de que el sistema de partículas y el objeto a seguir estén asignados
        if (particleSystem == null || targetObject == null)
        {
            Debug.LogError("Falta asignar el sistema de partículas o el objeto a seguir.");
            enabled = false; // Deshabilitar este script
            return;
        }

        // Obtener el módulo de emisión del sistema de partículas
        emissionModule = particleSystem.emission;

        // Inicializar la tasa de partículas en 0
        emissionModule.rateOverTime = 0f;
    }

    private void Update()
    {
        // Obtener la velocidad actual del objeto a seguir
        float currentSpeed = targetObject.GetComponent<Rigidbody>().velocity.magnitude;

        // Aumentar gradualmente la tasa de partículas si se alcanza la velocidad mínima requerida
        if (currentSpeed >= minRequiredSpeed)
        {
            // Calcular la velocidad normalizada del objeto a seguir
            float normalizedSpeed = Mathf.Clamp((currentSpeed - minRequiredSpeed) / maxSpeed, 0f, 1f);

            // Calcular la tasa de partículas interpolada basada en la velocidad normalizada
            float targetRate = Mathf.Lerp(minRate, maxRate, normalizedSpeed);

            // Aumentar la tasa de partículas gradualmente
            currentRate = Mathf.MoveTowards(currentRate, targetRate, Time.deltaTime * (maxRate - minRate));

            // Actualizar la tasa de emisión del sistema de partículas
            emissionModule.rateOverTime = currentRate;
        }
        else
        {
            // Si no se alcanza la velocidad mínima requerida, detener la emisión
            currentRate = 0f;
            emissionModule.rateOverTime = currentRate;
        }
    }
}
