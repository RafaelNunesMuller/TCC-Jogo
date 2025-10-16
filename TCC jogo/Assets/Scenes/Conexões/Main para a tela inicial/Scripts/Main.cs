using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // caso queira usar TextMeshPro

public class Main : MonoBehaviour
{
    [Header("Cena")]
    public string cenaParaCarregar = "Loading";

    [Header("Tempo")]
    public float tempoDeEspera = 3f;          // tempo total da tela de loading
    public float tempoEntreTextos = 0.5f;     // tempo entre mudan�a de textos

    [Header("Textos de Loading")]
    public TMP_Text targetText;               // arrasta o TMP_Text do Canvas
    public string[] textos;                   // os tr�s textos em loop

    private void Start()
    {
        if (textos == null || textos.Length == 0)
        {
            Debug.LogWarning("Nenhum texto de loading foi atribu�do!");
            return;
        }

        if (targetText == null)
        {
            Debug.LogWarning("Nenhum TMP_Text atribu�do!");
            return;
        }

        StartCoroutine(LoopTextos());
        StartCoroutine(CarregarCenaDepoisDoTempo());
    }

    IEnumerator LoopTextos()
    {
        int index = 0;

        while (true) // loop infinito at� a cena mudar
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

