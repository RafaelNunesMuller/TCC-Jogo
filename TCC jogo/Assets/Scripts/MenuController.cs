using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public RectTransform cursor;
    public RectTransform[] options;
    private int currentIndex = 0;
    public GameObject menuUI;
    public Player playerScript;
    public GameObject MenuPanel;

    void Start()
    {
        UpdateCursor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // ou ESC, ou outro botão
        {
            menuUI.SetActive(!menuUI.activeSelf); // ativa/desativa
        }


        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex = (currentIndex + 1) % options.Length;
            UpdateCursor();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex = (currentIndex - 1 + options.Length) % options.Length;
            UpdateCursor();
        }

        if (Input.GetKeyDown(KeyCode.Z)) // Confirmar
        {
            SelectOption();
        }
        
    }

    void UpdateCursor()
    {
        cursor.SetParent(options[currentIndex], false);
        cursor.anchoredPosition = new Vector2(200, 0); // esquerda
    }

    void SelectOption()
    {
        switch (currentIndex)
        {
            case 0:
                Debug.Log("Abrir ITEM");
                break;
            case 1:
                Debug.Log("Abrir MAGIC");
                break;
            case 2:
                Debug.Log("Abrir EQUIP");
                break;
            case 3:
                Debug.Log("Abrir STATUS");
                break;
            case 4:
                Debug.Log("Abrir SAVE");
                break;
            case 5:
                Debug.Log("Fechar menu");
                gameObject.SetActive(false);
                break;
        }
    }
}
