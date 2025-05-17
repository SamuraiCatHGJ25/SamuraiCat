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
            offset.y += Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 300;
            offset.y = Mathf.Clamp(offset.y, 10, 30);
            offset.z = -1f * offset.y * 1.1f;
         cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetTransform.position + offset, smoothness * Time.deltaTime); 
    }
}
