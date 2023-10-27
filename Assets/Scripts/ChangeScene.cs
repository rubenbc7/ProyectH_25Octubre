using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    
    [SerializeField] private SceneField _sceneToLoad;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Asegúrate de que el jugador tenga un tag "Player".
        {
            CargarEscena();
            Debug.Log("Cambiadeescenaalv)");
        }
    }

    private void CargarEscena()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }
}
