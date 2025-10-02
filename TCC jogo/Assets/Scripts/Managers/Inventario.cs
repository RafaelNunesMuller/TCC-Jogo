using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public static Inventario instance;

    public List<Item> itens = new List<Item>();

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject); // persiste entre cenas
    }

    public void Adicionar(Item item)
    {
        itens.Add(item);
        Debug.Log($"Adicionado: {item.nome}");
    }

    public void Remover(Item item)
    {
        itens.Remove(item);
        Debug.Log($"Removido: {item.nome}");
    }

    public void Usar(Item item, playerStats player)
    {
        item.Usar(player); // cada item sabe o que faz
        if (item.consumivel)
            Remover(item);
    }
}
