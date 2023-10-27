using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform target; // Objeto a seguir
    public float maxShakeAmount = 1.0f; // Intensidad máxima del shake
    public float shakeSpeed = 1.0f;
    public float shakeStart = 60f; // Velocidad a la que se aplica el shake

    private Vector3 originalPosition;
    private float currentShakeAmount = 0.0f;

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        if (target != null)
        {
            // Calcula el shake basado en la velocidad del objeto
            float targetVelocity = target.GetComponent<Rigidbody>().velocity.magnitude;
            currentShakeAmount = Mathf.Clamp(targetVelocity * shakeSpeed, 0.0f, maxShakeAmount);

            // Aplica el shake a la posición de la cámara
            if(targetVelocity >= shakeStart){
            Vector3 randomOffset = Random.insideUnitSphere * currentShakeAmount;
            transform.localPosition = originalPosition + randomOffset;
            }
            
        }
        else
        {
            currentShakeAmount = 0.0f;
            transform.localPosition = originalPosition;
        }
    }

}

