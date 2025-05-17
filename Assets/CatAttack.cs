using Unity.VisualScripting;
using UnityEngine;

public class CatAttack : MonoBehaviour
{
    [SerializeField] private int currentWeaponLevel;

    [SerializeField] private int[] attackDamage;
    [SerializeField] private float[] attackRange;
    [SerializeField] private float[] attackCooldown;
    [SerializeField] private ParticleSystem[] attackEffect;
    [SerializeField] private int[] attackEffectDuration;
    [SerializeField] private GameObject[] sword;
    [SerializeField] private LayerMask enemyLayerMask;

    [SerializeField] private Transform attackOffsetObject;

    private int lastSwordValue;
    private bool allowAttack = true;

    private void Start()
    {
        lastSwordValue = 1;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && allowAttack)
        {
            Attack();
            allowAttack = false;
            CancelInvoke(nameof(reAllowAttack));
            Invoke(nameof(reAllowAttack), attackCooldown[currentWeaponLevel]);
        }

        if (lastSwordValue != currentWeaponLevel)
        {
            lastSwordValue = currentWeaponLevel;
            foreach (GameObject sword in sword)
            {
                if (sword != null)
                {
                    sword.SetActive(false);
                }
            }

            if (sword[currentWeaponLevel] != null)
            {
                sword[currentWeaponLevel].SetActive(true);
            }
        }
    }

    private void Attack()
    {
        RaycastHit hit;
        Vector3 origin = attackOffsetObject.position;
        Vector3 direction = transform.forward;

        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange[currentWeaponLevel], enemyLayerMask);
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<EnemyHealthController>() != null)
            {
                collider.GetComponent<EnemyHealthController>().setDamage(attackDamage[currentWeaponLevel]);
            }
        }

        attackEffect[currentWeaponLevel].Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackOffsetObject.position, attackRange[currentWeaponLevel]);
    }

    private void reAllowAttack()
    {
        allowAttack = true;
    }
}
