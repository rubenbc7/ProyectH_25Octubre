using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebasarSFX : MonoBehaviour
{
    public AudioClip sonido;
    public Rigidbody objetoAseguir;
    public float velocidadMinimaParaSonido = 2.0f; // Ajusta esta velocidad según tus necesidades

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = sonido;
        audioSource.playOnAwake = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (objetoAseguir.velocity.magnitude >= velocidadMinimaParaSonido && collision.gameObject.CompareTag("AICarCollider"))
        {
            audioSource.PlayOneShot(sonido);
        }
    }
}
