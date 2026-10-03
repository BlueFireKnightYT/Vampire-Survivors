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

        // kijkt of de weapon level meer dan 6 is, zo ja gaat die uit de list
        for (int i = upgradeOptions.Count - 1; i >= 0; i--)
        {
            GameObject weaponObj = GameObject.FindGameObjectWithTag(upgradeOptions[i].weaponID);
            if (weaponObj != null)
            {
                Weapon weaponScript = weaponObj.GetComponent<Weapon>();
                WeaponSO weaponSO = weaponScript.weaponData;
                if (weaponScript != null && weaponScript.level >= weaponSO.maxLevel)
                {
                    upgradeOptions.RemoveAt(i);
                }
            }
        }

        // maakt een kopie van de overgebleven upgrades lijst
        List<UpgradeSO> sessionPool = new List<UpgradeSO>(upgradeOptions);

        // voegd de upgrade toe aan de button. als er geen upgrade meer over is word de button disavled
        foreach (Button button in buttons)
        {
            UpgradeCardUI dynamicCard = button.GetComponent<UpgradeCardUI>();

            if (sessionPool.Count == 0)
            {
                button.gameObject.SetActive(false);
                continue;
            }

            button.gameObject.SetActive(true);

            int randomIndex = Random.Range(0, sessionPool.Count);
            UpgradeSO chosenUpgrade = sessionPool[randomIndex];

            chosenUpgrades.Add(chosenUpgrade);
            sessionPool.RemoveAt(randomIndex);

            dynamicCard.title.text = chosenUpgrade.upgradeName;
            dynamicCard.image.sprite = chosenUpgrade.showcaseImage;
            dynamicCard.weaponID = chosenUpgrade.weaponID;
            dynamicCard.weapon = GameObject.FindGameObjectWithTag(chosenUpgrade.weaponID);
        }
    }

    GameObject FindWeaponObject()
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        UpgradeCardUI clickedCard = clickedButton.GetComponent<UpgradeCardUI>();

        clickedCard.weapon = GameObject.FindGameObjectWithTag(clickedCard.weaponID);

        return clickedCard.weapon;
    }

    IUpgradable FindUpgradeInterface()
    {
        IUpgradable upgradeInterface = FindWeaponObject().GetComponent<IUpgradable>();

        return upgradeInterface;
    }

    public void UpgradeButtonClick()
    {
        FindUpgradeInterface().Upgrade();

        upgradeMenu.SetActive(false);
        Time.timeScale = 1;
        chosenUpgrades.Clear();
    }
}
