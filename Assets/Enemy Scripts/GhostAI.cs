using JetBrains.Annotations;
using Pathfinding;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostAI : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;

    [SerializeField] private float movementSpeed;
    [SerializeField] private int damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float slowdownMultiplier;
    [SerializeField] private ParticleSystem ghostDamageEffect;

    [SerializeField] private float nearestTargetCheckRate = 0.5f;

    private AIDestinationSetter destinationSetter;

    void Start()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        InvokeRepeating(nameof(setPathfindingTarget), 0, nearestTargetCheckRate);
    }

    private void setPathfindingTarget()
    {
        Transform nearestTarget = findNearestTarget();

        if (nearestTarget != null)
        {
            destinationSetter.target = nearestTarget;
        }
    }

    private Transform findNearestTarget()
    {
        List<Transform> interactableList = new List<Transform>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, 100, targetLayer);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Transform targetTransform))
            {
                interactableList.Add(targetTransform);
            }
        }
        Transform closestTarget = null;
        foreach(Transform target in interactableList)
        {
            if(closestTarget == null)
            {
                closestTarget = target;
            }
            else if(Vector3.Distance(transform.position, target.position) < Vector3.Distance(transform.position, closestTarget.position))
            {
                closestTarget = target;
            }
        }

        if(closestTarget != null && Vector3.Distance(transform.position, closestTarget.position) < attackRange)
        {
            attackPlayer(closestTarget);
        }

        return closestTarget;
    }

    private void attackPlayer(Transform target)
    {
        ghostDamageEffect.Play();
        target.GetComponent<HealthController>()?.damage(damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.darkRed;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
