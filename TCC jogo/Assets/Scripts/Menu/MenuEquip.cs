using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuEquip : MonoBehaviour
{
    public playerStats playerStats;

    [Header("UI")]
    public TMP_Text statusText;
    public TMP_Text EquipamentoAtual;
    public Transform equipListParent;
    public GameObject equipItemPrefab;
    public RectTransform cursor;

    private List<Item> inventario = new List<Item>();
    private List<RectTransform> equipSlots = new List<RectTransform>();
    private List<Item> itensAtuais = new List<Item>();
    private int cursorIndex = 0;
    private string slotSelecionado = "";

    void Start()
    {
        // Exemplo de inventário
        inventario.Add(new Item("Poção de Vida", ItemTipo.Consumivel, 5));
        inventario.Add(new Item("Espada de Ferro", ItemTipo.Arma, 1, null, bonusForca: 5));
        inventario.Add(new Item("Espada Lendária", ItemTipo.Arma, 1, null, bonusForca: 15));
        inventario.Add(new Item("Armadura de Couro", ItemTipo.Armadura, 1, null, bonusDefesa: 3));
        inventario.Add(new Item("Armadura de Aço", ItemTipo.Armadura, 1, null, bonusDefesa: 8));

        AtualizarStatus();
        AtualizarEquip();
    }

    void Update()
    {
        if (!gameObject.activeSelf || itensAtuais.Count == 0) return;

        HandleInput();
    }

    public void SelecionarSlot(string slot)
    {
        slotSelecionado = slot; // "Arma" ou "Armadura"
        MostrarListaEquip();
    }

    void MostrarListaEquip()
    {
        // Limpa itens antigos
        foreach (Transform child in equipListParent)
            Destroy(child.gameObject);
        equipSlots.Clear();
        itensAtuais.Clear();

        // Filtra itens do tipo selecionado
        foreach (Item item in inventario)
        {
            if ((slotSelecionado == "Arma" && item.tipo == ItemTipo.Arma) ||
                (slotSelecionado == "Armadura" && item.tipo == ItemTipo.Armadura))
            {
                GameObject obj = Instantiate(equipItemPrefab, equipListParent);
                ItemEquipUI ui = obj.GetComponent<ItemEquipUI>();
                ui.Configurar(item, () => Equipar(item));

                equipSlots.Add(obj.GetComponent<RectTransform>());
                itensAtuais.Add(item);
            }
        }

        cursorIndex = 0;
        MoveCursor(cursorIndex);
    }

    void MoveCursor(int index)
    {
        if (index < 0 || index >= equipSlots.Count) return;
        cursor.anchoredPosition = equipSlots[index].anchoredPosition;
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            cursorIndex = Mathf.Min(cursorIndex + 1, equipSlots.Count - 1);
            MoveCursor(cursorIndex);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            cursorIndex = Mathf.Max(cursorIndex - 1, 0);
            MoveCursor(cursorIndex);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (cursorIndex >= 0 && cursorIndex < itensAtuais.Count)
                Equipar(itensAtuais[cursorIndex]);
        }
    }

    void Equipar(Item item)
    {
        if (slotSelecionado == "Arma")
            playerStats.EquiparArma(item);
        else if (slotSelecionado == "Armadura")
            playerStats.EquiparArmadura(item);

        AtualizarStatus();
        AtualizarEquip();
    }

    void AtualizarStatus()
    {
        statusText.text =
            $"HP: {playerStats.maxHP}\n" +
            $"ATK: {playerStats.strength}\n" +
            $"DEF: {playerStats.defense}";
    }

    void AtualizarEquip()
    {
        EquipamentoAtual.text =
            $"Arma: {playerStats.armaEquipada}\n" +
            $"Armadura: {playerStats.armaduraEquipada}";
    }
}
