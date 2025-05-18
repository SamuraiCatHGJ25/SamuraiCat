using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ArrowScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool damagingArrow = false;
    [SerializeField] private int arrowDamage;

    private void Start()
    {
        Destroy(gameObject, 3f);    
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (damagingArrow)
        {
            other.gameObject.GetComponent<HealthController>()?.damage(arrowDamage);
            if (other.gameObject.GetComponent<HealthController>() != null)
            {
                Destroy(gameObject, 0.1f);
            }
        }
    }
}
