using System.Runtime.CompilerServices;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float smoothness;
    [SerializeField] private Vector3 offset;

    private void Update()
    {
         cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetTransform.position + offset, smoothness * Time.deltaTime); 
    }
}
