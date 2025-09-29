using UnityEngine;

public enum ItemTipo
{
    Consumivel,
    Arma,
    Armadura,
    Acessorio,
    Luva,
    Elmo
}

[System.Serializable]
public class Item
{
    public string nome;
    public ItemTipo tipo;
    public int quantidade;
    public Sprite icone;

    // bônus caso seja equipamento
    public int bonusForca;
    public int bonusDefesa;

    public Item(string nome, ItemTipo tipo, int quantidade, Sprite icone = null, int bonusForca = 0, int bonusDefesa = 0)
    {
        this.nome = nome;
        this.tipo = tipo;
        this.quantidade = quantidade;
        this.icone = icone;
        this.bonusForca = bonusForca;
        this.bonusDefesa = bonusDefesa;
    }

    public void Usar(playerStats player)
    {
        if (tipo == ItemTipo.Consumivel)
        {
            if (nome == "Poção de Vida")
                player.Curar(30); // exemplo

            else if (nome == "StrenghPotion")
                player.strength += 5;

            else if (nome == "DefensePotion")
                player.defense += 5;
        }
    }

    

}
