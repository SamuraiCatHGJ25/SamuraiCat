using TMPro;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    [SerializeField] private int balance;
    [SerializeField] private TextMeshProUGUI text;

    public int GetBalance() => balance;

    public void AddBalance(int amount)
    {
        balance += amount * 10;
        text.text = "Yen: " + balance;
    }
}