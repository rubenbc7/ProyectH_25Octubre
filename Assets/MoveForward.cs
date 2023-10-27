using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
   public float speed = 5.0f; // Adjust the speed as needed

    void Update()
    {
        // Calculate the distance to move based on time and speed
        float distanceToMove = speed * Time.deltaTime;

        // Move the object forward
        transform.Translate(Vector3.forward * distanceToMove);
    }
}
