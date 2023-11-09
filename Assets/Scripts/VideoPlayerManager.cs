 using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoPlayerManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] videoClips;
    public Button nextButton;
    public Button prevButton;
    public Button changeSceneButton;
    public Button finishButton;
    public RawImage fadeImage;
    public string PersistentGameplay;

    private int currentClipIndex = 0;
    private bool isPlaying = false;
    private bool transitioning = false;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private string _sceneToReloadName;
    [SerializeField] private SceneField[] _scenesToLoad;
    [SerializeField] private SceneField[] _scenesToUnload;

    private bool loadingScenes = false;
    private GameObject objectToDeactivate;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;
        videoPlayer.loopPointReached += OnVideoEnd;
        PlayCurrentClip();
        UpdateButtonVisibility();
       objectToDeactivate = GameObject.Find(PersistentGameplay);

        // Check if the GameObject was found
        if (objectToDeactivate != null)
        {
            // Deactivate the GameObject
            objectToDeactivate.SetActive(false);
        }
        else
        {
            Debug.LogError("GameObject with the name " + PersistentGameplay + " not found.");
        }
        _sceneToReloadName = SceneManager.GetActiveScene().name;
    }
    public void OnFinishCutscene()
    {
        if (!loadingScenes)
        {
            objectToDeactivate.SetActive(true);
           LoadScenesAsync();
        }
        
        
    }

    private void LoadScenesAsync()
    {
        loadingScenes = true;

        foreach (var sceneToLoad in _scenesToLoad)
        {
            if (!SceneManager.GetSceneByName(sceneToLoad.SceneName).isLoaded)
            {
                SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
                //operation.allowSceneActivation = true; // Evita que la escena se active automáticamente

                //while (!operation.isDone)
                //{
                //    float progress = Mathf.Clamp01(operation.progress / 0.9f); // Limita el progreso a 0-1
                //    Debug.Log("Cargando escena " + sceneToLoad.SceneName + " - Progreso: " + (progress * 100) + "%");

                 //   if (progress >= 0.9f)
                // //   {
                //        operation.allowSceneActivation = true; // Activa la escena cuando esté casi cargada
                //    }

                   // Espera un frame antes de la siguiente iteración
                //}
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

   

    private void UpdateButtonVisibility()
    {
        nextButton.interactable = currentClipIndex < videoClips.Length - 1;
        prevButton.gameObject.SetActive(currentClipIndex > 0);
        changeSceneButton.gameObject.SetActive(currentClipIndex == videoClips.Length - 1);
        finishButton.gameObject.SetActive(currentClipIndex == videoClips.Length - 1);
    }

    private void PlayCurrentClip()
    {
        videoPlayer.clip = videoClips[currentClipIndex];
        videoPlayer.Play();
        isPlaying = true;
        transitioning = false;
    }

    public void PlayNextClip()
    {
        if (currentClipIndex < videoClips.Length - 1 && !transitioning)
        {
            currentClipIndex++;
            transitioning = true;
            StartCoroutine(TransitionAndPlay());
        }
    }

    public void PlayPreviousClip()
    {
        if (currentClipIndex > 0 && !transitioning)
        {
            currentClipIndex--;
            transitioning = true;
            StartCoroutine(TransitionAndPlay());
        }
    }

    public void ChangeScene()
    {
        currentClipIndex = 0;
        transitioning = true;
            StartCoroutine(TransitionAndPlay());
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        isPlaying = false;
    }

    private IEnumerator TransitionAndPlay()
    {
        // Realiza la transición de fade blanco.
         // Ajusta la duración de la transición según tus necesidades.

        Color initialColor = new Color(1f, 1f, 1f, 0f);
        Color targetColor = new Color(1f, 1f, 1f, 1f);

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            ChangeColorAlpha(fadeImage, Color.Lerp(initialColor, targetColor, timer / duration));
            yield return null;
        }

        PlayCurrentClip();
        UpdateButtonVisibility();

        timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            ChangeColorAlpha(fadeImage, Color.Lerp(targetColor, initialColor, timer / duration));
            yield return null;
        }

        transitioning = false;
    }

    private void ChangeColorAlpha(RawImage image, Color newColor)
    {
        image.color = newColor;
    }
}