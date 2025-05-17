using UnityEditor.Rendering;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private CurrencyController currencyController;
    [SerializeField] private int healthPoints;
    private int currentHealthPoints;
    [SerializeField] private ParticleSystem deathEffect;
    [SerializeField] private float timeBeforeDespawn;

    private void Start()
    {
        currentHealthPoints = healthPoints;
    }

    public void setDamage(int damage)
    {
        currentHealthPoints -= damage;
        Debug.Log("received damage");
        if(currentHealthPoints <= 0)
        {
            // TODO: Implement variable balance based on enemy type
            currencyController.AddBalance(1);

            deathEffect.Play();
            Destroy(gameObject, timeBeforeDespawn);
        }
    }
}
