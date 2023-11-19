using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class IAWayCorrection : MonoBehaviour
{   [SerializeField] WaypointCircuit circuit;
    [SerializeField] private float fixAngle;

    int puntoActual = 0;

    void Update()
    {
        Vector3 direccionCarro = transform.forward;
        Vector3 direccionPuntoActual = circuit.Waypoints[puntoActual].position - transform.position;

        float angulo = Vector3.Angle(direccionCarro, direccionPuntoActual);

        if (angulo > fixAngle)
        {
            Rotar(direccionPuntoActual);
        }
        
        if (Vector3.Distance(transform.position, circuit.Waypoints[puntoActual].position) < 20f)
        {
            puntoActual = (puntoActual + 1) % circuit.Waypoints.Length;
        }
    }

    void Rotar(Vector3 direccionPuntoActual)
    {
        Quaternion rotacion = Quaternion.LookRotation(direccionPuntoActual);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, Time.deltaTime);
    }
}
