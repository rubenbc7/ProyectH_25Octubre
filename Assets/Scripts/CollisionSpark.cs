using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSpark : MonoBehaviour
{
    //public GameObject sparksPrefab; // Assign the sparks particle system prefab in the Inspector
    public float sparksDuration = 1f; // Duration of the particle effect
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Prota")
        {
            ContactPoint contact = collision.contacts[0]; // Get the first contact point
            GameObject sparks = ObjectPoolManager.Instance.GetPooledObject();
            sparks.transform.position = contact.point;
            //Instantiate(sparksPrefab, contact.point, Quaternion.identity);

            ObjectPoolManager.Instance.ReturnToPoolDelayed(sparks, sparksDuration);
        }
        
    }
}
