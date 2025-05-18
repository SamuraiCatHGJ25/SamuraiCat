using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcherAI : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;

    [SerializeField] private int damage;
    [SerializeField] private float attackRange;
    [SerializeField] private GameObject arrowObject;
    [SerializeField] private AIPath aiPath;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float shootSpeedMultiplier;
    [SerializeField] private float slowDownTime = 1;
    [SerializeField] private Animator animator;
    [SerializeField] private float arrowBackOffset = 3f;

    [SerializeField] private float nearestTargetCheckRate = 0.5f;

    private AIDestinationSetter destinationSetter;

    void Start()
    {
        SetAISpeed(normalSpeed);
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

        if (closestTarget != null && Vector3.Distance(transform.position, closestTarget.position) < attackRange)
        {
            attackPlayer(closestTarget);
            SetAISpeed(shootSpeedMultiplier);
            CancelInvoke(nameof(resetAISpeed));
            Invoke(nameof(resetAISpeed), slowDownTime);
        }

        return closestTarget;
    }

    private void attackPlayer(Transform target)
    {
        Instantiate(arrowObject, transform.position - transform.forward * arrowBackOffset, Quaternion.LookRotation(target.position - transform.position));
        animator.SetBool("Shooting", true);
        //target.GetComponent<HealthController>()?.damage(damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.darkRed;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void SetAISpeed(float speed)
    {
        aiPath.maxSpeed = speed;
    }

    private void resetAISpeed()
    {
        aiPath.maxSpeed = normalSpeed;
        animator.SetBool("Shooting", false);
    }
}
