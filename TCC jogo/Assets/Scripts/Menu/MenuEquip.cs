using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuEquip : MonoBehaviour
{
    [Header("Referências Principais")]
    public playerStats playerStats;
    public Inventario inventarioCentral;

    public TMP_Text EquipamentoAtual;
    public Transform equipListParent;
    public GameObject equipItemPrefab;
    public RectTransform cursor;

    public Button weaponButton;
    public Button healmetButton;
    public Button gloveButton;
    public Button armorButton;
    public Button accessoryButton;

    public GameObject menuPanel;

    private List<Item> inventario = new List<Item>();
    private List<RectTransform> equipSlots = new List<RectTransform>();
    private List<Item> itensAtuais = new List<Item>();
    private int cursorIndex = 0;
    private string slotSelecionado = "";

    private bool navegandoSlots = true; 
    private List<RectTransform> botoesSlots = new List<RectTransform>();

    private int cursorIndex = 0;
    private string slotSelecionado = "";
    private bool navegandoSlots = true;

    // salva o último slot selecionado (para restaurar se voltar)
    private string ultimoSlotSelecionado = "";

    void Awake()
    {
        // 🔹 Garante que o GameManager tenha a referência
        if (GameManager.Instance != null && GameManager.Instance.playerStats != null)
            playerStats = GameManager.Instance.playerStats;
    }

    void Start()
    {
        inventarioCentral = Inventario.instance;

        botoesSlots.Add(weaponButton.GetComponent<RectTransform>());
        botoesSlots.Add(healmetButton.GetComponent<RectTransform>());
        botoesSlots.Add(gloveButton.GetComponent<RectTransform>());
        botoesSlots.Add(armorButton.GetComponent<RectTransform>());
        botoesSlots.Add(accessoryButton.GetComponent<RectTransform>());

        // 🔹 Corrige prefab se tiver sido destruído
        if (equipItemPrefab == null)
        {
            equipItemPrefab = Resources.Load<GameObject>("UI/EquipItemPrefab");
            if (equipItemPrefab == null)
                Debug.LogError("❌ EquipItemPrefab não encontrado em Resources/UI/");
        }

        cursorIndex = 0;
        MoveCursor(cursorIndex);
        AtualizarEquip();
    }

    void OnEnable()
    {
        RestaurarReferencias();
        AtualizarEquip();

        // 🔹 se tinha um slot antes, reabre nele
        if (!string.IsNullOrEmpty(ultimoSlotSelecionado))
            SelecionarSlot(ultimoSlotSelecionado);
    }

    private void RestaurarReferencias()
    {
        if (playerStats == null && GameManager.Instance != null)
            playerStats = GameManager.Instance.playerStats;

        if (inventarioCentral == null)
            inventarioCentral = Inventario.instance;

        if (equipItemPrefab == null)
            equipItemPrefab = Resources.Load<GameObject>("UI/EquipItemPrefab");
    }

    void Update()
    {
        if (!gameObject.activeSelf) return;

        HandleInput();

        if (Input.GetKeyDown(KeyCode.X))
            VoltarParaMenu();
        }
    }
    public void VoltarParaMenu()
    {
        gameObject.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void VoltarParaMenu()
    {
        ultimoSlotSelecionado = slotSelecionado; // salva estado
        gameObject.SetActive(false);
        menuPanel.SetActive(true);
    }

    // =============================
    // CONTROLE DE INPUT
    // =============================
    void HandleInput()
    {
        if (navegandoSlots)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                cursorIndex = Mathf.Min(cursorIndex + 1, botoesSlots.Count - 1);
                MoveCursor(cursorIndex);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                cursorIndex = Mathf.Max(cursorIndex - 1, 0);
                MoveCursor(cursorIndex);
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (cursorIndex == 0) SelecionarSlot("Arma");
                else if (cursorIndex == 1) SelecionarSlot("Elmo");
                else if (cursorIndex == 2) SelecionarSlot("Luva");
                else if (cursorIndex == 3) SelecionarSlot("Armadura");
                else if (cursorIndex == 4) SelecionarSlot("Acessorio");
            }
        }
        else
        {
            if (equipSlots.Count == 0) return;

            equipSlots.RemoveAll(slot => slot == null);

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                cursorIndex = Mathf.Min(cursorIndex + 1, equipSlots.Count - 1);
                MoveCursor(cursorIndex);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                cursorIndex = Mathf.Max(cursorIndex - 1, 0);
                MoveCursor(cursorIndex);
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (cursorIndex >= 0 && cursorIndex < itensAtuais.Count && itensAtuais[cursorIndex] != null)
                    Equipar(itensAtuais[cursorIndex]);
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                navegandoSlots = true;
                cursorIndex = 0;
                MoveCursor(cursorIndex);
            }
        }
    }

    // =============================
    // SELEÇÃO E EXIBIÇÃO DE ITENS
    // =============================
    public void SelecionarSlot(string slot)
    {
        slotSelecionado = slot;
        ultimoSlotSelecionado = slot;
        MostrarListaEquip();
    }

    void MostrarListaEquip()
{
    // limpa lista anterior
    if (equipListParent.childCount > 1)
{
    Destroy(equipListParent.GetChild(0).gameObject);
    equipSlots.RemoveAt(0);
    itensAtuais.RemoveAt(0);
}


    
    if (inventarioCentral == null)
        inventarioCentral = Inventario.instance;

    inventario = inventarioCentral.itens;

    foreach (Item item in inventario)
    {
        if ((slotSelecionado == "Arma" && item.tipo == ItemTipo.Arma) ||
            (slotSelecionado == "Armadura" && item.tipo == ItemTipo.Armadura) ||
            (slotSelecionado == "Acessorio" && item.tipo == ItemTipo.Acessorio) ||
            (slotSelecionado == "Elmo" && item.tipo == ItemTipo.Elmo) ||
            (slotSelecionado == "Luva" && item.tipo == ItemTipo.Luva))
        {
            // cria novo item UI a partir do prefab real
            GameObject obj = Instantiate(equipItemPrefab, equipListParent);
            ItemEquipUI ui = obj.GetComponent<ItemEquipUI>();

            // configura ícone e callback
            ui.Configurar(item, () => Equipar(item));

            // adiciona na lista de navegação
            equipSlots.Add(obj.GetComponent<RectTransform>());
            itensAtuais.Add(item);
        }
    }

    cursorIndex = 0;
    navegandoSlots = false;

    if (equipSlots.Count > 0)
        MoveCursor(cursorIndex);
    else
        cursor.gameObject.SetActive(false);
}


    // =============================
    // MOVIMENTO DO CURSOR
    // =============================
    void MoveCursor(int index)
    {
        Vector2 offset = new Vector2(1250f, -600f); 

        if (navegandoSlots)
        {
            if (index < 0 || index >= botoesSlots.Count) return;
            cursor.gameObject.SetActive(true);
            cursor.anchoredPosition = botoesSlots[index].anchoredPosition + offset;
        }
        else
        {
            if (equipSlots.Count == 0) return;
            if (index < 0 || index >= equipSlots.Count) return;

            RectTransform targetSlot = equipSlots[index];
            if (targetSlot != null)
            {
                cursor.gameObject.SetActive(true);
                cursor.anchoredPosition = targetSlot.anchoredPosition + offset;
            }
        }
    }

    // =============================
    // EQUIPAR ITEM
    // =============================
    void Equipar(Item item)
    {
        if (item == null || playerStats == null) return;

        switch (slotSelecionado)
        {
            case "Arma": playerStats.EquiparArma(item); break;
            case "Armadura": playerStats.EquiparArmadura(item); break;
            case "Elmo": playerStats.EquiparElmo(item); break;
            case "Luva": playerStats.EquiparLuva(item); break;
            case "Acessorio": playerStats.EquiparAcessorio(item); break;
        }

        AtualizarEquip();
        navegandoSlots = true;
        cursorIndex = 0;
        MoveCursor(cursorIndex);
    }

    // =============================
    // ATUALIZA STATUS NA UI
    // =============================
    void AtualizarEquip()
    {
        if (playerStats == null) return;

        EquipamentoAtual.text =
            $"Arma: {(playerStats.armaEquipada != null ? playerStats.armaEquipada.nome : "Nenhuma")}\n" +
            $"Elmo: {(playerStats.elmoEquipada != null ? playerStats.elmoEquipada.nome : "Nenhum")}\n" +
            $"Armadura: {(playerStats.armaduraEquipada != null ? playerStats.armaduraEquipada.nome : "Nenhuma")}\n" +
            $"Luva: {(playerStats.luvaEquipada != null ? playerStats.luvaEquipada.nome : "Nenhuma")}\n" +
            $"Acessório: {(playerStats.acessorioEquipado != null ? playerStats.acessorioEquipado.nome : "Nenhum")}";
    }
}
