using UnityEngine;

public class DefensePotion : Item
{
    public int DefUp;

    public DefensePotion(int quantidade) : base("DefensePotion", "aumentou", quantidade)
    {
        DefUp = 20; // Quantidade que vai aumentar a for�a
    }

    public override void Usar(playerStats player)
    {
        player.DefUp(DefUp);
    }
}
