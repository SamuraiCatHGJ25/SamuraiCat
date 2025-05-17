using System;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private SamuraiAnimation samuraiAnimation;
    [SerializeField] private float speedHorizontal;
    [SerializeField] private float speedVertical;
    [SerializeField] private float jumpForce;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSmoothness;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashMultiplier;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float gravityMultiplier;

    private Vector3 smoothTargetMovement;
    private bool canDash = true;
    private bool applyGravity = true;
    private float multiplier = 1;
    private float jumpMultiplier;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && canDash)
        {
            Debug.Log("Dash triggered");
            canDash = false;
            multiplier = dashMultiplier;
            CancelInvoke(nameof(DisableDash));
            CancelInvoke(nameof(EnableDash));
            Invoke(nameof(DisableDash), dashDuration);
            Invoke(nameof(EnableDash), dashCooldown);
        }

        if (Input.GetButtonDown("Jump") && characterController.isGrounded)
        {
            Debug.Log("Jump triggered");
            applyGravity = false;
            CancelInvoke(nameof(DisableJump));
            Invoke(nameof(DisableJump), 0.15f);
        }

        jumpMultiplier = Input.GetButton("Jump") && !applyGravity ? 1 : 0;
        
        float horizontal = Input.GetAxis("Horizontal") * speedHorizontal * multiplier;
        float vertical = Input.GetAxis("Vertical") * speedVertical * multiplier;
        float jump =  jumpMultiplier * jumpForce * multiplier;
        
        Vector3 targetMovement = new Vector3(horizontal, jump, vertical);
        if (characterController.isGrounded == false && applyGravity)
        {
            //Add our gravity Vecotr
            targetMovement += Physics.gravity* gravityMultiplier;
            jumpMultiplier = 0;
        }

        Vector3 eulerTargetRotation = targetMovement.normalized;

        smoothTargetMovement = Vector3.Lerp(smoothTargetMovement, targetMovement, movementSmoothness * Time.deltaTime);

        samuraiAnimation.UpdateAnimation(Math.Abs(horizontal) > 0 || Math.Abs(vertical) > 0);
        characterController.Move(smoothTargetMovement * Time.deltaTime);

        if (eulerTargetRotation != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(eulerTargetRotation);
            var rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
            
            transform.rotation = new Quaternion(rotation.x, rotation.y, transform.rotation.z, rotation.w);
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

    private void DisableJump()
    {
        applyGravity = true;
    }
}
