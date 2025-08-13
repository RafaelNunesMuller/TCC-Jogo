using UnityEngine;

public class Potion : Item
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Potion(int quantidade) : base("Potion", "Cura", quantidade) { }

    public override void Usar()
    {
        Debug.Log("Usou uma Potion");
        playerStats player = GameObject.FindFirstObjectByType<playerStats>();
        if (player != null )
        {
            player.Curar(50);
            Debug.Log("HP atual: " + player.currentHP);
        }
        else
        {
            Debug.LogWarning("Nenhum playerStats encontrado na cena!");
        }
    }
}
