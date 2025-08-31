using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventarioUI : MonoBehaviour
{
    public GameObject itemSlotPrefab; // Prefab do slot (um objeto com TMP_Text e opcionalmente Image)
    public Transform content;         // Onde os slots vão aparecer (pai com Vertical Layout Group)
    public RectTransform cursor;      // Cursor gráfico (uma seta ou highlight)

    public GameObject menuPrincipal;  // Referência ao Menu (Atacar / Item / Fugir)
    public GameObject inventarioUI;   // Este painel de inventário

    private List<GameObject> slots = new List<GameObject>();
    private int indexSelecionado = 0;

    void OnEnable()
    {
        AtualizarInventario();
        if (slots.Count > 0)
            AtualizarCursor();
    }

    void Update()
    {
        if (slots.Count == 0) return;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            indexSelecionado = (indexSelecionado + 1) % slots.Count;
            AtualizarCursor();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            indexSelecionado--;
            if (indexSelecionado < 0) indexSelecionado = slots.Count - 1;
            AtualizarCursor();
        }

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            UsarItem(indexSelecionado);
        }

        // tecla X para voltar sem usar nada
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Escape))
        {
            VoltarMenuPrincipal();
        }
    }

    public void AtualizarInventario()
    {
        // limpa slots antigos
        foreach (Transform child in content)
            Destroy(child.gameObject);

        slots.Clear();

        // cria slots novos
        for (int i = 0; i < Inventario.instance.itens.Count; i++)
        {
            Item item = Inventario.instance.itens[i];

            GameObject slot = Instantiate(itemSlotPrefab, content);

            // nome + quantidade
            TMP_Text txt = slot.GetComponentInChildren<TMP_Text>();
            if (txt != null)
                txt.text = $"{item.nome} x{item.quantidade}";

            // ícone (se houver)
            Image img = slot.GetComponentInChildren<Image>();
            if (img != null && item.icone != null)
                img.sprite = item.icone;

            slots.Add(slot);
        }

        // reseta seleção
        indexSelecionado = 0;
    }

    void AtualizarCursor()
    {
        if (slots.Count == 0) return;

        // coloca o cursor como filho do slot selecionado
        cursor.SetParent(slots[indexSelecionado].transform, false);

        // ajusta posição (um pouco à esquerda do texto)
        cursor.anchoredPosition = new Vector2(-50, 0);
    }

    void UsarItem(int index)
    {
        playerStats player = FindFirstObjectByType<playerStats>();
        if (player == null) return;

        Item item = Inventario.instance.itens[index];
        item.Usar(player);

        item.quantidade--;
        if (item.quantidade <= 0)
        {
            Inventario.instance.itens.RemoveAt(index);
        }

        AtualizarInventario();

        // se não sobrou nenhum item → fecha direto
        if (Inventario.instance.itens.Count == 0)
        {
            VoltarMenuPrincipal();
        }
        else
        {
            // fecha inventário mesmo que ainda tenha itens (como RPGs tradicionais)
            VoltarMenuPrincipal();
        }
    }

    void VoltarMenuPrincipal()
    {
        inventarioUI.SetActive(false);
        if (menuPrincipal != null)
            menuPrincipal.SetActive(true);
    }
}
