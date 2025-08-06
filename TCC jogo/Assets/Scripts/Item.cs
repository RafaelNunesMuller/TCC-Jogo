[System.Serializable]
public class Item
{
    public string nome;
    public string tipo;
    public int quantidade;

    public Item(string nome, string tipo, int quantidade)
    {
        this.nome = nome;
        this.tipo = tipo;
        this.quantidade = quantidade;
    }
}
