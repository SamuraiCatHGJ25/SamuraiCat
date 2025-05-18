using UnityEngine;

public class Interactor : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    [SerializeField] private Movement movement;
    [SerializeField] private bool isShopKeeper = false;
    [SerializeField] private bool inShop = false;
    [SerializeField] private GameObject userInterface;
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
        if(!isShopKeeper) { return; }
        if(!inShop)
        {
            movement.SetShop(1);
            inShop = true;
            userInterface.SetActive(true);
        }
        else
        {
            movement.SetShop(0);
            inShop = false;
            userInterface.SetActive(false);
        }
    }
}
