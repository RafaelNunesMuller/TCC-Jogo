// TextCyclerObjects.cs
using UnityEngine;

public class TextCyclerObjects : MonoBehaviour
{

    [Tooltip("Arraste os 3 (ou mais) GameObjects de texto na ordem.")]
    public GameObject[] textObjects; // coloca 3 aqui no Inspector

    [Tooltip("Se true, volta pro primeiro texto depois do �ltimo.")]
    public bool loop = false;

    int index = 0;
    bool finished = false;

    void Start()
    {
        if (textObjects == null || textObjects.Length == 0)
        {
            Debug.LogWarning("TextCyclerObjects: nenhum texto atribu�do.");
            return;
        }

        // Ativa s� o primeiro no come�o
        for (int i = 0; i < textObjects.Length; i++)
            textObjects[i].SetActive(i == 0);
    }

    void Update()
    {
        // Detecta clique do mouse ou toque no in�cio do frame
        if (finished) return;

        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            ShowNext();
        }
    }

    public void ShowNext()
    {
        if (textObjects == null || textObjects.Length == 0) return;

        // desativa o atual
        textObjects[index].SetActive(false);

        index++;

        if (index >= textObjects.Length)
        {
            if (loop)
            {
                index = 0;
            }
            else
            {
                finished = true; // para depois do �ltimo
                return;
            }
        }

        // ativa o pr�ximo
        textObjects[index].SetActive(true);

        
    }
}
