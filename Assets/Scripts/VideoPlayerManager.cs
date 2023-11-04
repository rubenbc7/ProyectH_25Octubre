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
    public RawImage fadeImage;

    private int currentClipIndex = 0;
    private bool isPlaying = false;
    private bool transitioning = false;
    [SerializeField] private float duration = 0.5f;

    private void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
        PlayCurrentClip();
        UpdateButtonVisibility();
    }

    private void UpdateButtonVisibility()
    {
        nextButton.interactable = currentClipIndex < videoClips.Length - 1;
        prevButton.gameObject.SetActive(currentClipIndex > 0);
        changeSceneButton.gameObject.SetActive(currentClipIndex == videoClips.Length - 1);
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
        if (currentClipIndex == videoClips.Length - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Cambia "OtraEscena" al nombre de tu escena.
        }
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