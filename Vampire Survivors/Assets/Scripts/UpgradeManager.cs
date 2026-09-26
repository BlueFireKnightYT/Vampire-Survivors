using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance { get; private set; }
    public List<Button> buttons = new List<Button>();
    public List<UpgradeSO> upgradeOptions = new List<UpgradeSO>();

    public List<UpgradeSO> chosenUpgrades = new List<UpgradeSO>();
    UpgradeSO chosenUpgrade;

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
            UpgradeCardUI cardComponents = button.GetComponent<UpgradeCardUI>();
            do
            {
                chosenUpgrade = upgradeOptions[Random.Range(0, upgradeOptions.Count)];
            }
            while (chosenUpgrades.Contains(chosenUpgrade));

            chosenUpgrades.Add(chosenUpgrade);

            cardComponents.title.text = chosenUpgrade.upgradeName;
            cardComponents.image.sprite = chosenUpgrade.showcaseImage;

            chosenUpgrade = null;
        }
        chosenUpgrades.Clear();
    }
}
