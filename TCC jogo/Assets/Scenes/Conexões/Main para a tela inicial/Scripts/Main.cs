using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class Main : MonoBehaviour
{
    public string cenaParaCarregar = "Loading";

    public float tempoDeEspera = 3f;         
    public float tempoEntreTextos = 0.5f;     

    public TMP_Text targetText;               
    public string[] textos;                  

    private void Start()
    {
        if (textos == null || textos.Length == 0)
        {
            return;
        }

        if (targetText == null)
        {
            return;
        }

        StartCoroutine(LoopTextos());
        StartCoroutine(CarregarCenaDepoisDoTempo());
    }

    IEnumerator LoopTextos()
    {
        int index = 0;

        while (true) 
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

