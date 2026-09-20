using System.Collections.Generic;
using UnityEngine;

public class PlayerXpSystem : MonoBehaviour
{
    public float lvlOneNeededXp;
    public float neededXpMultiplier;
    float nextLevelXp;

    public float xp;
    float totalXp;

    private void Start()
    {
        nextLevelXp = lvlOneNeededXp;
        xp = 0;
    }



    public void GetXp(float xpAmount)
    {
        xp += xpAmount;
        totalXp += xpAmount; //possible fun stat for death screen

        if (xp >= nextLevelXp)
        {
            nextLevelXp *= neededXpMultiplier;
            LevelUp();
        }
    }

    void LevelUp()
    {
        Debug.Log("Level Up. New goal is " + nextLevelXp + " Xp" );
    }
}
