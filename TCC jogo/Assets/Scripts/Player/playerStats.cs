using UnityEngine;
using UnityEngine.SceneManagement;

public class playerStats : MonoBehaviour
{

    public int level = 1;
    public int experience = 0;

    public int strength = 5;
    public int defense = 3;
    public int magic = 1;
    public int magicDefense = 1;

    public int maxHP = 50;
    public int currentHP;

    public Equipamento armaEquipada;
    public Equipamento armaduraEquipada;
    public Equipamento acessorioEquipado;
    public Equipamento elmoEquipada;
    public Equipamento luvaEquipada;

    public Attack[] ataques;



    



    public int StrengthTotal => strength + (armaEquipada != null ? armaEquipada.bonusForca : 0);
    public int DefenseTotal => defense +
                               (armaduraEquipada != null ? armaduraEquipada.bonusDefesa : 0) +
                               (acessorioEquipado != null ? acessorioEquipado.bonusDefesa : 0);

    public void EquiparArma(Item arma)
    {
        if (armaEquipada != null)
            strength -= armaEquipada.bonusForca;

        armaEquipada = new Equipamento(arma.nome, arma.bonusForca, arma.bonusDefesa, arma.icone);
        strength += armaEquipada.bonusForca;

    }


    public void EquiparArmadura(Item armadura)
    {
        if (armaduraEquipada != null)
            strength -= armaduraEquipada.bonusForca;

        armaduraEquipada = new Equipamento(armadura.nome, armadura.bonusForca, armadura.bonusDefesa, armadura.icone);
        defense += armaduraEquipada.bonusDefesa;
    }

    public void EquiparElmo(Item elmo)
    {
        if (elmoEquipada != null)
            strength -= elmoEquipada.bonusForca;

        elmoEquipada = new Equipamento(elmo.nome, elmo.bonusForca, elmo.bonusDefesa, elmo.icone);
        defense += elmoEquipada.bonusDefesa;
    }


    public void EquiparLuva(Item luva)
    {
        if (luvaEquipada != null)
            strength -= luvaEquipada.bonusForca;

        luvaEquipada = new Equipamento(luva.nome, luva.bonusForca, luva.bonusDefesa, luva.icone);
        defense += luvaEquipada.bonusDefesa;
    }

    public void EquiparAcessorio(Item acessorio)
    {

        if (acessorioEquipado != null)
            strength -= acessorioEquipado.bonusForca;

        acessorioEquipado = new Equipamento(acessorio.nome, acessorio.bonusForca, acessorio.bonusDefesa, acessorio.icone);
        defense += acessorioEquipado.bonusDefesa;
    }

    public void Curar(int quantidade)
    {
        currentHP += quantidade;
        if (currentHP > maxHP)
            currentHP = maxHP;

    }

    public void ForcaUp(int quantidade)
    {
        strength += quantidade;
    }

    public void DefUp(int quantidade)
    {
        defense += quantidade;
    }

    public void GainExperience(int xp)
    {
        if (level == 10)
        {
            experience = 0;
        }
        else
        {
           experience += xp; 
        }

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
    }

    

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;

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
        SceneManager.LoadScene("Game Over");
    }

    void Start()
    {
        currentHP = maxHP;
        DontDestroyOnLoad(gameObject);
    }

    
}
