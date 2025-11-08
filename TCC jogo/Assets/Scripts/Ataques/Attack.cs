using UnityEngine;

[CreateAssetMenu(fileName = "NovoAtaque", menuName = "RPG/Ataque")]
public class Attack : ScriptableObject
{
    public string nome;
    public int power;
    public int manaCost;
    public bool isMagic;

    [TextArea]
    public string descricao;
}
