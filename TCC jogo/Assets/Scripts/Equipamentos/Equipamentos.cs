using UnityEngine;

[System.Serializable]
public class Equipamento
{
    public string nome;
    public int bonusForca;
    public int bonusDefesa;
    public Sprite sprite; // ícone do equipamento

    public Equipamento(string nome, int forca, int defesa, Sprite sprite = null)
    {
        this.nome = nome;
        this.bonusForca = forca;
        this.bonusDefesa = defesa;
        this.sprite = sprite;
    }
}
