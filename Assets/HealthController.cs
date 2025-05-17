using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject nonPlayerCharacter;
    [SerializeField] private int maxHealth;
    // Armor protects unit from damage, trading 1 unit of armor for 1 instance of damage
    [SerializeField] private int armor;
    [SerializeField] private int maxArmor;
    [SerializeField] private String anim_hurt;
    [SerializeField] private String anim_death;
    
    private Image healthBar;
    private int curHealth;
    private Boolean dead;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        curHealth = maxHealth;
        if (player != null)
        {
            healthBar = GameObject.Find("HPBarBackground").GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void die(String unitName)
    {
        // play death anim and effects by asset/file name specified in calling func
        Destroy(this.gameObject);
    }

    public void damage(int damage)
    {
        if (armor > 0)
        {
            armor -= 1;
            damage = 0;
        }
        
     curHealth -= damage;
     
     if (player != null)
     {
         UpdateHealthBar(healthBar);
     }
     
     if (curHealth <= 0)
     {
         die(anim_death);
     }
    }

    public void heal(int heal)
    {
        curHealth += heal;
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }

        if (player != null)
        {
            UpdateHealthBar(healthBar);
        }
    }

    public void addArmor(int armor)
    {
        armor += armor;
        if (armor >= maxArmor)
        {
            armor = maxArmor;
        }
    }

    public void removeArmor(int armor)
    {
        armor -= armor;
        if (armor > 0)
        {
            armor = 0;
        }
    }

    public void UpdateHealthBar(Image bar)
    {
        bar.fillAmount = curHealth / (float)maxHealth * 100;
    }

}
