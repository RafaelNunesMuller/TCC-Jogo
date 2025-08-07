using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public static Inventario instance;

    public List<Item> itens = new List<Item>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        CarregarItensIniciais();
    }

    void CarregarItensIniciais()
    {
        itens.Add(new Item("Potion", "Cura", 3));
        itens.Add(new Item("Ether", "Magia", 1));
        itens.Add(new Item("Antidote", "Cura", 2));
    }
}
