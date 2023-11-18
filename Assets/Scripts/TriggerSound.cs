using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    [SerializeField] Transform objetivo;
    private AudioSource audioSource;
    private bool objetivoAdelante = false;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 direccionOrigen = transform.forward;
        Vector3 direccionObjetivo = objetivo.position - transform.position;

        float angulo = Vector3.Angle(direccionOrigen, direccionObjetivo);

        if (angulo < 90f)
        {
            objetivoAdelante = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && objetivoAdelante)
        {
            audioSource.Play();
        }
    }
}
