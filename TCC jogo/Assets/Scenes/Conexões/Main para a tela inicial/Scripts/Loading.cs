using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // caso queira usar TextMeshPro

public class LoadingScreenLoop : MonoBehaviour
{
    [Header("Cena")]
    public string cenaParaCarregar = "Fase1";

    [Header("Tempo")]
    public float tempoDeEspera = 3f;          // tempo total da tela de loading
    public float tempoEntreTextos = 0.5f;     // tempo entre mudança de textos

    [Header("Textos de Loading")]
    public TMP_Text targetText;               // arrasta o TMP_Text do Canvas
    public string[] textos;                   // os três textos em loop

    private void Start()
    {
        if (textos == null || textos.Length == 0)
        {
            Debug.LogWarning("Nenhum texto de loading foi atribuído!");
            return;
        }

        if (targetText == null)
        {
            Debug.LogWarning("Nenhum TMP_Text atribuído!");
            return;
        }

        StartCoroutine(LoopTextos());
        StartCoroutine(CarregarCenaDepoisDoTempo());
    }

    IEnumerator LoopTextos()
    {
        int index = 0;

        while (true) // loop infinito até a cena mudar
        {
            targetText.text = textos[index];
            index = (index + 1) % textos.Length;
            yield return new WaitForSeconds(tempoEntreTextos);
        }
    }

    IEnumerator CarregarCenaDepoisDoTempo()
    {
        yield return new WaitForSeconds(tempoDeEspera);
        SceneManager.LoadScene(cenaParaCarregar);
    }
}
