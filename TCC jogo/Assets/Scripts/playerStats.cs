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

    [Header("Equipamentos")]
    public Equipamento armaEquipada;
    public Equipamento armaduraEquipada;

    // -------- MÉTODOS DE STATUS --------
    public int StrengthTotal => strength + (armaEquipada != null ? armaEquipada.bonusForca : 0);
    public int DefenseTotal  => defense + (armaduraEquipada != null ? armaduraEquipada.bonusDefesa : 0);

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
        Debug.Log("Força aumentada");
    }

    public void DefUp(int quantidade)
    {
        defense += quantidade;
        Debug.Log("Defesa aumentada");
    }

    // -------- EQUIPAR / DESEQUIPAR --------
    public void EquiparArma(Equipamento arma)
    {
        armaEquipada = arma;
        Debug.Log($"Arma equipada: {arma.nome}");
    }

    public void EquiparArmadura(Equipamento armadura)
    {
        armaduraEquipada = armadura;
        Debug.Log($"Armadura equipada: {armadura.nome}");
    }

    // -------- XP e Level --------
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
        return level * 10 + 20; 
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

    // Start
    void Start()
    {
        currentHP = maxHP;
    }
}
