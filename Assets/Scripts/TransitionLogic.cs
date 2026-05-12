using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionLogic : MonoBehaviour
{
    void Update()
    {
        // Al pulsar cualquier tecla o click, pasamos al nivel de las orugas
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Day1"); 
        }
    }
}