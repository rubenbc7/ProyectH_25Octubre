using UnityEngine;

public class FixedTimeStep : MonoBehaviour
{
    private void Awake()
    {
        Time.fixedDeltaTime = 1f / 60f;
    }
}
