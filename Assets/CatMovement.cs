using UnityEngine;
using UnityEngine.EventSystems;

public class CatMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speedHorizontal;
    [SerializeField] private float speedVertical;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSmoothness;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashMultiplier;
    [SerializeField] private float dashCooldown;

    private Vector3 smoothTargetMovement;
    private bool canDash = true;
    private float multiplier = 1;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && canDash)
        {
            canDash = false;
            multiplier = dashMultiplier;
            CancelInvoke(nameof(DisableDash));
            CancelInvoke(nameof(EnableDash));
            Invoke(nameof(DisableDash), dashDuration);
            Invoke(nameof(EnableDash), dashCooldown);
        }

        Vector3 targetMovement = new Vector3(Input.GetAxis("Horizontal") * speedHorizontal * multiplier, 0, Input.GetAxis("Vertical") * speedVertical * multiplier);
        Vector3 eulerTargetRotation = targetMovement.normalized;

        smoothTargetMovement = Vector3.Lerp(smoothTargetMovement, targetMovement, movementSmoothness * Time.deltaTime);

        characterController.Move(smoothTargetMovement * Time.deltaTime);

        if (eulerTargetRotation != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(eulerTargetRotation);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    private void DisableDash()
    {
        multiplier = 1;
    }

    private void EnableDash()
    {
        canDash = true;
    }
}
