using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IADeform : MonoBehaviour
{
    //[Range(0, 10)]
    [SerializeField] float deformRadius = 0.2f;
    [SerializeField] float hardness = 0f;
    //[Range(0, 1)]
    [SerializeField] float damageFalloff = 1;
    //[Range(0, 10)]
    [SerializeField] float damageMultiplier = 1;
    //[Range(0, 100000)]
    [SerializeField] float minDamage = 1;
    public AudioClip[] collisionSounds;

    private MeshFilter filter;
    private MeshCollider coll;
    private Vector3[] startingVerticies;
    private Vector3[] meshVerticies;

    void Start()
    {
        filter = GetComponent<MeshFilter>();

        coll = GetComponent<MeshCollider>();
 
        startingVerticies = filter.mesh.vertices;
        meshVerticies = filter.mesh.vertices;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        float collisionPower = collision.impulse.magnitude;
        float maxDeform = collisionPower / hardness;
        if (collisionPower > minDamage)
        {
            Vector3[] inversePoints = new Vector3[collision.contacts.Length];
            for (int i = 0; i < collision.contacts.Length; i++)
            {
                inversePoints[i] = transform.InverseTransformPoint(collision.contacts[i].point);
            }

            for (int i = 0; i < meshVerticies.Length; i++)
            {
                Vector3 vertexPosition = meshVerticies[i];
                Vector3 startingVertex = startingVerticies[i];

                for (int j = 0; j < inversePoints.Length; j++)
                {
                    float distanceFromCollision = Vector3.Distance(vertexPosition, inversePoints[j]);
                    float distanceFromOriginal = Vector3.Distance(startingVertex, vertexPosition);

                    if (distanceFromCollision < deformRadius && distanceFromOriginal < maxDeform) 
                    {
                        float falloff = 1 - distanceFromCollision / deformRadius * damageFalloff;

                        float xDeform = inversePoints[j].x * falloff;
                        float yDeform = inversePoints[j].y * falloff;
                        float zDeform = inversePoints[j].z * falloff;

                        xDeform = Mathf.Clamp(xDeform, 0, maxDeform);
                        yDeform = Mathf.Clamp(yDeform, 0, maxDeform);
                        zDeform = Mathf.Clamp(zDeform, 0, maxDeform);

                        Vector3 deform = new Vector3(xDeform, yDeform, zDeform);
                        meshVerticies[i] -= deform * damageMultiplier;
                    }
                }
            }
 
            UpdateMeshVerticies();
        }
    }
 
    void UpdateMeshVerticies()
    {
        filter.mesh.vertices = meshVerticies;
        coll.sharedMesh = filter.mesh;
    }


}
