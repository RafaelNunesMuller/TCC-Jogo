using UnityEngine;
using System.Collections.Generic;

public class Inventario : MonoBehaviour
{
    public static Inventario instance; // Singleton global
    public List<Item> itens = new List<Item>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ?? Mantém entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Adicionar(Item item)
    {
        itens.Add(item);
    }

    public void Usar(Item item, playerStats player)
    {
        item.Usar(player);
        item.quantidade--;
        if (item.quantidade <= 0) itens.Remove(item);
    }
}
