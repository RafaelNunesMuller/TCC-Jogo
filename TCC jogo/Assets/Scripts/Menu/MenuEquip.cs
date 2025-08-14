using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuEquip : MonoBehaviour
{
    public playerStats playerStats;

    [Header("UI")]
    public TMP_Text statusText;
    public Transform equipListParent;
    public GameObject equipItemPrefab;
    public RectTransform cursor; // Cursor para seleção

    private List<Equipamento> inventarioEquip = new List<Equipamento>();
    private List<GameObject> botoesAtuais = new List<GameObject>();

    private string slotSelecionado = "";
    private int cursorIndex = 0;
    private bool selecionandoItem = false;

    void Start()
    {
        // Equipamentos de exemplo
        inventarioEquip.Add(new Equipamento("Espada de Ferro", 5, 0));
        inventarioEquip.Add(new Equipamento("Espada Lendária", 15, 0));
        inventarioEquip.Add(new Equipamento("Armadura de Couro", 0, 3));
        inventarioEquip.Add(new Equipamento("Armadura de Aço", 0, 8));

        AtualizarStatus();
    }

    void Update()
    {
        if (!selecionandoItem) return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            cursorIndex = (cursorIndex - 1 + botoesAtuais.Count) % botoesAtuais.Count;
            MoverCursor();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            cursorIndex = (cursorIndex + 1) % botoesAtuais.Count;
            MoverCursor();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            Equipar(botoesAtuais[cursorIndex].GetComponent<ItemEquipUI>().equipamento);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            FecharLista();
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

        botoesAtuais.Clear();

        foreach (Equipamento equip in inventarioEquip)
        {
            if ((slotSelecionado == "Arma" && equip.bonusForca > 0) ||
                (slotSelecionado == "Armadura" && equip.bonusDefesa > 0))
            {
                GameObject obj = Instantiate(equipItemPrefab, equipListParent);
                obj.GetComponentInChildren<TMP_Text>().text = equip.nome;

                // Guardar info do equipamento no botão
                ItemEquipUI ui = obj.AddComponent<ItemEquipUI>();
                ui.equipamento = equip;

                botoesAtuais.Add(obj);
            }
        }

        if (botoesAtuais.Count > 0)
        {
            cursorIndex = 0;
            selecionandoItem = true;
            cursor.gameObject.SetActive(true);
            MoverCursor();
        }
        else
        {
            selecionandoItem = false;
            cursor.gameObject.SetActive(false);
        }
    }

    void MoverCursor()
    {
        cursor.position = botoesAtuais[cursorIndex].transform.position;
    }

    void Equipar(Equipamento equip)
    {
        if (slotSelecionado == "Arma")
            playerStats.EquiparArma(equip);
        else if (slotSelecionado == "Armadura")
            playerStats.EquiparArmadura(equip);

        AtualizarStatus();
        FecharLista();
    }

    void FecharLista()
    {
        selecionandoItem = false;
        cursor.gameObject.SetActive(false);
    }

    void AtualizarStatus()
    {
        statusText.text =
            $"HP: {playerStats.maxHP}\n" +
            $"ATK: {playerStats.strength}\n" +
            $"DEF: {playerStats.defense}";
    }
}

public class ItemEquipUI : MonoBehaviour
{
    public Equipamento equipamento;
}
