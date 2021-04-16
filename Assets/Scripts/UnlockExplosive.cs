using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLevel : MonoBehaviour
{
    public GameObject player;
        
    // Start is called before the first frame update
    void Start()
    {
        if (player.GetComponent<Player>().playerLevel > 1)
            UnlockExplosive(1);
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UnlockExplosive(int explosiveNumber)
    {
        explosiveNumber--;
        transform.GetChild(explosiveNumber).gameObject.GetComponent<ExplosiveMenu>().ExplosiveUnlocked(); //Unlock the explosive
    }

}
