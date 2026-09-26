using UnityEngine;
using UnityEngine.UI;

public class PlayerXpSystem : MonoBehaviour
{
    public float lvlOneNeededXp;
    public float neededXpMultiplier;
    float nextLevelXp;

    public float xp;
    float totalXp;

    Slider xpBar;
    public GameObject upgradeMenu;

    private void Start()
    {
        xpBar = GameObject.FindGameObjectWithTag("XpBar").GetComponent<Slider>();
        nextLevelXp = lvlOneNeededXp;
        xp = 0;
    }



    public void GetXp(float xpAmount)
    {
        xp += xpAmount;
        totalXp += xpAmount; //possible fun stat for death screen

        if (xp >= nextLevelXp)
        {
            LevelUp();
        }

        float xpPercent = (xp / nextLevelXp) * 100;
        xpBar.value = xpPercent;
    }

    void LevelUp()
    {
        float extraXp = xp - nextLevelXp;
        xp = 0 + extraXp;

        nextLevelXp *= neededXpMultiplier;

        upgradeMenu.SetActive(true);
        UpgradeManager.instance.RetrieveUpgradeData();
        Time.timeScale = 0f;
    }
}
