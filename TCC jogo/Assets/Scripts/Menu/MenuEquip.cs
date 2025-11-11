using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuEquip : MonoBehaviour
{
    [Header("Referências Principais")]
    public playerStats playerStats;
    public Inventario inventarioCentral;

    [Header("UI")]
    public TMP_Text EquipamentoAtual;
    public Transform equipListParent;
    public GameObject equipItemPrefab; //  arraste o prefab real aqui
    public RectTransform cursor;

    [Header("Botões de Slots")]
    public Button weaponButton;
    public Button healmetButton;
    public Button gloveButton;
    public Button armorButton;
    public Button accessoryButton;

    [Header("Outros Painéis")]
    public GameObject menuPanel;

    private List<Item> inventario = new List<Item>();
    private List<RectTransform> equipSlots = new List<RectTransform>();
    private List<Item> itensAtuais = new List<Item>();
    private List<RectTransform> botoesSlots = new List<RectTransform>();

    private int cursorIndex = 0;
    private string slotSelecionado = "";
    private bool navegandoSlots = true;

    void Awake()
    {
        // pega o player de forma persistente
        if (GameManager.Instance != null && GameManager.Instance.playerStats != null)
            playerStats = GameManager.Instance.playerStats;
    }

    void Start()
    {
        inventarioCentral = Inventario.instance;

        // registra os botões
        botoesSlots.Add(weaponButton.GetComponent<RectTransform>());
        botoesSlots.Add(healmetButton.GetComponent<RectTransform>());
        botoesSlots.Add(gloveButton.GetComponent<RectTransform>());
        botoesSlots.Add(armorButton.GetComponent<RectTransform>());
        botoesSlots.Add(accessoryButton.GetComponent<RectTransform>());

        if (equipItemPrefab == null)
            Debug.LogError(" [MenuEquip] EquipItemPrefab não está atribuído no Inspector!");

        cursorIndex = 0;
        MoveCursor(cursorIndex);
        AtualizarEquip();
    }

    void OnEnable()
    {
        // Reatribui as referências caso percam ao trocar de cena
        if (GameManager.Instance != null && playerStats == null)
            playerStats = GameManager.Instance.playerStats;

        if (inventarioCentral == null)
            inventarioCentral = Inventario.instance;

        if (equipItemPrefab == null)
            Debug.LogWarning(" [MenuEquip] Prefab ainda não atribuído — arraste no Inspector!");

        AtualizarEquip();
    }

    void Update()
    {
        if (!gameObject.activeSelf) return;
        HandleInput();
    }

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
            if (Input.GetKeyDown(KeyCode.X))
                VoltarParaMenu();
        }
        else
        {
            if (equipSlots.Count == 0) return;

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
                if (cursorIndex >= 0 && cursorIndex < itensAtuais.Count)
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

    public void SelecionarSlot(string slot)
    {
        slotSelecionado = slot;
        MostrarListaEquip();
    }

    void MostrarListaEquip()
    {
        if (equipItemPrefab == null)
        {
            Debug.LogError(" EquipItemPrefab está nulo! Arraste no Inspector.");
            return;
        }

        // limpa os itens antigos
        foreach (Transform child in equipListParent)
            Destroy(child.gameObject);

        equipSlots.Clear();
        itensAtuais.Clear();

        inventario = inventarioCentral.itens;

        foreach (Item item in inventario)
        {
            if ((slotSelecionado == "Arma" && item.tipo == ItemTipo.Arma) ||
                (slotSelecionado == "Armadura" && item.tipo == ItemTipo.Armadura) ||
                (slotSelecionado == "Acessorio" && item.tipo == ItemTipo.Acessorio) ||
                (slotSelecionado == "Elmo" && item.tipo == ItemTipo.Elmo) ||
                (slotSelecionado == "Luva" && item.tipo == ItemTipo.Luva))
            {
                GameObject obj = Instantiate(equipItemPrefab, equipListParent);
                ItemEquipUI ui = obj.GetComponent<ItemEquipUI>();
                ui.Configurar(item, () => Equipar(item));

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

    void Equipar(Item item)
    {
        if (playerStats == null)
        {
            Debug.LogWarning(" PlayerStats está nulo, tentando restaurar...");
            playerStats = GameManager.Instance?.playerStats;
            if (playerStats == null) return;
        }

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

    public void VoltarParaMenu()
    {
        gameObject.SetActive(false);
        menuPanel.SetActive(true);
    }
}
