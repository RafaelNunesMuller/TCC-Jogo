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
    }

    public void funcItens(int index)
{
    // Se índice inválido, não faz nada
    if (index < 0 || index >= itens.Count)
        return;

    // Usa o item
    Item item = itens[index];
    item.Usar();

    // Diminui quantidade
    item.quantidade--;
    if (item.quantidade <= 0)
    {
        itens.RemoveAt(index);
    }
}
}
