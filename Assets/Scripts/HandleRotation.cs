using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleRotation : MonoBehaviour
{
   // Variable para almacenar el objeto cuya rotación Y queremos seguir
    public Transform targetObject;

    private void Update()
    {
        if (targetObject != null)
        {
            // Obtenemos la rotación Y local del objeto objetivo
            float targetRotationY = targetObject.localEulerAngles.y;

            // Obtenemos la rotación actual del objeto actual
            Vector3 currentRotation = transform.localEulerAngles;

            // Creamos una nueva rotación local para el objeto actual, solo modificando la rotación en el eje Y
            Vector3 newRotation = new Vector3(currentRotation.x, targetRotationY, currentRotation.z);

            // Aplicamos la nueva rotación local al objeto actual
            transform.localEulerAngles = newRotation;
        }
    }
}
