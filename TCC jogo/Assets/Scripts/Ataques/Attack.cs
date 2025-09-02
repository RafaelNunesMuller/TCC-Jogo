using UnityEngine;

[System.Serializable]
public class Attack
{
    public string nome;
    public int power;
    public int manaCost;
    public bool isMagic; // true = usa magia, false = ataque físico
}
