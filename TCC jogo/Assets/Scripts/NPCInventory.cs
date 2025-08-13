using System.Collections.Generic;
using UnityEngine;

public class NPCInventory : MonoBehaviour
{
    public List<Item> itens = new List<Item>();

    void Start()
    {
        // Exemplo: Invent�rio fixo do NPC
        itens.Add(new Item("Po��o", "Consum�vel", 5));
        itens.Add(new Item("Espada de Ferro", "Equipamento", 1));
    }

    // M�todo para adicionar item ao NPC
    public void AdicionarItem(Item item)
    {
        itens.Add(item);
    }

    // M�todo para remover item
    public void RemoverItem(string nome)
    {
        itens.RemoveAll(i => i.nome == nome);
    }
}
