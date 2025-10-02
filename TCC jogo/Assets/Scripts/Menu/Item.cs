using UnityEngine;

public class Item
{
    public string nome;
    public ItemTipo tipo;
    public int quantidade;
    public Sprite icone;
    public bool consumivel;

    public int bonusForca;
    public int bonusDefesa;

    public Item(string nome, ItemTipo tipo, int quantidade, Sprite icone = null, int bonusForca = 0, int bonusDefesa = 0, bool equipavel = false)
    {
        this.nome = nome;
        this.tipo = tipo;
        this.quantidade = quantidade;
        this.icone = icone;
        this.bonusForca = bonusForca;
        this.bonusDefesa = bonusDefesa;
    }

    // Virtual ? sobrescrito nas subclasses (Poções, etc.)
    public virtual void Usar(playerStats player)
    {
        Debug.Log($"{nome} foi usado.");
    }
}

public enum ItemTipo
{
    Consumivel,
    Arma,
    Armadura,
    Acessorio,
    Elmo,
    Luva,
    Pocao
}
