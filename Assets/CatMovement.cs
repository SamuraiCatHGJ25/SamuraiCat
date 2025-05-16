using UnityEngine;

public class CatMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speedHorizontal;
    [SerializeField] private float speedVertical;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSmoothness;

    private Vector3 smoothTargetMovement;

    private void Update()
    {
        Vector3 targetMovement = new Vector3(Input.GetAxis("Horizontal") * speedHorizontal, 0, Input.GetAxis("Vertical") * speedVertical);
        smoothTargetMovement = Vector3.Lerp(smoothTargetMovement, targetMovement, movementSmoothness * Time.deltaTime);

        characterController.Move(smoothTargetMovement * Time.deltaTime);
    }
}
