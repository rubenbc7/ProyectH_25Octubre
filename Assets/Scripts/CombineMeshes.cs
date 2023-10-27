using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineMeshes : MonoBehaviour
{
    [SerializeField] MeshFilter[] meshFilters;
    private Vector3[] startingVerticies;
    private Vector3[] meshVerticies;
    void Start()
    {
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        
        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;

            meshFilters[i] = GetComponent<MeshFilter>();
            startingVerticies = meshFilters[i].mesh.vertices;
            meshVerticies = meshFilters[i].mesh.vertices;
        }
        
        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine);
        
        GetComponent<MeshFilter>().sharedMesh = combinedMesh;
        gameObject.GetComponent<MeshCollider>().sharedMesh = combinedMesh;
    }
}
