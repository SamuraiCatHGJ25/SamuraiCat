using TMPro;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    [SerializeField] private int balance;

    public int GetBalance() => balance;

    public void AddBalance(int amount)
    {
        balance += amount;
    }
}