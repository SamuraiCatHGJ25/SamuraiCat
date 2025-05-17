using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    // Armor protects unit from damage, trading 1 unit of armor for 1 instance of damage
    [SerializeField] private int armor;
    [SerializeField] private int maxArmor;
    [SerializeField] private String anim_hurt;
    [SerializeField] private String anim_death;

    private int curHealth;
    private Boolean dead;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void die(String unitName)
    {
        // play death anim and effects by asset/file name specified in calling func
        Destroy(this.gameObject);
    }

    void damage(int damage)
    {
        if (armor > 0)
        {
            armor -= 1;
            damage = 0;
        }
        
     curHealth -= damage;
     if (curHealth <= 0)
     {
         die(anim_death);
     }
    }

    void heal(int heal)
    {
        curHealth += heal;
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
    }

    void addArmor(int armor)
    {
        armor += armor;
        if (armor >= maxArmor)
        {
            armor = maxArmor;
        }
    }

    void removeArmor(int armor)
    {
        armor -= armor;
        if (armor > 0)
        {
            armor = 0;
        }
    }
    
}
