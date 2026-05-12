using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionTimer : MonoBehaviour
{
    void Start()
    {
        // Espera 3 segundos y ejecuta la función "CargarNivel"
        Invoke("CargarNivel", 3f);
    }

    void CargarNivel()
    {
        // Carga la escena del nivel 
        SceneManager.LoadScene(2);
    }
}
