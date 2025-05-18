using System.Runtime.CompilerServices;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float smoothness;
    [SerializeField] private Vector3 offset;
    private float regularFieldOfVision;
    private Quaternion regularRotation;
    [SerializeField] private Camera cameraComponent;
    [Header("_")]
    [Header("Shop Settings")]
    [Header("_")]
    [SerializeField] private int currentShopValue;
    [SerializeField] private Transform[] cameraShopTransforms;
    [SerializeField] private float shopFieldOfVision = 60f;
    [SerializeField] private float shopTransitionSmoothness = 1f;

    private void Start()
    {
        regularFieldOfVision = cameraTransform.GetComponent<Camera>().fieldOfView;
        regularRotation = transform.rotation;
    }
    private void Update()
    {
        offset.y += Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 300;
        offset.y = Mathf.Clamp(offset.y, 10, 30);
        offset.z = -1f * offset.y * 1.1f;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            currentShopValue = 0;
        }

        if(currentShopValue == 0)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetTransform.position + offset, smoothness * Time.deltaTime);
            cameraComponent.fieldOfView = Mathf.Lerp(cameraComponent.fieldOfView, regularFieldOfVision, shopTransitionSmoothness * Time.deltaTime);
            cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, regularRotation, smoothness * Time.deltaTime);
        }
        else
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraShopTransforms[currentShopValue].position, shopTransitionSmoothness * Time.deltaTime);
            cameraComponent.fieldOfView = Mathf.Lerp(cameraComponent.fieldOfView, shopFieldOfVision, shopTransitionSmoothness * Time.deltaTime);
            cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, cameraShopTransforms[currentShopValue].rotation, shopTransitionSmoothness * Time.deltaTime);
        }
    }

    public void SetShop(int value)
    {
        currentShopValue = value;
    }
}
