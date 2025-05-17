using UnityEngine;

public class CatAttack : MonoBehaviour
{
    [SerializeField] private int currentAttackValue;

    [SerializeField] private int[] attackDamage;
    [SerializeField] private float[] attackRange;
    [SerializeField] private float[] attackCooldown;
    [SerializeField] private ParticleSystem[] attackEffect;
    [SerializeField] private int[] attackEffectDuration;

    [SerializeField] private Transform attackOffsetObject;

    private bool allowAttack;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && allowAttack)
        {
            Attack();
            allowAttack = false;
            CancelInvoke(nameof(reAllowAttack));
            Invoke(nameof(allowAttack), attackCooldown[currentAttackValue]);
        }
    }

    private void Attack()
    {
        RaycastHit hit;
        Vector3 origin = attackOffsetObject.position;
        Vector3 direction = transform.forward;

        if (Physics.SphereCast(origin, attackRange[currentAttackValue], direction, out hit))
        {
            Debug.Log("Hit: " + hit.collider.name + " || Damage: " + attackDamage[currentAttackValue]);
        }

        attackEffect[currentAttackValue].Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackOffsetObject.position, attackRange[currentAttackValue]);
    }

    private void reAllowAttack()
    {
        allowAttack = true;
    }
}
