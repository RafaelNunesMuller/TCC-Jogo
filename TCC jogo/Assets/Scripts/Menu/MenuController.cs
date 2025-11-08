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
    public GameObject MenuEquip;
    public GameObject MenuStatus;


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
            MenuEquip.SetActive(false);

            playerScript.canMove = !isActive;
        }




        if(Input.GetKeyDown(KeyCode.UpArrow))
{
            currentIndex--;
            if (currentIndex < 0)
                currentIndex = options.Length - 1;
            UpdateCursor();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex++;
            if (currentIndex >= options.Length)
                currentIndex = 0;
            UpdateCursor();
        }

        if (menuUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                SelectOption();
            }
        }

    }



    void UpdateCursor()
    {
        cursor.SetParent(options[currentIndex], false);
        cursor.anchoredPosition = new Vector2(200, 0);
    }
    


    void SelectOption()
    {
        switch (currentIndex)
        {
            case 0:
                menuUI.SetActive(false);
                playerScript.canMove = false;
                menuItem.Open();
                break;

            case 1:
                menuUI.SetActive(false);
                playerScript.canMove = false; 
                MenuEquip.SetActive(true);
                break;

            case 2:
                menuUI.SetActive(false);
                playerScript.canMove = false;
                MenuStatus.SetActive(true);
                break;

            case 3:
                break;

            case 4:
                menuUI.SetActive(false);
                playerScript.canMove = true;
                break;
        }
    }

}
