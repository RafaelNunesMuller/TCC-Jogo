using System.Collections.Generic;
using UnityEngine;

public class NPCInventory : MonoBehaviour
{
    public List<Item> itens = new List<Item>();

    void Start()
    {
        // Exemplo: Inventário fixo do NPC
        itens.Add(new Item("Poção", "Consumível", 5));
        itens.Add(new Item("Espada de Ferro", "Equipamento", 1));
    }

    // Método para adicionar item ao NPC
    public void AdicionarItem(Item item)
    {
        itens.Add(item);
    }

    // Método para remover item
    public void RemoverItem(string nome)
    {
        itens.RemoveAll(i => i.nome == nome);
    }
}
