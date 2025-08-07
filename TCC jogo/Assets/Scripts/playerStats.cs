using UnityEngine;

public class playerStats : MonoBehaviour
{

    [Header("Atributos Base")]
    public int level = 1;
    public int experience = 0;

    public int strength = 5;
    public int defense = 3;
    public int magic = 4;
    public int magicDefense = 2;
    public int speed = 6;

    [Header("HP/MP")]
    public int maxHP = 30;
    public int currentHP;

    public int maxMP = 10;
    public int currentMP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = maxHP;
        currentMP = maxMP;
    }

    // Update is called once per frame
    public void GainExperience(int xp)
    {
        experience += xp;
        if (experience >= ExperienceToNextLevel())
        {
            LevelUp();
        }
    }

    int ExperienceToNextLevel()
    {
        return level * 10 + 20; // fórmula simples, pode ajustar depois
    }

    void LevelUp()
    {
        level++;
        experience = 0;
        strength += 1;
        defense += 1;
        magic += 1;
        magicDefense += 1;
        speed += 1;
        maxHP += 5;
        maxMP += 2;
        currentHP = maxHP;
        currentMP = maxMP;
        Debug.Log("Level up! Agora nível " + level);
    }
}

