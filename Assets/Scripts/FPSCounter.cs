using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public TMP_Text fpsText;
    private float deltaTime = 0.0f;

    private void Update()
    {
        // Calculate frames per second
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        int roundedFPS = Mathf.RoundToInt(fps);

        // Update the TextMesh Pro Text component with the FPS
        fpsText.text = "FPS: " + roundedFPS;
    }
}