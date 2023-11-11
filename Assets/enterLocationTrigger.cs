
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enterLocationTrigger : MonoBehaviour
{
    [SerializeField] private SceneField[] _scenesToLoad;
    [SerializeField] private SceneField[] _scenesToUnload;
    [SerializeField] private SceneField _scenesCarSpawn;
    [SerializeField] private bool _carVisible = false;
    public string eButton = "e";
    public GameObject enterLocationbutton;
    public GameObject car; 

    private bool loadingScenes = false;
    void Start()
    {
        enterLocationbutton.SetActive(false);
        car = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag("Prota")) && !loadingScenes)
        {
            enterLocationbutton.SetActive(true);
            

            if(Input.GetKeyDown(eButton))
            {
                if(car == null)
                {
                    Scene otraEscena = SceneManager.GetSceneByName(_scenesCarSpawn.SceneName);
                    GameObject[] objetosEnOtraEscena = otraEscena.GetRootGameObjects();

                    car = System.Array.Find(objetosEnOtraEscena, obj => obj.name == "PlayerCar");

                    if(car == null)
                    {
                        foreach (var obj in objetosEnOtraEscena)
                        {
                            car = obj.transform.Find("PlayerCar")?.gameObject;

                            if (car != null)
                            {
                                break;
                            }
                        }
                    }
                    

                    //car = System.Array.Find(objetosEnOtraEscena, obj => obj.CompareTag("Player"));
                    //car = GameObject.FindGameObjectWithTag("CarPos");
                }
                
                car.SetActive(_carVisible);
                StartCoroutine(LoadScenesAsync());
            }

            
        }
    }
      void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Prota")
        {
           
            enterLocationbutton.SetActive(false);
        }
    }

    private IEnumerator LoadScenesAsync()
    {
        loadingScenes = true;

        foreach (var sceneToLoad in _scenesToLoad)
        {
            if (!SceneManager.GetSceneByName(sceneToLoad.SceneName).isLoaded)
            {
                AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
                operation.allowSceneActivation = false; // Evita que la escena se active automáticamente

                while (!operation.isDone)
                {
                    float progress = Mathf.Clamp01(operation.progress / 0.9f); // Limita el progreso a 0-1
                    Debug.Log("Cargando escena " + sceneToLoad.SceneName + " - Progreso: " + (progress * 100) + "%");

                    if (progress >= 0.9f)
                    {
                        operation.allowSceneActivation = true; // Activa la escena cuando esté casi cargada
                    }

                    yield return null; // Espera un frame antes de la siguiente iteración
                }
            }
        }

        UnloadScenes();
        loadingScenes = false;
    }

    private void UnloadScenes()
    {
        foreach (var sceneToUnload in _scenesToUnload)
        {
            if (SceneManager.GetSceneByName(sceneToUnload.SceneName).isLoaded)
            {
                SceneManager.UnloadSceneAsync(sceneToUnload.SceneName);
            }
        }
    }
}
