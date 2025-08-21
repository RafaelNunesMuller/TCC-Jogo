using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class MenuItem : MonoBehaviour
{
    public GameObject itemSlotPrefab;       // Prefab do slot de item
    public Transform itemSlotContainer;     // Container dos slots (pai)
    public RectTransform cursor;             // Cursor que indica o slot selecionado
    public GameObject inventarioPainel;     // Painel do inventário

    [Header("Referências")]
    public GameObject menuPanel; // arrasta o MenuPanel no inspector

    private List<RectTransform> itemSlots = new List<RectTransform>();  // Lista dos slots
    private int cursorIndex = 0;             // Índice do cursor
    private List<Item> itensAtuais = new List<Item>();  // Lista atual dos itens mostrados


    public static List<Item> inventario = new List<Item>();

    void Start()
    {
        // Exemplo de inicialização
        inventario.Add(new Item("Poção de Vida", ItemTipo.Consumivel, 5));
        inventario.Add(new Item("Espada de Ferro", ItemTipo.Arma, 1, null, bonusForca: 5));
        inventario.Add(new Item("Espada Lendária", ItemTipo.Arma, 1, null, bonusForca: 15));
        inventario.Add(new Item("Armadura de Couro", ItemTipo.Armadura, 1, null, bonusDefesa: 3));
        inventario.Add(new Item("Armadura de Aço", ItemTipo.Armadura, 1, null, bonusDefesa: 8));
    }

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
        gameObject.SetActive(false); // fecha o Status
        menuPanel.SetActive(true);        // reabre o menu principal
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
    public void Open(List<Item> itens)
    {
        itensAtuais = itens;  // Guarda a lista atual para uso posterior
        inventarioPainel.SetActive(true);

        // Limpa os slots antigos
        foreach (Transform child in itemSlotContainer)
            Destroy(child.gameObject);

        itemSlots.Clear();

        // Cria 25 slots, preenchendo com os itens disponíveis ou "---" se vazio
        for (int i = 0; i < 30; i++)
        {
            GameObject newSlot = Instantiate(itemSlotPrefab, itemSlotContainer);
            newSlot.transform.localScale = Vector3.one; // garante escala correta

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

    // Move o cursor para o slot selecionado


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

        // Obtém o item
        Item item = itensAtuais[index];

        // Procura o player para aplicar o efeito
        playerStats player = FindFirstObjectByType<playerStats>();
        if (player != null)
            item.Usar(player);

        // Diminui quantidade
        item.quantidade--;

        // Remove se acabou
        if (item.quantidade <= 0)
            itensAtuais.RemoveAt(index);

        // Ajusta índice do cursor para não sair do limite
        if (cursorIndex >= itensAtuais.Count)
            cursorIndex = Mathf.Max(0, itensAtuais.Count - 1);

        // Reabre/atualiza inventário visual
        Open(itensAtuais);

        // Reposiciona cursor
        MoveCursor(cursorIndex);
    }


}
