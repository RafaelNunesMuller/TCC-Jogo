public class Potion : Item
{
    public int curarHP;

    public Potion(int quantidade) : base("Potion", "Cura", quantidade)
    {
        curarHP = 20; // Quantidade que vai curar
    }

    public override void Usar(playerStats player)
    {
        player.Curar(curarHP);
    }
}
