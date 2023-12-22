using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarRotación : MonoBehaviour
{
    // Cambiar la velocidad de rotación si lo deseas
    public float velocidadRotacion = 30f;

    // Update se llama una vez por frame
    void Update()
    {
        // Obtener la rotación actual en el eje Y
        float rotacionY = transform.rotation.eulerAngles.y;

        // Si la rotación es positiva, cambia a negativa; si es negativa, cambia a positiva
        float nuevaRotacionY = rotacionY > 0 ? -rotacionY : Mathf.Abs(rotacionY);

        // Aplicar la nueva rotación al objeto
        transform.rotation = Quaternion.Euler(0, nuevaRotacionY, 0);
    }
}
