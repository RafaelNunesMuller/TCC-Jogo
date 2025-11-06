using UnityEngine;

public class DefensePotion : Item
{
    public int DefUp;

    public DefensePotion(int quantidade) : base("DefensePotion", ItemTipo.Consumivel, quantidade)
    {
        DefUp = 20;
    }
}
