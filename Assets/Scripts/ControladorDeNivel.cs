using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorDeNivel : MonoBehaviour
{
    public float tiempoDeNivel = 60f; // El día dura 60 segundos

    void Update()
    {
        tiempoDeNivel -= Time.deltaTime;

        if (tiempoDeNivel <= 0)
        {
            // Carga la escena de transición del Día 2
            SceneManager.LoadScene("Day2");
        }
    }
}
