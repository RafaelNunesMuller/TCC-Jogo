using UnityEngine;

public class StrenghPotion : Item
{
    public int ForcaUp;

    public StrenghPotion(int quantidade) : base("StrenghPotion", ItemTipo.Consumivel, quantidade)
    {
        ForcaUp = 20; // Quantidade que vai aumentar a força
    }

    
}
