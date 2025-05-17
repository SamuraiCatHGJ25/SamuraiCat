using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : MonoBehaviour
{
    [SerializeField] private int currentArcherTowerLevel;

    [SerializeField] private float[] cooldown;
    [SerializeField] private int[] damage;
    [SerializeField] private float[] range;
    [SerializeField] private GameObject[] archerTowerPrefabs;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Vector3 offset;
    [SerializeField] private ArcherAI assignedArcher;
    [SerializeField] private float animationDelay;

    [SerializeField] private bool upgrade = false;

    private void Start()
    {
        ShootOne();
    }

    private void Update()
    {
        if(upgrade)
        {
            upgrade = false;
            upgradeArcherTower();
        }
    }

    private void tryToShootEnemies()
    {
        Transform enemy = findNearestEnemy();

        if(enemy != null && enemy.GetComponent<EnemyHealthController>() != null)
        {
            enemy.GetComponent<EnemyHealthController>().setDamage(damage[currentArcherTowerLevel]);
            GameObject projectile = Instantiate(arrow, transform.position + offset, Quaternion.LookRotation(enemy.position - (transform.position + offset)).normalized);
            assignedArcher.makeArcherShoot(0f);
        }
    }

    private Transform findNearestEnemy()
    {
        List<Transform> interactableList = new List<Transform>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, range[currentArcherTowerLevel], enemyLayerMask);
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

    private void ShootOne()
    {
        tryToShootEnemies();
        Invoke(nameof(ShootTwo), cooldown[currentArcherTowerLevel]);
    }
    private void ShootTwo()
    {
        tryToShootEnemies();
        Invoke(nameof(ShootOne), cooldown[currentArcherTowerLevel]);
    }

    public void upgradeArcherTower()
    {
        currentArcherTowerLevel += 1;
        archerTowerPrefabs[currentArcherTowerLevel - 1].SetActive(false);
        archerTowerPrefabs[currentArcherTowerLevel].SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.darkGreen;
        Gizmos.DrawWireSphere(transform.position, range[currentArcherTowerLevel]);
    }
}
