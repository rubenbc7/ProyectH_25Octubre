using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Main Menu Objects")]
    [SerializeField] private GameObject _loadingBarObject;
    [SerializeField] private Image _loadingBar;
    [SerializeField] private GameObject[] _ObjectsToHide;

    [Header("Scenes to load")]
    [SerializeField] private SceneField _persistentGameplay;
    [SerializeField] private SceneField _levelScene;
    [SerializeField] private SceneField _levelSceneExtra;
    [Header("Scenes to Unload")]
    [SerializeField] private SceneField _scenesToUnload;

    private List<AsyncOperation> _scenesToLoad = new List<AsyncOperation>();

    private void Awake()
    {
        _loadingBarObject.SetActive(false);
    }

    // Update is called once per frame
    public void StartGame()
    {
        // Hide the menu immediately
        HideMenu();

        // Load scenes asynchronously
        _scenesToLoad.Add(SceneManager.LoadSceneAsync(_persistentGameplay));
        _scenesToLoad.Add(SceneManager.LoadSceneAsync(_levelScene, LoadSceneMode.Additive));
        _scenesToLoad.Add(SceneManager.LoadSceneAsync(_levelSceneExtra, LoadSceneMode.Additive));

        // Unload scenes asynchronously
        SceneManager.UnloadSceneAsync(_scenesToUnload);

        // Start a coroutine to check loading progress
        StartCoroutine(ProgressLoadingBar());
    }

    private void HideMenu()
    {
        for (int i = 0; i < _ObjectsToHide.Length; i++)
        {
            _ObjectsToHide[i].SetActive(false);
        }
    }

    private IEnumerator ProgressLoadingBar()
    {
        float totalProgress = 0f;
        _loadingBarObject.SetActive(true);

        // Check the progress of all scenes
        foreach (var sceneOperation in _scenesToLoad)
        {
            while (!sceneOperation.isDone)
            {
                totalProgress = CalculateTotalProgress();
                _loadingBar.fillAmount = totalProgress;

                if (totalProgress >= 0.9f)
                {
                    // Once the total progress is at least 90%, you can consider the scenes almost loaded
                    HideMenu();
                }

                yield return null;
            }
        }
    }

    private float CalculateTotalProgress()
    {
        float totalProgress = 0f;
        foreach (var sceneOperation in _scenesToLoad)
        {
            totalProgress += sceneOperation.progress;
        }
        return totalProgress / _scenesToLoad.Count;
    }
}