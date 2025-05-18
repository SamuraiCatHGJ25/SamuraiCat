using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] private int[] katanaPriceConfig;
    [SerializeField] private int[] towerPriceConfig;
    [SerializeField] private int[] magicPriceConfig;

    [SerializeField] private bool[] katanasOwned;
    [SerializeField] private bool[] magicOwned;
    [SerializeField] private bool[] archerTowerLevelOwned;

    [SerializeField] private Button[] katanaEquipButtons;
    [SerializeField] private Button[] katanaBuyButtons;
    
    [SerializeField] private Button[] magicEquipButtons;
    [SerializeField] private Button[] magicBuyButtons;

    [SerializeField] private Button[] towerBuyButtons;
    [SerializeField] private GameObject towerRoot;

    [SerializeField] private CatAttack catAttack;
    [SerializeField] private CurrencyController currencyController;
    [SerializeField] private SpellCaster spellController;

    private void Awake()
    {
        for (var i = 0; i < katanaEquipButtons.Length; i++)
        {
            var offset = i;
            katanaEquipButtons[i].onClick.AddListener(() =>
            {
                equipKatana(offset + 1);
            });
        }

        for (var i = 0; i < katanaBuyButtons.Length; i++)
        {
            var offset = i;
            katanaBuyButtons[i].onClick.AddListener(() =>
            { 
                buyKatana(offset, katanaPriceConfig[offset]);
            });
        }

        for (var i = 0; i < towerBuyButtons.Length; i++)
        {
            var offset = i;
            towerBuyButtons[i].onClick.AddListener(() =>
            { 
                buyTower(offset, towerPriceConfig[offset]);
            });
        }
    }

    public void buyKatana(int katanaId, int price)
    {
        if (katanasOwned[katanaId] == false && price < currencyController.GetBalance())
        {
            currencyController.AddBalance(-price);
            katanasOwned[katanaId] = true;
            katanaEquipButtons[katanaId].interactable = true;
            katanaBuyButtons[katanaId].interactable = false;
        }
    }

    private void buyTower(int towerId, int price)
    {
        if (archerTowerLevelOwned[towerId] == false && price < currencyController.GetBalance())
        {
            Debug.Log("Upgrading towers");
            currencyController.AddBalance(-price);
            archerTowerLevelOwned[towerId] = true;
            towerBuyButtons[towerId].interactable = false;
            foreach (ArcherTower tower in towerRoot.GetComponentsInChildren(typeof(ArcherTower)))
            {
                Debug.Log("Upgrading tower...");
                tower.upgradeArcherTower();
            }
        }
    }

    public void buyMagic(int magicId, int price)
    {
        if (katanasOwned[magicId] == false && price < currencyController.GetBalance())
        {
            currencyController.AddBalance(-price);
            katanasOwned[magicId] = true;
            katanaEquipButtons[magicId].interactable = true;
            katanaBuyButtons[magicId].interactable = false;
        }
    }

    private void equipMagic(int magicId)
    {
        
    }

    public void equipKatana(int katanaId)
    {
        catAttack.currentWeaponLevel = katanaId;
    }
}
