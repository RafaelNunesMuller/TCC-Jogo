using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEntrance : MonoBehaviour
{
    public string entradaID; // ex: "SaidaDungeon", "PortaLoja"

    void Start()
    {
        // Se este é o ponto de entrada correto, move o player pra cá
        if (GameManager.Instance != null && GameManager.Instance.pontoDeEntrada == entradaID)
        {
            var player = FindAnyObjectByType<Player>();
            if (player != null)
            {
                player.transform.position = transform.position;
                Debug.Log($"➡ Player posicionado no ponto de entrada: {entradaID}");
            }
        }
    }
}
