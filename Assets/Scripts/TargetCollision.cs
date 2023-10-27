using UnityEngine;
using System.Collections;

public class TargetCollision : MonoBehaviour
{
    
    public Transform cameraToShake;
    public float maxShakeIntensity = 0.5f;
    public float shakeDuration = 0.5f;

    private Vector3 originalCameraPosition;
    private float currentShakeDuration;
    private float currentShakeIntensity;

    public AudioClip sonido;
    private AudioSource audioSource;
    private bool sonidoReproducido = false;
    public CarControllerPlayer carControllerPlayer;
    public Deform deform;

    private void Start()
    {
        originalCameraPosition = cameraToShake.localPosition;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = sonido;
        audioSource.playOnAwake = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Pits")
        {
            carControllerPlayer.CurrentNosLeft = carControllerPlayer.MaxNOSCapacity;
            deform.carHealth = 500f;
            Debug.Log("pits");
        }
        
        if(collision.gameObject.layer != 8 && collision.gameObject.layer != 10 && collision.gameObject.tag != "Prota"){
            float collisionForce = collision.impulse.magnitude;
            currentShakeIntensity = Mathf.Clamp(collisionForce * 0.05f, 0f, maxShakeIntensity);
        
            // Start shaking the camera
            currentShakeDuration = shakeDuration;
            StartCoroutine(ShakeCamera());

            if(!sonidoReproducido)
            {
                audioSource.PlayOneShot(sonido);
                sonidoReproducido = true;
                Invoke("ResetearBandera", sonido.length);
            }
            //Debug.Log( "collide (name) : " + collision.gameObject.name );
        }
        
    }
     private void ResetearBandera()
    {
        sonidoReproducido = false;
    }

    private IEnumerator ShakeCamera()
    {
        while (currentShakeDuration > 0)
        {
            Vector3 randomShake = Random.insideUnitSphere * currentShakeIntensity;
            cameraToShake.localPosition = originalCameraPosition + randomShake;

            currentShakeDuration -= Time.deltaTime;
            yield return null;
        }

        cameraToShake.localPosition = originalCameraPosition;
    }
}
