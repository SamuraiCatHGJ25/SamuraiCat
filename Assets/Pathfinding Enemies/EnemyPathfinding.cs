using Pathfinding;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private Transform player;

    private AIDestinationSetter destinationSetter;

    void Start()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();

        if (player == null)
        {
            enabled = false;
            Debug.LogError("No player for enemy to follow");
            return;
        }

        destinationSetter.target = player;
    }
}
