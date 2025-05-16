using UnityEngine;

public class Interactor : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;   
    }

    public void Interact(Transform interactorTransform)
    {
        Debug.Log("Interact signal received");
    }
}
