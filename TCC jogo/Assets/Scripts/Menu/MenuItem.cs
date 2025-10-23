using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuItem : MonoBehaviour
{
    public GameObject itemSlotPrefab;       // Prefab do slot de item
    public Transform itemSlotContainer;     // Container dos slots (pai)
    public RectTransform cursor;            // Cursor que indica o slot selecionado
    public GameObject inventarioPainel;     // Painel do inventário

    [Header("Referências")]
    public GameObject menuPanel; // arrasta o MenuPanel no inspector

    private List<RectTransform> itemSlots = new List<RectTransform>();
    private int cursorIndex = 0;
    private List<Item> itensAtuais = new List<Item>();

    void Update()
    {
        if (!inventarioPainel.activeSelf) return; // Só responde se o inventário estiver aberto

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

    // Abre o inventário e cria/atualiza os slots dos itens
    public void Open()
    {


        itensAtuais = new List<Item>(Inventario.instance.itens);  // sempre pega do Inventario central
        inventarioPainel.SetActive(true);

        // Limpa os slots antigos
        foreach (Transform child in itemSlotContainer)
            Destroy(child.gameObject);

        itemSlots.Clear();

        // Cria até 30 slots
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

    // Usa o item selecionado
    public void UseItem(int index)
    {
        if (index < 0 || index >= itensAtuais.Count)
            return;

        Item item = itensAtuais[index];

        // 🔹 Bloqueia uso de itens que não são consumíveis
        if (item.tipo != ItemTipo.Consumivel)
        {
            Debug.Log($"{item.nome} não pode ser usado aqui. Vá até o menu de Equipar!");
            MessageUI.instance.ShowMessage($"{item.nome} só pode ser equipado no menu de Equipar!");
            return;
        }

        // 🔹 Procura o player e usa o item
        playerStats player = FindFirstObjectByType<playerStats>();
        if (player != null)
        {
            Inventario.instance.Usar(item, player);
            Debug.Log($"{item.nome} foi usado!");
        }

        // 🔹 Atualiza interface
        Open();
        MoveCursor(cursorIndex);
    }

}
