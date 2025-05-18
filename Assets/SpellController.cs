using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI; // For cooldowns

public class SpellCaster : MonoBehaviour
{
    public Transform castPoint; // Where projectiles/AoEs originate (e.g., player's hand or slightly in front)
    public List<SpellData> availableSpells; // Assign your SpellData assets here in Inspector
    public int currentSpellIndex = 0;

    // Basic mana system (can be expanded)
    public float currentMana = 100f;
    [SerializeField] public float maxMana = 100f;
    [SerializeField] public float manaRegenRate = 5f; // Mana per second

    private Dictionary<SpellData, float> spellCooldowns = new Dictionary<SpellData, float>();
    private Image manaBar;
    private Boolean castDelayed = false;
    
    IEnumerator delayCast(float delay)
    {
        castDelayed = true;
        
        // Move the first cube up or down.
        yield return new WaitForSeconds(delay);

        castDelayed = false;
    }
    
    void Start()
    {
        if (castPoint == null)
        {
            castPoint = transform; // Default to caster's position if not set
        }

        // Initialize cooldowns
        foreach (SpellData spell in availableSpells)
        {
            spellCooldowns[spell] = 0f;
        }
        
        manaBar = GameObject.Find("MPBarBackground").GetComponent<Image>();
    }

    void Update()
    {
        // Update cooldowns
        List<SpellData> keys = new List<SpellData>(spellCooldowns.Keys);
        foreach (SpellData spell in keys)
        {
            if (spellCooldowns[spell] > 0)
            {
                spellCooldowns[spell] -= Time.deltaTime;
            }
        }

        // Mana Regen
        if (currentMana < maxMana)
        {
            currentMana += manaRegenRate * Time.deltaTime;
            currentMana = Mathf.Min(currentMana, maxMana);
            UpdateManaBar(manaBar);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1) && availableSpells.Count > 0) currentSpellIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2) && availableSpells.Count > 1) currentSpellIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3) && availableSpells.Count > 2) currentSpellIndex = 2;

        for (int i = 0; i < availableSpells.Count; i++)
        {
            
        }



        if (Input.GetButtonDown("Fire2")) //  Left Mouse Click or other cast button
        {
            if (availableSpells.Count > currentSpellIndex)
            {
                TryCastSpell(availableSpells[currentSpellIndex]);
            }
        }
    }

    public void TryCastSpell(SpellData spell)
    {
        if (spell == null)
        {
            Debug.LogWarning("No spell selected/available.");
            return;
        }

        if (spellCooldowns.ContainsKey(spell) && spellCooldowns[spell] <= 0 && currentMana >= spell.manaCost)
        {
            currentMana -= spell.manaCost;
            
            Debug.Log($"Casting {spell.spellName}!");
            CastSpellActual(spell);
            spellCooldowns[spell] = spell.cooldown; // Start cooldown
        }
        else if (currentMana < spell.manaCost)
        {
            Debug.Log($"Not enough mana for {spell.spellName}!");
        }
        else
        {
            Debug.Log($"{spell.spellName} is on cooldown!");
        }
    }

    private void CastSpellActual(SpellData spell)
    {
        // Use castPoint's orientation for spells
        Vector3 spellOrigin = castPoint.position;
        Quaternion spellRotation = castPoint.rotation; // Use caster's forward direction for directional spells

        switch (spell.spellType)
        {
            case SpellType.Projectile:
                CastProjectile(spell, spellOrigin, spellRotation);
                break;
            case SpellType.DirectionalAOE:
                CastDirectionalAOE(spell, spellOrigin, spellRotation);
                break;
            case SpellType.RadialAOE:
                CastRadialAOE(spell, spellOrigin);
                break;
        }
    }
    

    private void CastProjectile(SpellData spell, Vector3 origin, Quaternion rotation)
    {
        if (spell.projectilePrefab == null)
        {
            Debug.LogError($"Projectile prefab not set for {spell.spellName}");
            return;
        }

        GameObject projectileGO = Instantiate(spell.projectilePrefab, origin, rotation);
        Projectile projectileScript = projectileGO.GetComponent<Projectile>();
        
        if (projectileScript != null)
        {
            projectileScript.Initialize(spell.damage, spell.projectileSpeed, spell.projectileLifetime, spell.hitMask, spell.hitEffectPrefab);
        }
        else
        {
            // Fallback if no projectile script - basic forward movement with Rigidbody
            Rigidbody rb = projectileGO.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = projectileGO.transform.forward * spell.projectileSpeed;
            }
            Destroy(projectileGO, spell.projectileLifetime); // Ensure it gets destroyed
        }
    }

    private void CastDirectionalAOE(SpellData spell, Vector3 origin, Quaternion rotation)
    {
        // Visualize the AoE
        if (spell.directionalAoEPrefab != null)
        {
            GameObject vfx = Instantiate(spell.directionalAoEPrefab, origin, rotation);
            // Adjust vfx to match aoeRange and aoeWidth if the particle system isn't pre-scaled
            // e.g., vfx.transform.localScale = new Vector3(spell.aoeWidth, spell.aoeHeight, spell.aoeRange);
            Destroy(vfx, spell.effectDuration);
        }
        
        

        // Calculate Box AoE parameters
        // The center of the box is pushed forward slightly from the cast point along its forward direction
        Vector3 boxCenter = origin + (castPoint.forward * (spell.aoeRange / 2f + spell.aoeOffsetDistance));
        Vector3 halfExtents = new Vector3(spell.aoeWidth / 2f, spell.aoeHeight / 2f, spell.aoeRange / 2f);
        
        if (spell.castingDelay > 0f)
        {
            Debug.Log($"Delayed Spell Cast > {spell.spellName}!");
            castDelayed = true;
            StartCoroutine(delayCast(spell.castingDelay));
            while (castDelayed)
            {
                Debug.Log($"Waiting to cast...");
            }
        }

        Collider[] hits = Physics.OverlapBox(boxCenter, halfExtents, rotation, spell.hitMask);

        foreach (Collider hit in hits)
        {
            // Ensure we don't hit ourselves if the caster is on the hitMask layer
            if (hit.transform == transform) continue;

            Debug.Log($"Directional AOE hit: {hit.name}");
            
            ApplyDamageAndEffect(hit.transform, spell.damage, spell.hitEffectPrefab);
            // You might want to ensure each target is only hit once per cast
        }
    }


    private void CastRadialAOE(SpellData spell, Vector3 origin)
    {
        // Visualize the AoE
        if (spell.radialAoEPrefab != null)
        {
            GameObject vfx = Instantiate(spell.radialAoEPrefab, origin, Quaternion.identity);
            // Adjust vfx scale if needed, e.g.,
            // vfx.transform.localScale = Vector3.one * spell.shockwaveRadius * 2; // For a sphere
            Destroy(vfx, spell.effectDuration);
        }
        
        if (spell.castingDelay > 0f)
        {
            Debug.Log($"Delayed Spell Cast > {spell.spellName}!");
            StartCoroutine(delayCast(spell.castingDelay));
            while (castDelayed)
            {
                Debug.Log($"Waiting to cast...");
            }
        }
        Collider[] hits = Physics.OverlapSphere(origin, spell.shockwaveRadius, spell.hitMask);

        foreach (Collider hit in hits)
        {
            // Ensure we don't hit ourselves
            if (hit.transform == transform) continue;
            
            Debug.Log($"Radial AOE hit: {hit.name}");
            ApplyDamageAndEffect(hit.transform, spell.damage, spell.hitEffectPrefab);
        }
    }

    private void ApplyDamageAndEffect(Transform target, float damage, GameObject hitEffectPrefab)
    {
        // Attempt to get a HealthController (or your equivalent) from the target
        EnemyHealthController healthController = target.GetComponent<EnemyHealthController>();
        if (healthController != null)
        {
            healthController.setDamage((int)damage); // Assuming TakeDamage takes an int
        }

        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, target.position, Quaternion.LookRotation(target.position - castPoint.position));
            // Destroy hit effect after a short duration if it doesn't destroy itself
        }
    }
    
    public void UpdateManaBar(Image bar)
    {
        bar.fillAmount = currentMana / (float)maxMana;
    }
}