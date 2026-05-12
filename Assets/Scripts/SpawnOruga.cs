using UnityEngine;



public class SpawnOruga : MonoBehaviour
{
    public GameObject orugaPrefab;
    public Transform[] todasLasPlantas; // Arrastra las 8 plantas 
    public Transform[] puntosDeEntrada; // Arrastra los pts de entrada

    void Start()
    {
        InvokeRepeating("GenerarEnemigo", 2f, 10f);
    }

    void GenerarEnemigo()
    {
        int indiceEntrada = Random.Range(0, puntosDeEntrada.Length);
        Transform dondeNace = puntosDeEntrada[indiceEntrada];

        // 2. Tirar el dado para la PLANTA (Esto es lo que te fallaba)
        int indicePlanta = Random.Range(0, todasLasPlantas.Length);
        Transform plantaDestino = todasLasPlantas[indicePlanta];

        // 3. Crear la oruga
        GameObject nuevaOruga = Instantiate(orugaPrefab, dondeNace.position, Quaternion.identity);

        // 4. DARLE el destino a esa oruga específica
        nuevaOruga.GetComponent<OrugaAI>().targetPlant = plantaDestino;
    }


}
        
 