using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuItem : MonoBehaviour
{
    public GameObject itemSlotPrefab;       
    public Transform itemSlotContainer;    
    public RectTransform cursor;           
    public GameObject inventarioPainel;  

    public GameObject menuPanel;

    private List<RectTransform> itemSlots = new List<RectTransform>();
    private int cursorIndex = 0;
    private List<Item> itensAtuais = new List<Item>();

    void Update()
    {
        if (!inventarioPainel.activeSelf) return; 

        HandleInput();

        if (Input.GetKeyDown(KeyCode.X))
        {
            VoltarParaMenu();
        }
    }

    public void VoltarParaMenu()
    {
        gameObject.SetActive(false);
        menuPanel.SetActive(true);
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            cursorIndex = Mathf.Min(cursorIndex + 1, itemSlots.Count - 1);
            MoveCursor(cursorIndex);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            cursorIndex = Mathf.Max(cursorIndex - 1, 0);
            MoveCursor(cursorIndex);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            UseItem(cursorIndex);
        }
    }

    public void Open()
    {


        itensAtuais = new List<Item>(Inventario.instance.itens);
        inventarioPainel.SetActive(true);

    
        foreach (Transform child in itemSlotContainer)
            Destroy(child.gameObject);

        itemSlots.Clear();


        for (int i = 0; i < 30; i++)
        {



            GameObject newSlot = Instantiate(itemSlotPrefab, itemSlotContainer);
            newSlot.transform.localScale = Vector3.one;

            Text text = newSlot.GetComponentInChildren<Text>();
            if (i < itensAtuais.Count)
                text.text = itensAtuais[i].nome + " x" + itensAtuais[i].quantidade;
            else
                text.text = "---";

            itemSlots.Add(newSlot.GetComponent<RectTransform>());
        }

        cursorIndex = 0;
        MoveCursor(cursorIndex);


    }

    void MoveCursor(int index)
    {
        if (index < 0 || index >= itemSlots.Count) return;

        RectTransform targetSlot = itemSlots[index];
        cursor.anchoredPosition = targetSlot.anchoredPosition;
    }

    public void UseItem(int index)
    {
        if (index < 0 || index >= itensAtuais.Count)
            return;

        Item item = itensAtuais[index];

        if (item.tipo != ItemTipo.Consumivel)
        {
            MessageUI.instance.ShowMessage($"{item.nome} só pode ser equipado no menu de Equipar!");
            return;
        }


        playerStats player = FindFirstObjectByType<playerStats>();
        if (player != null)
        {
            Inventario.instance.Usar(item, player);
        }

        Open();
        MoveCursor(cursorIndex);
    }

}
