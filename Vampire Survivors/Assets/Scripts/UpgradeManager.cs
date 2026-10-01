using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance { get; private set; }
    public List<Button> buttons = new List<Button>();
    public List<UpgradeSO> upgradeOptions = new List<UpgradeSO>();
    public List<UpgradeSO> chosenUpgrades = new List<UpgradeSO>();
    public GameObject upgradeMenu;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void RetrieveUpgradeData()
    {
        foreach (Button button in buttons)
        {
            UpgradeCardUI dynamicCard = button.GetComponent<UpgradeCardUI>();
            UpgradeSO chosenUpgrade;

            do
            {
                chosenUpgrade = upgradeOptions[Random.Range(0, upgradeOptions.Count)];
            }
            while (chosenUpgrades.Contains(chosenUpgrade));

            chosenUpgrades.Add(chosenUpgrade);

            dynamicCard.title.text = chosenUpgrade.upgradeName;
            dynamicCard.image.sprite = chosenUpgrade.showcaseImage;
            dynamicCard.weaponID = chosenUpgrade.weaponID;
        }
    }

    public void UpgradeButtonClick()
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        UpgradeCardUI clickedCard = clickedButton.GetComponent<UpgradeCardUI>();

        clickedCard.weapon = GameObject.FindGameObjectWithTag(clickedCard.weaponID);
        IUpgradable upgradeInterface = clickedCard.weapon.GetComponent<IUpgradable>();

        upgradeInterface.Upgrade();

        upgradeMenu.SetActive(false);
        Time.timeScale = 1;
        chosenUpgrades.Clear();
    }
}
