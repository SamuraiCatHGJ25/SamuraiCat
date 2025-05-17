using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : MonoBehaviour
{
    [SerializeField] private float cooldown;
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Vector3 offset;

    private void Start()
    {
        InvokeRepeating(nameof(tryToShootEnemies), 0, cooldown);
    }

    private void tryToShootEnemies()
    {
        Transform enemy = findNearestEnemy();

        if(enemy != null && enemy.GetComponent<EnemyHealthController>() != null)
        {
            enemy.GetComponent<EnemyHealthController>().setDamage(damage);
            GameObject projectile = Instantiate(arrow, transform.position + offset, Quaternion.LookRotation(enemy.position - (transform.position + offset)).normalized);
        }
    }

    private Transform findNearestEnemy()
    {
        List<Transform> interactableList = new List<Transform>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, enemyLayerMask);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Transform targetTransform))
            {
                interactableList.Add(targetTransform);
            }
        }
        Transform closestTarget = null;
        foreach (Transform target in interactableList)
        {
            if (closestTarget == null)
            {
                closestTarget = target;
            }
            else if (Vector3.Distance(transform.position, target.position) < Vector3.Distance(transform.position, closestTarget.position))
            {
                closestTarget = target;
            }
        }
        return closestTarget;
    }
}
