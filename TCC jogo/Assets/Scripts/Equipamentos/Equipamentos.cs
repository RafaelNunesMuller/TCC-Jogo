[System.Serializable]
public class Equipamento
{
    public string nome;
    public int bonusForca;
    public int bonusDefesa;

    public Equipamento(string nome, int bonusForca, int bonusDefesa)
    {
        this.nome = nome;
        this.bonusForca = bonusForca;
        this.bonusDefesa = bonusDefesa;
    }
}
