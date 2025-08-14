using UnityEngine;

public class Equipamento
{
    public string nome;
    public int bonusForca;
    public int bonusDefesa;
    public Sprite sprite;

    public Equipamento(string nome, int bonusForca, int bonusDefesa, Sprite sprite = null)
    {
        this.nome = nome;
        this.bonusForca = bonusForca;
        this.bonusDefesa = bonusDefesa;
        this.sprite = sprite;
    }
}
