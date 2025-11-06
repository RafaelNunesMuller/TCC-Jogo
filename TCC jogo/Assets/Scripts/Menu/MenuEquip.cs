using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuEquip : MonoBehaviour
{
    public playerStats playerStats;

    public TMP_Text EquipamentoAtual;
    public Transform equipListParent;
    public GameObject equipItemPrefab;
    public RectTransform cursor;

    public Button weaponButton;
    public Button healmetButton;
    public Button gloveButton;
    public Button armorButton;
    public Button accessoryButton;

    [Header("Referências")]

    public GameObject menuPanel;

    private List<Item> inventario = new List<Item>();
    private List<RectTransform> equipSlots = new List<RectTransform>();
    private List<Item> itensAtuais = new List<Item>();
    private int cursorIndex = 0;
    private string slotSelecionado = "";

    private bool navegandoSlots = true; 
    private List<RectTransform> botoesSlots = new List<RectTransform>();

    public GameObject EquipItemPrefab;

    private Inventario inventarioCentral;


    void Start()
    {
        Sprite espadaComum = Resources.Load<Sprite>("Icones/sword_02a");
        Sprite espadaIncomum = Resources.Load<Sprite>("Icones/sword_02b");
        Sprite espadaRara = Resources.Load<Sprite>("Icones/sword_02c");
        Sprite espadaEpica = Resources.Load<Sprite>("Icones/sword_02d");
        Sprite espadaLendaria = Resources.Load<Sprite>("Icones/sword_02e");

        Sprite elmoComum = Resources.Load<Sprite>("Icones/Helmet_02a");
        Sprite elmoIncomum = Resources.Load<Sprite>("Icones/Helmet_02b");
        Sprite elmoRara = Resources.Load<Sprite>("Icones/Helmet_02c");
        Sprite elmoEpica = Resources.Load<Sprite>("Icones/Helmet_02d");
        Sprite elmoLendaria = Resources.Load<Sprite>("Icones/Helmet_02e");

        Sprite armaduraComum = Resources.Load<Sprite>("Icones/armor_01a");
        Sprite armaduraIncomum = Resources.Load<Sprite>("Icones/armor_01b");
        Sprite armaduraRara = Resources.Load<Sprite>("Icones/armor_01c");
        Sprite armaduraEpica = Resources.Load<Sprite>("Icones/armor_01d");
        Sprite armaduraLendaria = Resources.Load<Sprite>("Icones/armor_01e");

        Sprite luvaComum = Resources.Load<Sprite>("Icones/Gloves_01a");
        Sprite luvaIncomum = Resources.Load<Sprite>("Icones/Gloves_01b");
        Sprite luvaRara = Resources.Load<Sprite>("Icones/Gloves_01c");
        Sprite luvaEpica = Resources.Load<Sprite>("Icones/Gloves_01d");
        Sprite luvaLendaria = Resources.Load<Sprite>("Icones/Gloves_01e");

        Sprite acessorioComum = Resources.Load<Sprite>("Icones/necklace_01a");
        Sprite acessorioIncomum = Resources.Load<Sprite>("Icones/necklace_01b");
        Sprite acessorioRara = Resources.Load<Sprite>("Icones/necklace_01c");
        Sprite acessorioEpica = Resources.Load<Sprite>("Icones/necklace_01d");
        Sprite acessorioLendaria = Resources.Load<Sprite>("Icones/necklace_01e");



        inventarioCentral = Inventario.instance;
        botoesSlots.Add(weaponButton.GetComponent<RectTransform>());
        botoesSlots.Add(healmetButton.GetComponent<RectTransform>());
        botoesSlots.Add(gloveButton.GetComponent<RectTransform>());
        botoesSlots.Add(armorButton.GetComponent<RectTransform>());    
        botoesSlots.Add(accessoryButton.GetComponent<RectTransform>());

        
        AtualizarEquip();

        cursorIndex = 0;
        MoveCursor(cursorIndex);
    }

    void Update()
    {
        if (!gameObject.activeSelf) return;
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
                equipItemPrefab.SetActive(true);

                if (cursorIndex == 0) SelecionarSlot("Arma");
                else if (cursorIndex == 1) SelecionarSlot("Elmo");
                else if (cursorIndex == 2) SelecionarSlot("Luva");
                else if (cursorIndex == 3) SelecionarSlot("Armadura");
                else if (cursorIndex == 4) SelecionarSlot("Acessorio");
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                gameObject.SetActive(false);
            }
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
        if (slotSelecionado == "Arma")
            playerStats.EquiparArma(item);
        else if (slotSelecionado == "Armadura")
            playerStats.EquiparArmadura(item);
        else if (slotSelecionado == "Elmo")
            playerStats.EquiparArmadura(item);
        else if (slotSelecionado == "Luva")
            playerStats.EquiparArmadura(item);
        else if (slotSelecionado == "Acessorio")
            playerStats.EquiparAcessorio(item);

        AtualizarEquip();
    }

    

    void AtualizarEquip()
    {
        EquipamentoAtual.text =
            $"Arma: {playerStats.armaEquipada.nome}\n" +
            $"Elmo: {playerStats.elmoEquipada.nome}\n" +
            $"Armadura: {playerStats.armaduraEquipada.nome}\n" +
            $"Luva: {playerStats.luvaEquipada.nome}\n" +
            $"Acessório: {playerStats.acessorioEquipado.nome}";
    }
}
