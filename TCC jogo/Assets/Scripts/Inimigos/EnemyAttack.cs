using UnityEngine;

[System.Serializable]
public class EnemyAttack : MonoBehaviour
{
    public string attackName;
    public int damage;

    public void Execute(playerStats player)
    {
        int finalDamage = Mathf.Max(1, damage - player.defense);
        player.currentHP -= finalDamage;
        Debug.Log($"👹 O inimigo usou {attackName} e causou {finalDamage} de dano!");
    }
}