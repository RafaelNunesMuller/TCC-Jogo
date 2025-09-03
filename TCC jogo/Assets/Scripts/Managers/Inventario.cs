using System;
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
        itens.Add(new Potion(3));
        itens.Add(new StrenghPotion(4));
        itens.Add(new DefensePotion(4));
    }

    public void funcItens(int index)
    {
        if (index < 0 || index >= itens.Count) return;

        Item item = itens[index];

        // Busca o PlayerStats na cena
        playerStats player = FindFirstObjectByType<playerStats>();
        if (player != null)
        {
            item.Usar(player); // passa o player
        }

        // Diminui quantidade
        item.quantidade--;
        if (item.quantidade <= 0)
        {
            itens.RemoveAt(index);
        }
    }

}
