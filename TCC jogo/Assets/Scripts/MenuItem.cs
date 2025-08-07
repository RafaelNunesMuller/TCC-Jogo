using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuItem : MonoBehaviour
{
    public GameObject itemSlotPrefab;
    public Transform itemSlotContainer;
    public RectTransform cursor;

    private List<RectTransform> itemSlots = new List<RectTransform>();
    private int cursorIndex = 0;

    void Start()
    {
        GenerateItemSlots(25);
        MoveCursor(0);
    }

    void Update()
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
            gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            UseItem(cursorIndex);
        }
    }

    void GenerateItemSlots(int count)
    {
        var listaItens = Inventario.instance.itens;

        for (int i = 0; i < count; i++)
        {
            GameObject newSlot = Instantiate(itemSlotPrefab, itemSlotContainer);
            newSlot.transform.localScale = Vector3.one;
            Text text = newSlot.GetComponentInChildren<Text>();

            if (i < listaItens.Count)
            {
                Item item = listaItens[i];
                text.text = item.nome + " x" + item.quantidade;
            }
            else
            {
                text.text = "---";
            }

            itemSlots.Add(newSlot.GetComponent<RectTransform>());
        }
    }

    void MoveCursor(int index)
    {
        cursor.SetParent(itemSlots[index]);
        cursor.anchoredPosition = new Vector2(-40f, 0f);
    }

    void UseItem(int index)
    {
        var listaItens = Inventario.instance.itens;

        if (index < listaItens.Count)
        {
            Item item = listaItens[index];
            item.quantidade--;

            Debug.Log("Usou: " + item.nome);

            if (item.quantidade <= 0)
            {
                listaItens.RemoveAt(index);
            }

            AtualizarSlots();
        }
    }

    void AtualizarSlots()
    {
        foreach (Transform child in itemSlotContainer)
        {
            Destroy(child.gameObject);
        }

        itemSlots.Clear();
        GenerateItemSlots(25);
        cursorIndex = Mathf.Clamp(cursorIndex, 0, itemSlots.Count - 1);
        MoveCursor(cursorIndex);
    }
}
