//using CI.QuickSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainmenu;
    [SerializeField] private GameObject _startgamebutton;
    [SerializeField] private GameObject _persistantGameplay;
    public string PersistentGameplay;

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
    [Header("Default New Games Values")]
    [SerializeField] private Vector3 _defaultpos = new Vector3(12.32f, 50f, -720.7f);

    private List<AsyncOperation> _scenesToLoad = new List<AsyncOperation>();

    private void Start()
    {
        //_persistantGameplay = GameObject.FindWithTag("PG");
         //Debug.LogError(_persistantGameplay);
        _loadingBarObject.SetActive(false);
        //_persistantGameplay.SetActive(false);
        ShowMenu();
        Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;
    }
    public void StartGame()
    {
        
        _startgamebutton.SetActive(false);
        _mainmenu.SetActive(true);
    }

    // Update is called once per frame
    public void Continue()
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
        _persistantGameplay.SetActive(true);
    }

    public void NewGame()
    {
        // Hide the menu immediately
        /*QuickSaveWriter.Create("Inputs")
                       .Write("Input1", new Vector3(7f, -50f, -78f))
                       .Write("Input2", new Quaternion(0f, -96f, -0f, 0))
					   .Write("Input3", new Vector3(7f, -50f, -78f))
                       .Write("Input4", new Quaternion(0f, 286f, -0f, 0))
					   .Write("Input5", new Vector3(-12f, 26f, -0.8f))
                       .Write("Input7", new Quaternion(29f, 26f, 0.8f, 0))
                       .Write("Input6", false)
                       .Write("Scene1", true)
                       .Write("Cash", 20)
                       .Commit();
        */
        
        
        
        HideMenu();

        // Load scenes asynchronously
        Time.timeScale = 0f;
        //_scenesToLoad.Add(SceneManager.LoadSceneAsync(_persistentGameplay));
        _scenesToLoad.Add(SceneManager.LoadSceneAsync(_levelScene, LoadSceneMode.Additive));
        _scenesToLoad.Add(SceneManager.LoadSceneAsync(_levelSceneExtra, LoadSceneMode.Additive));

        // Unload scenes asynchronously
        SceneManager.UnloadSceneAsync(_scenesToUnload);

        // Start a coroutine to check loading progress
        StartCoroutine(ProgressLoadingBar());
        //_persistantGameplay.SetActive(true);
    }

    private void HideMenu()
    {
        for (int i = 0; i < _ObjectsToHide.Length; i++)
        {
            _ObjectsToHide[i].SetActive(false);
        }
    }
    private void ShowMenu()
    {
        for (int i = 0; i < _ObjectsToHide.Length; i++)
        {
            _ObjectsToHide[i].SetActive(true);
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

    public void Exit(){
		Application.Quit();
	}
}