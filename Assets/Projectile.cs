using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private float damage;
    private float speed;
    private float lifetime;
    private LayerMask hitMask;
    private GameObject hitEffectPrefab;

    private Rigidbody rb;
    private float currentLifetime = 0f;

    public void Initialize(float dmg, float spd, float life, LayerMask mask, GameObject hitVFX)
    {
        damage = dmg;
        speed = spd;
        lifetime = life;
        hitMask = mask;
        hitEffectPrefab = hitVFX;

        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Projectile prefab needs a Rigidbody!", gameObject);
            Destroy(gameObject);
            return;
        }
        rb.useGravity = false;
        rb.linearVelocity = transform.forward * speed;
        currentLifetime = 0f;
    }

    void Update()
    {
        currentLifetime += Time.deltaTime;
        if (currentLifetime >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // The (1 << other.gameObject.layer) creates a bitmask for the collided object's layer
        // and compares it with our hitMask
        if (((1 << other.gameObject.layer) & hitMask) != 0)
        {
            HandleHit(other.transform);
        }
        
        // else if (other.gameObject.layer != LayerMask.NameToLayer("IgnoreProjectiles"))
        // {
        // Destroy(gameObject);
        // }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & hitMask) != 0)
        {
            HandleHit(collision.transform);
        }
        // destroy on hitting anything
        // else if (collision.gameObject.layer != LayerMask.NameToLayer("IgnoreProjectiles"))
        // {
        // Destroy(gameObject);
        // }
    }


    private void HandleHit(Transform hitTarget)
    {
        Debug.Log($"{gameObject.name} hit {hitTarget.name}");

        // Apply Damage
        EnemyHealthController health = hitTarget.GetComponent<EnemyHealthController>();
        if (health != null)
        {
            health.setDamage((int)damage);
        }

        // Spawn Hit Effect
        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.LookRotation(hitTarget.position - transform.position));
            // The hit effect prefab should manage its own life time
        }

        Destroy(gameObject); // Projectile is consumed on hit
    }
}

