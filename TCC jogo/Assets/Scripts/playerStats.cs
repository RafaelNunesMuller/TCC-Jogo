using UnityEngine;

public class playerStats : MonoBehaviour
{

    [Header("Atributos Base")]
    public int level = 1;
    public int experience = 0;

    public int strength = 5;
    public int defense = 3;
    public int magic = 1;
    public int magicDefense = 1;
    public int speed = 6;

    [Header("HP/MP")]
    public int maxHP = 50;
    public int currentHP;


    public void Curar(int quantidade)
    {
        currentHP += quantidade;
        if (currentHP > maxHP)
            currentHP = maxHP;

        Debug.Log("HP Atual: " + currentHP + "/" + maxHP);
    }

    public void ForcaUp(int quantidade)
    {
        strength += quantidade;
        
        Debug.Log("forca aumentada");
    }

    public void DefUp(int quantidade)
    {
        defense += quantidade;

        Debug.Log("defesa aumentada");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = maxHP;
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
        speed += 1;
        maxHP += 5;
        currentHP = maxHP;
        Debug.Log("Level up! Agora nível " + level);
    }
}

