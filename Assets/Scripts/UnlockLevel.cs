using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockExplosive : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        UnlockExplosives();
    }

    // Update is called once per frame
    void UnlockExplosives()
    {
        
        if (player.GetComponent<Player>().playerLevel >= 1)
            ExplosiveUnlock(1);
        if (player.GetComponent<Player>().playerLevel >= 2)
            ExplosiveUnlock(4);
        if (player.GetComponent<Player>().playerLevel >= 3)
            ExplosiveUnlock(2);
        if (player.GetComponent<Player>().playerLevel >= 4)
            ExplosiveUnlock(7);
        if (player.GetComponent<Player>().playerLevel >= 5)
            ExplosiveUnlock(5);
        if (player.GetComponent<Player>().playerLevel >= 6)
            ExplosiveUnlock(8);
        if (player.GetComponent<Player>().playerLevel >= 7)
            ExplosiveUnlock(3);
        if (player.GetComponent<Player>().playerLevel >= 8)
            ExplosiveUnlock(6);
        if (player.GetComponent<Player>().playerLevel >= 9)
            ExplosiveUnlock(9);
}
    public void ExplosiveUnlock(int explosiveNumber)
    {
        explosiveNumber--;
        transform.GetChild(explosiveNumber).gameObject.GetComponent<ExplosiveMenu>().ExplosiveUnlocked(); //Unlock the explosive
    }
}