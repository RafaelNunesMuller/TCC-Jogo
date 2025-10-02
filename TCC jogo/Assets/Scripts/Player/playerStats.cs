using UnityEngine;
using UnityEngine.SceneManagement;

public class playerStats : MonoBehaviour
{
    [Header("Atributos Base")]
    public int level = 1;
    public int experience = 0;

    public int strength = 5;
    public int defense = 3;
    public int magic = 1;
    public int magicDefense = 1;

    [Header("HP/MP")]
    public int maxHP = 50;
    public int currentHP;

    [Header("Equipamentos")]
    public Equipamento armaEquipada;
    public Equipamento armaduraEquipada;
    public Equipamento acessorioEquipado;
    public Equipamento elmoEquipada;
    public Equipamento luvaEquipada;

    [Header("Ataques Disponíveis")]
    public Attack[] ataques;

    // -------- PROPRIEDADES --------
    public int StrengthTotal => strength + (armaEquipada != null ? armaEquipada.bonusForca : 0);
    public int DefenseTotal => defense +
                               (armaduraEquipada != null ? armaduraEquipada.bonusDefesa : 0) +
                               (acessorioEquipado != null ? acessorioEquipado.bonusDefesa : 0);

    // -------- M�TODOS DE EQUIPAR --------
    public void EquiparArma(Item arma)
    {
        // Remove b�nus da arma anterior
        if (armaEquipada != null)
            strength -= armaEquipada.bonusForca;

        // Equipa a nova arma
        armaEquipada = new Equipamento(arma.nome, arma.bonusForca, arma.bonusDefesa, arma.icone);
        strength += armaEquipada.bonusForca;

        Debug.Log("Equipou arma: " + arma.nome + " | Strength agora: " + strength);
    }


    public void EquiparArmadura(Item armadura)
    {
        // Remove b�nus da arma anterior
        if (armaduraEquipada != null)
            strength -= armaduraEquipada.bonusForca;

        armaduraEquipada = new Equipamento(armadura.nome, armadura.bonusForca, armadura.bonusDefesa, armadura.icone);
        defense += armaduraEquipada.bonusDefesa;
        Debug.Log("Equipou armadura: " + armadura.nome);
    }

    public void EquiparElmo(Item elmo)
    {
        // Remove b�nus da arma anterior
        if (elmoEquipada != null)
            strength -= elmoEquipada.bonusForca;

        elmoEquipada = new Equipamento(elmo.nome, elmo.bonusForca, elmo.bonusDefesa, elmo.icone);
        defense += elmoEquipada.bonusDefesa;
        Debug.Log("Equipou o elmo: " + elmo.nome);
    }


    public void EquiparLuva(Item luva)
    {
        // Remove b�nus da arma anterior
        if (luvaEquipada != null)
            strength -= luvaEquipada.bonusForca;

        luvaEquipada = new Equipamento(luva.nome, luva.bonusForca, luva.bonusDefesa, luva.icone);
        defense += luvaEquipada.bonusDefesa;
        Debug.Log("Equipou o elmo: " + luva.nome);
    }

    public void EquiparAcessorio(Item acessorio)
    {
        // Remove b�nus da arma anterior
        if (acessorioEquipado != null)
            strength -= acessorioEquipado.bonusForca;

        acessorioEquipado = new Equipamento(acessorio.nome, acessorio.bonusForca, acessorio.bonusDefesa, acessorio.icone);
        defense += acessorioEquipado.bonusDefesa;
        Debug.Log("Equipou acess�rio: " + acessorio.nome);
    }

    // -------- M�TODOS DE CURA E AUMENTO --------
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
        Debug.Log("For�a aumentada");
    }

    public void DefUp(int quantidade)
    {
        defense += quantidade;
        Debug.Log("Defesa aumentada");
    }

    // -------- XP e LEVEL --------
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
        maxHP += 5;
        currentHP = maxHP;
        Debug.Log("Level up! Agora n�vel " + level);
    }

    

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;

        Debug.Log($"💔 Player recebeu {damage} de dano! (HP: {currentHP}/{maxHP})");

        FindAnyObjectByType<DamageFlash>()?.Flash();

        CameraShake camShake = Camera.main.GetComponent<CameraShake>();
        if (camShake != null)
        {
            StartCoroutine(camShake.Shake(1.2f, 0.2f)); // duração, intensidade
        }

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"☠️ Player foi derrotado!");
        SceneManager.LoadScene("Game Over");

        
    }

    // -------- START --------
    void Start()
    {
        currentHP = maxHP;

    }

    
}
