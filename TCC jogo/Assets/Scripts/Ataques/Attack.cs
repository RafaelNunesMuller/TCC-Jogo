using UnityEngine;

[CreateAssetMenu(fileName = "NovoAtaque", menuName = "RPG/Ataque")]
public class Attack : ScriptableObject
{
    [Header("Configuração do Ataque")]
    public string nome;
    public int power;
    public int manaCost;
    public bool isMagic; // true = mágico, false = físico

    [TextArea]
    public string descricao; // opcional, aparece em menus
}
