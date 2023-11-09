
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RemovableSceneTrigger : MonoBehaviour
{
    [SerializeField] private SceneField[] _scenesToLoad;
    [SerializeField] private SceneField[] _scenesToUnload;

    private bool loadingScenes = false;
    //private bool active = true;
    public SceneData SceneData;

    private void Awake(){
        SceneData.LoadSceneState();
        GetComponent<Collider>().enabled = SceneData.sceneEnabled;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("Prota")) && !loadingScenes)
        {
            //active = false;
            SceneData.sceneEnabled = false;
            SceneData.SaveSceneState();
            StartCoroutine(LoadScenesAsync());
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
