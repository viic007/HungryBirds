using UnityEngine;

public class MenuVictoria : MonoBehaviour
{
    public void SalirDelJuego()
    {
        Debug.Log("¡Botón Exit pulsado! Cerrando el juego...");

        Application.Quit();

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}