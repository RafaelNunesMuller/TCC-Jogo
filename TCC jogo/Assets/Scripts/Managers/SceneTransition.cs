using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Header("Configuração da Transição")]
    public string cenaDestino;
    public string pontoDeEntradaDestino;

    private bool podeEntrar = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            podeEntrar = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            podeEntrar = false;
    }

    void Update()
    {
        if (podeEntrar && Input.GetKeyDown(KeyCode.Z))
        {
            GameManager.Instance.pontoDeEntrada = pontoDeEntradaDestino;
            GameManager.Instance.lastScene = SceneManager.GetActiveScene().name;
            GameManager.Instance.lastPlayerPosition = transform.position;

            SceneManager.LoadScene(cenaDestino);
        }
    }
}
