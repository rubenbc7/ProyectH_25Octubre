using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyConverter : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Verifica si el objeto ya tiene un componente Rigidbody.
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                // Agrega un componente Rigidbody al objeto.
                rb = gameObject.AddComponent<Rigidbody>();
                //Debug.Log("Se agregó un componente Rigidbody al objeto.");
            }
            Destroy(gameObject, 10f);
        }
    }
}
