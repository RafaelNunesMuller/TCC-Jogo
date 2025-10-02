using UnityEngine;

public class Potion : Item
{
    public int cura;

    public Potion(string nome, int qtd, Sprite icone, int cura)
        : base(nome, ItemTipo.Pocao, qtd, icone, 0, 0, true)
    {
        this.cura = cura;
    }

    public override void Usar(playerStats player)
    {
        player.Curar(cura);
        Debug.Log($"{nome} curou {cura} HP!");
    }
}
