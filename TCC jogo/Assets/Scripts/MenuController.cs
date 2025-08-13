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
    public GameObject inventario;


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
            inventario.SetActive(false);

            // Bloqueia ou libera o movimento do player
            playerScript.canMove = !isActive;
        }




        if(Input.GetKeyDown(KeyCode.UpArrow))
{
            currentIndex--;  // diminui
            if (currentIndex < 0)
                currentIndex = options.Length - 1;  // volta para o último
            UpdateCursor();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex++;  // aumenta
            if (currentIndex >= options.Length)
                currentIndex = 0;  // volta para o primeiro
            UpdateCursor();
        }

        if (Input.GetKeyDown(KeyCode.Z)) // Confirmar
        {
            SelectOption();
        }

        if (Input.GetKeyDown(KeyCode.X)) // Confirmar
        {
            menuUI.SetActive(true); // Fecha o menu principal
            playerScript.canMove = false; // Bloqueia movimento
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
                Debug.Log("Abrir EQUIP");
                break;

            case 2:
                Debug.Log("Abrir STATUS");
                break;

            case 3:
                Debug.Log("Abrir SAVE");
                break;

            case 4:
                Debug.Log("Fechar menu");
                menuUI.SetActive(false);
                playerScript.canMove = true;
                break;
        }
    }

}
