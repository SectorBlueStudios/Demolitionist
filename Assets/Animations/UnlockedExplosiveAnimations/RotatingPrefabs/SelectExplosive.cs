using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectExplosive : MonoBehaviour
{
    public GameObject player;
    public GameObject[] explosiveList = new GameObject[9];

    void Start()
    {
        int bombIndex = player.GetComponent<Player>().playerLevel - 1;

        GameObject unlockedExplosive;
        unlockedExplosive = Instantiate(explosiveList[bombIndex], transform);
        unlockedExplosive.transform.SetParent(transform);
    }
}
