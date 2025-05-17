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

    [SerializeField] private Transform attackOffsetObject;

    private bool allowAttack = true;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && allowAttack)
        {
            Attack();
            allowAttack = false;
            CancelInvoke(nameof(reAllowAttack));
            Invoke(nameof(reAllowAttack), attackCooldown[currentWeaponLevel]);
        }
        else
        {
            Debug.Log("AllowAttack: " + allowAttack);
        }
    }

    private void Attack()
    {
        RaycastHit hit;
        Vector3 origin = attackOffsetObject.position;
        Vector3 direction = transform.forward;

        if (Physics.SphereCast(origin, attackRange[currentWeaponLevel], direction, out hit))
        {
            Debug.Log("Hit: " + hit.collider.name + " || Damage: " + attackDamage[currentWeaponLevel]);
            if (hit.collider.gameObject.layer == 7)
            {
                hit.collider.gameObject.GetComponent<HealthController>().damage(attackDamage[currentWeaponLevel]);
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
