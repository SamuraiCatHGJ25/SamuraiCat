using UnityEngine;
using UnityEngine.UI;

public class CameraTitileRotation : MonoBehaviour
{
    [SerializeField] private float rotationSmoothness;
    [SerializeField] private Transform regularRotation;
    [SerializeField] private Transform targetRotation;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button backButton;
    private bool lookAround = false;

    private void Awake()
    {
        creditsButton.onClick.AddListener(() =>
        {
            Switch();
        });
        backButton.onClick.AddListener(() =>
        {
            Switch();
        });
    }

    private void Switch()
    {
        if(lookAround)
        {
            lookAround = false;
        }
        else
        {
            lookAround = true;
        }
    }

    private void Update()
    {
        if(!lookAround)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, regularRotation.rotation, rotationSmoothness * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation.rotation, rotationSmoothness * Time.deltaTime);
        }
    }
}
