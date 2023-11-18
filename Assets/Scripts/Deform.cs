using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deform : MonoBehaviour
{
    //[Range(0, 10)]
    [SerializeField] float deformRadius = 0.2f;
    [SerializeField] float hardness = 0f;
    private float initialHardness;
    //[Range(0, 1)]
    [SerializeField] float damageFalloff = 1;
    //[Range(0, 10)]
    [SerializeField] float damageMultiplier = 1;
    //[Range(0, 100000)]
    [SerializeField] float minDamage = 1;
    [SerializeField] public float carHealth = 2000f;

    public AudioClip[] collisionSounds;
 
    [SerializeField] private MeshFilter filter;
    //private Rigidbody physics;
    private MeshCollider coll;
    private MeshCollider Startingcoll;
    [SerializeField] private Vector3[] startingVerticies;
    [SerializeField] private Vector3[] meshVerticies;
    public GameObject NormalCamera;
    public GameObject CrashCamera;
    public GameObject DrownedCamera;
    public CameraController cameraController;

    [SerializeField] MeshFilter[] carParts;
    Rigidbody m_Rigidbody;
    public GameObject crashUI;
    public GameObject raceUI;
    public bool crashed = false;
    public UIGauge uIHealthGauge;
    private Vector3 deform;
    private bool isDrowned = false;
    [SerializeField] private GameObject _carroceria;
 
    void Start()
    {
        CombineInstance[] combine = new CombineInstance[carParts.Length];
        
        for (int i = 0; i < carParts.Length; i++)
        {
            combine[i].mesh = carParts[i].sharedMesh;
            combine[i].transform = carParts[i].transform.localToWorldMatrix;

        }
        
        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine);
        
        GetComponent<MeshFilter>().sharedMesh = combinedMesh;
        GetComponent<MeshCollider>().sharedMesh = combinedMesh;

        filter = GetComponent<MeshFilter>();
 
        if (GetComponent<MeshCollider>())
            coll = GetComponent<MeshCollider>();

        Startingcoll = coll;
        startingVerticies = filter.mesh.vertices;
        meshVerticies = filter.mesh.vertices;
        initialHardness = hardness;
        Debug.Log(meshVerticies);
    }
    void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag == "WaterCollider")
       {
        isDrowned = true;
        Drown();
       }
    }
    void Crash()
    {
                //Debug.Log(collisionPower);
                Debug.Log("FATAL CRASH");

                crashed = true;
                carHealth = 0f;
                
                
                NormalCamera.SetActive(false);
                CrashCamera.SetActive(true);

                Cursor.lockState = CursorLockMode.Confined;
			    Cursor.visible = true;
    }

    void Drown()
    {
                //Debug.Log(collisionPower);
                Debug.Log("Te Ahogaste");

                isDrowned = true;
                carHealth = 0f;
                NormalCamera.SetActive(false);
                CrashCamera.SetActive(true);
                

                Cursor.lockState = CursorLockMode.Confined;
			    Cursor.visible = true;
                StartCoroutine(WaitForOneSecond());
                m_Rigidbody = GetComponent<Rigidbody>();
                m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                DrownedCamera.SetActive(true);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        float collisionPower = (collision.impulse.magnitude)/100;
        

        if(carHealth <= 0f && !crashed || collisionPower > 400f || collision.gameObject.tag == "terrain" && !crashed)
            {
                collisionPower = 400f;
                hardness = hardness/2;
                
                Crash();
                //cameraController.SetCrashCamera();
                StartCoroutine(WaitForOneSecond());
                m_Rigidbody = GetComponent<Rigidbody>();
                m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                crashUI.SetActive(true);
            }

        float maxDeform = collisionPower / (hardness * 100);

        

        if (collisionPower > minDamage)
        {
            carHealth = carHealth - (collisionPower/2);
            uIHealthGauge.ApplyCalculation(carHealth);
            
            if (collisionSounds.Length > 0)
                AudioSource.PlayClipAtPoint(collisionSounds[Random.Range(0, collisionSounds.Length)], transform.position, 0.5f);
 
            foreach (ContactPoint point in collision.contacts)
            {
                Debug.Log(meshVerticies);
                for (int i = 0; i < meshVerticies.Length; i++)
                {
                    
                    Vector3 vertexPosition = meshVerticies[i];
                    Vector3 pointPosition = transform.InverseTransformPoint(point.point);
                    float distanceFromCollision = Vector3.Distance(vertexPosition, pointPosition);
                    float distanceFromOriginal = Vector3.Distance(startingVerticies[i], vertexPosition);
 
                    if (distanceFromCollision < deformRadius && distanceFromOriginal < maxDeform) // If within collision radius and within max deform
                    {
                        float falloff = 1 - (distanceFromCollision / deformRadius) * damageFalloff;
 
                        float xDeform = pointPosition.x * falloff;
                        float yDeform = pointPosition.y * falloff;
                        float zDeform = pointPosition.z * falloff;
 
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
    private IEnumerator WaitForOneSecond()
    {
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = Time.timeScale * 0.01f;
        
        //crashUI.SetActive(true);
        raceUI.SetActive(false);

        yield return new WaitForSeconds(0.09f);
        
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.01f;
        gameObject.GetComponent<UserControl> ().enabled = false;
        
        
    }
    
 
    void UpdateMeshVerticies()
    {
        filter.mesh.vertices = meshVerticies;
        coll.sharedMesh = filter.mesh;
    }

    public void RestoreMeshVerticies()
    {
       
       filter.mesh.vertices = startingVerticies;
        meshVerticies = filter.mesh.vertices;
       coll.sharedMesh = Startingcoll.sharedMesh;
       hardness = initialHardness;
       gameObject.GetComponent<UserControl> ().enabled = true;
       m_Rigidbody = GetComponent<Rigidbody>();
       m_Rigidbody.constraints = RigidbodyConstraints.None;;
       
       //Start();
    }
}