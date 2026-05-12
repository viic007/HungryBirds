using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Day1");
    }

    public void ExitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
