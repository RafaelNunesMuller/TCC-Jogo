using UnityEngine;
using System.Collections.Generic;

public class BossBattleStarter : MonoBehaviour
{
    public BattleSystem battleSystem;
    public EnemyStats bossPrefab;

   void Start()
{
    if (FindAnyObjectByType<EnemyStats>() != null)
    {
        Debug.Log("Já existe um inimigo nessa cena, não criar outro.");
        return;
    }

    EnemyStats boss = Instantiate(bossPrefab);
    battleSystem.SetEnemies(new System.Collections.Generic.List<EnemyStats>() { boss });
}

}
