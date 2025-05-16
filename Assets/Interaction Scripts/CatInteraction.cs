using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CatInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactRange = 4f;
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private float interactionCheckRate = 0.1f;
    [SerializeField] private GameObject interactUiGameObject;
    [SerializeField] private TextMeshProUGUI interactUiText;
    [SerializeField] private Vector3 interactUiOffset;
    [SerializeField] private Transform cameraTransform;

    private void Start()
    {
        //interactUiGameObject = GameObject.FindGameObjectWithTag("InteractUI");
        InvokeRepeating(nameof(CheckForInteractableObject), interactionCheckRate, interactionCheckRate);
    }

    private void Update()
    { 

        if (Input.GetKeyDown(KeyCode.E))
        {
            IInteractable interactable = GetInteractableObject();
            if (interactable != null)
            {
                interactable.Interact(transform);
            }
        }

        if (interactUiGameObject != null)
        {
            interactUiGameObject.transform.LookAt(cameraTransform.position);
        }
    }

    public IInteractable GetInteractableObject()
    {
        List<IInteractable> interactableList = new List<IInteractable>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange, interactionLayer);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                interactableList.Add(interactable);
            }
        }

        IInteractable closest = null;
        foreach (IInteractable interactable in interactableList)
        {
            if (closest == null)
            {
                closest = interactable;
            }
            else if (Vector3.Distance(transform.position, interactable.GetTransform().position) < Vector3.Distance(transform.position, closest.GetTransform().position))
            {
                closest = interactable;
            }
        }

        return closest;
    }

    private void CheckForInteractableObject()
    {
        IInteractable interactable = GetInteractableObject();
        if (interactable != null)
        {
            interactUiGameObject.transform.position = interactable.GetTransform().position + interactUiOffset;
            interactUiGameObject.SetActive(true);
            interactUiText.text = interactable.GetInteractText();
        }
        else
        {
            interactUiGameObject.SetActive(false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = GetInteractableObject() == null ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
