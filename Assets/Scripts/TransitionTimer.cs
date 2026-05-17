using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TransitionTimer : MonoBehaviour
{
    [Header("Configuración de la Transición")]
    [Tooltip("Tiempo en segundos que se mostrará este cartel antes de avanzar.")]
    public float tiempoDeEspera = 3f;

    [Tooltip("Lvl3")]
    public string nombreSiguienteEscena;

    void Start()
    {
        StartCoroutine(CuentaAtrasParaCambiar());
    }

    IEnumerator CuentaAtrasParaCambiar()
    {
        yield return new WaitForSeconds(tiempoDeEspera);

        if (!string.IsNullOrEmpty(nombreSiguienteEscena))
        {
            SceneManager.LoadScene(nombreSiguienteEscena);
        }
        else
        {
            Debug.LogError("¡Error! No has puesto el nombre de la siguiente escena en el Inspector de " + gameObject.name);
        }
    }
}