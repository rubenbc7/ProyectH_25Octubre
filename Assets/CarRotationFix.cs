using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRotationFix : MonoBehaviour
{
    public float maxRotationThreshold = 50f;
    public float minRotationThreshold = -50f;
    public float maxSpeedForFix = 5f;
    public float fixRotationSpeed = 30f;
    public float timeThreshold = 4f;

    private Quaternion targetRotation;
    private float timeAtThreshold;

    private Rigidbody carRigidbody;

    private void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float currentRotation = transform.eulerAngles.z;
        float currentRotationNeg = (currentRotation > 180) ? currentRotation - 360 : currentRotation;
        float currentRotationx = transform.eulerAngles.x;
        float currentRotationxNeg = (currentRotationx > 180) ? currentRotationx - 360 : currentRotationx;

        if ((currentRotation > maxRotationThreshold || currentRotationNeg < minRotationThreshold) &&
            carRigidbody.velocity.magnitude < maxSpeedForFix)
        {
            if (Time.time - timeAtThreshold > timeThreshold)
            {
                // Calculate target rotation towards 0 degrees on the Z-axis
                targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
               
            }

            // Rotate the car towards the target rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, fixRotationSpeed * Time.deltaTime);
        }
        if ((currentRotationx > maxRotationThreshold || currentRotationxNeg < minRotationThreshold) &&
            carRigidbody.velocity.magnitude < maxSpeedForFix)
        {
            if (Time.time - timeAtThreshold > timeThreshold)
            {
                // Calculate target rotation towards 0 degrees on the Z-axis
                targetRotation = Quaternion.Euler(0f, transform.eulerAngles.y, transform.eulerAngles.z);
               
            }

            // Rotate the car towards the target rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, fixRotationSpeed * Time.deltaTime);
        }
        else
        {
            // Reset the time threshold when rotation is within limits
            timeAtThreshold = Time.time;
        }
    }
}
