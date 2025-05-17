using UnityEngine;

public enum SpellType
{
    Projectile,
    DirectionalAOE, // In front
    RadialAOE       // Around caster
}

[CreateAssetMenu(fileName = "NewSpell", menuName = "Spells/Spell Data")]
public class SpellData : ScriptableObject
{
    [Header("Basic Info")]
    public string spellName = "New Spell";
    public Sprite icon;
    public SpellType spellType = SpellType.Projectile;
    public float cooldown = 1f;
    public float manaCost = 10f;
    public float damage = 10f;

    [Header("Projectile Specific (if SpellType is Projectile)")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 20f;
    public float projectileLifetime = 3f; // How long it lives if it doesn't hit anything

    [Header("Directional AOE Specific (if SpellType is DirectionalAOE)")]
    public GameObject directionalAoEPrefab; // Visual effect
    public float aoeRange = 5f;          // How far the box extends
    public float aoeWidth = 3f;          // How wide the box is
    public float aoeHeight = 2f;         // How tall the box is (for 3D)
    public float aoeOffsetDistance = 1f; // How far in front of player the AoE center is slightly pushed

    [Header("Radial AOE Specific (if SpellType is RadialAOE)")]
    public GameObject radialAoEPrefab;    // Visual effect
    public float shockwaveRadius = 5f;

    [Header("Common Effect Settings")]
    public LayerMask hitMask; // What layers can this spell hit (e.g., "Enemies")
    public GameObject hitEffectPrefab; // Particle effect for when something is hit
    public float effectDuration = 0.5f; // How long AoE visuals or hit effects last
}