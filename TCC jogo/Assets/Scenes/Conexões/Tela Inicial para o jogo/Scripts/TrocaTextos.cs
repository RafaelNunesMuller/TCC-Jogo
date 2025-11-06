// TextCyclerObjects.cs
using UnityEngine;

public class TextCyclerObjects : MonoBehaviour
{


    public GameObject[] textObjects; 

    public bool loop = false;

    int index = 0;
    bool finished = false;

    void Start()
    {
        if (textObjects == null || textObjects.Length == 0)
        {
            return;
        }

        for (int i = 0; i < textObjects.Length; i++)
            textObjects[i].SetActive(i == 0);
    }

    void Update()
    {
        if (finished) return;

        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            ShowNext();
        }
    }

    public void ShowNext()
    {
        if (textObjects == null || textObjects.Length == 0) return;

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
                finished = true;
                return;
            }
        }

        textObjects[index].SetActive(true);

        
    }
}
