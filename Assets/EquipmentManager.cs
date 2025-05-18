using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] private int money;

    [SerializeField] private bool[] katanasOwned;
    [SerializeField] private CatAttack catAttack;

    [SerializeField] private Button[] katanaEquipButtons;
    [SerializeField] private Button[] katanaBuyButtons;

    [SerializeField] private bool[] msOwned;
    [SerializeField] private int msEquipped;

    [SerializeField] private bool[] archerTowerLevelOwned;

    private void Awake()
    {
        katanaEquipButtons[0].onClick.AddListener(() =>
        {
            equipKatana(1);
        });
        katanaEquipButtons[1].onClick.AddListener(() =>
        {
            equipKatana(2);
        });
        katanaEquipButtons[2].onClick.AddListener(() =>
        {
            equipKatana(3);
        });
        katanaBuyButtons[0].onClick.AddListener(() =>
        {
            buyKatana(0, 0);
        });
        katanaBuyButtons[1].onClick.AddListener(() =>
        {
            buyKatana(1, 300);
        });
        katanaBuyButtons[2].onClick.AddListener(() =>
        {
            buyKatana(2, 900);
        });
    }

    public void buyKatana(int katanaId, int amount)
    {
        if (katanasOwned[katanaId] == false && amount < money)
        {
            money -= amount;
            katanasOwned[katanaId] = true;
            katanaEquipButtons[katanaId].interactable = true;
            katanaBuyButtons[katanaId].interactable = false;
        }
    }

    public void equipKatana(int katanaId)
    {
        catAttack.currentWeaponLevel = katanaId;
    }

    public void buyMs(int id, int amount)
    {
        if (msOwned[id] == false && amount < money)
        {
            money -= amount;
            katanasOwned[id] = true;
            katanaEquipButtons[id].interactable = true;
            katanaBuyButtons[id].interactable = false;
        }
    }

    public void equipMs(int id)
    {
        catAttack.currentWeaponLevel = id;
    }
}
