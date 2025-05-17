using Pathfinding;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private Transform targetPlayer;
    [SerializeField] private float speed;

    private AIDestinationSetter destinationSetter;

    void Start()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();

        if (targetPlayer == null)
        {
            enabled = false;
            Debug.LogError("No player for enemy to follow");
            return;
        }

        destinationSetter.target = targetPlayer;
    }
}
