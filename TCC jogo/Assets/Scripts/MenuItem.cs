using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuItem : MonoBehaviour
{
    public GameObject itemSlotPrefab;       // Prefab do slot de item
    public Transform itemSlotContainer;     // Container dos slots (pai)
    public RectTransform cursor;             // Cursor que indica o slot selecionado
    public GameObject inventarioPainel;     // Painel do inventário

    private List<RectTransform> itemSlots = new List<RectTransform>();  // Lista dos slots
    private int cursorIndex = 0;             // Índice do cursor
    private List<Item> itensAtuais = new List<Item>();  // Lista atual dos itens mostrados

    void Update()
    {
        if (!inventarioPainel.activeSelf) return; // Só responde se o inventário estiver aberto

        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            cursorIndex = Mathf.Min(cursorIndex + 1, itemSlots.Count - 1);
            MoveCursor(cursorIndex);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            cursorIndex = Mathf.Max(cursorIndex - 1, 0);
            MoveCursor(cursorIndex);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            inventarioPainel.SetActive(false);
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
        for (int i = 0; i < 25; i++)
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

    // Move o cursor para o slot selecionado
    void MoveCursor(int index)
    {
        // Faz o cursor filho do slot para se posicionar junto
        cursor.SetParent(itemSlots[index]);
        cursor.anchoredPosition = new Vector2(-40f, 0f);
        cursor.localScale = Vector3.one;  // Reset escala, importante!
    }

    // Usa o item selecionado
    void UseItem(int index)
    {
        if (index < itensAtuais.Count)
        {
            Item item = itensAtuais[index];
            item.quantidade--;

            Debug.Log("Usou: " + item.nome);

            if (item.quantidade <= 0)
            {
                itensAtuais.RemoveAt(index);
            }

            // Ajusta o cursor para não sair do intervalo válido
            if (cursorIndex >= itensAtuais.Count)
                cursorIndex = Mathf.Max(0, itensAtuais.Count - 1);

            Open(itensAtuais);
            MoveCursor(cursorIndex);
        }
    }
}
