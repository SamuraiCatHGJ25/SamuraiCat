using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] private bool[] katanasOwned;
    [SerializeField] private CatAttack catAttack;
    [SerializeField] private CurrencyController currencyController;

    [SerializeField] private Button[] katanaEquipButtons;
    [SerializeField] private Button[] katanaBuyButtons;

    [SerializeField] private Button[] towerBuyButtons;

    [SerializeField] private bool[] archerTowerLevelOwned;
    [SerializeField] private GameObject towerRoot;

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

        towerBuyButtons[0].onClick.AddListener(() =>
        {
            buyTower(0, 0);
        });
        towerBuyButtons[1].onClick.AddListener(() =>
        {
            buyTower(1, 300);
        });
        towerBuyButtons[2].onClick.AddListener(() =>
        {
            buyTower(2, 900);
        });
    }

    public void buyKatana(int katanaId, int amount)
    {
        if (katanasOwned[katanaId] == false && amount < currencyController.GetBalance())
        {
            currencyController.AddBalance(-amount);
            katanasOwned[katanaId] = true;
            katanaEquipButtons[katanaId].interactable = true;
            katanaBuyButtons[katanaId].interactable = false;
        }
    }

    private void buyTower(int towerId, int cost)
    {
        if (archerTowerLevelOwned[towerId] == false && cost < currencyController.GetBalance())
        {
            Debug.Log("Upgrading towers");
            currencyController.AddBalance(-cost);
            archerTowerLevelOwned[towerId] = true;
            towerBuyButtons[towerId].interactable = false;
            foreach (ArcherTower tower in towerRoot.GetComponentsInChildren(typeof(ArcherTower)))
            {
                Debug.Log("Upgrading tower...");
                tower.upgradeArcherTower();
            }
        }
    }

    public void equipKatana(int katanaId)
    {
        catAttack.currentWeaponLevel = katanaId;
    }
}
