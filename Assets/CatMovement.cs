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
        float multiplier = 1;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            multiplier = 3;
        }

        Vector3 targetMovement = new Vector3(Input.GetAxis("Horizontal") * speedHorizontal * multiplier, 0, Input.GetAxis("Vertical") * speedVertical * multiplier);
        smoothTargetMovement = Vector3.Lerp(smoothTargetMovement, targetMovement, movementSmoothness * Time.deltaTime);

        characterController.Move(smoothTargetMovement * Time.deltaTime);
    }
}
