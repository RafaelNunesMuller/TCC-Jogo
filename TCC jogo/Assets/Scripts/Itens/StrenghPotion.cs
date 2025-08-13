using UnityEngine;

public class StrenghPotion : Item
{
    public int ForcaUp;

    public StrenghPotion(int quantidade) : base("StrenghPotion", "aumentou", quantidade)
    {
        ForcaUp = 20; // Quantidade que vai aumentar a força
    }

    public override void Usar(playerStats player)
    {
        player.ForcaUp(ForcaUp);
    }
}
