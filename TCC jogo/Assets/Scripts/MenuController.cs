using System.Collections.Generic;
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
    public static Inventario instance;
    public MenuItem menuItem;


    void Start()
    {
        UpdateCursor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            bool isActive = !menuUI.activeSelf;
            menuUI.SetActive(isActive);

            // Bloqueia ou libera o movimento do player
            playerScript.canMove = !isActive;
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
                menuUI.SetActive(false); // Fecha o menu principal
                playerScript.canMove = false; // Bloqueia movimento
                menuItem.Open(Inventario.instance.itens); // Abre inventário
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
                menuUI.SetActive(false);
                playerScript.canMove = true;
                break;
        }
    }

}
