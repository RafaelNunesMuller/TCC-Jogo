public class Potion : Item
{
    public int curarHP;

    public Potion(int quantidade) : base("Potion", ItemTipo.Consumivel, quantidade)
    {
        curarHP = 20; // Quantidade que vai curar
    }

    
}
