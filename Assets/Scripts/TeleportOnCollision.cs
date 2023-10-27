using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnCollision : MonoBehaviour
{
    [SerializeField] private Transform targetPosition; // Objeto al que deseas mover el objeto con el tag "AICAR"

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("AICAR"))
        {
            // Mueve el objeto con el tag "AICAR" a la posición del objeto targetPosition
            collision.transform.position = targetPosition.position;
        }
    }
}
