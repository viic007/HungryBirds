using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuGameOver : MonoBehaviour
{
    public void VolverAlInicio()
    {
        Debug.Log("Volviendo al menú de inicio...");

        SceneManager.LoadScene("Menu");
    }

    public void SalirDelJuego()
    {
        Debug.Log("¡Botón Exit (GameOver) pulsado! Cerrando el juego...");
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
