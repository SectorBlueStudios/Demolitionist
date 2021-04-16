using UnityEngine;

public class ClearExplosives : MonoBehaviour {

    private GameObject[] explosiveList;
    public GameObject dragScript;

    public void DeleteAllExplosives()
    {
        explosiveList = GameObject.FindGameObjectsWithTag("Explosive");

        for(int i = 0; i < explosiveList.Length; i++)
        {
            dragScript.GetComponent<Drag>().cost = dragScript.GetComponent<Drag>().cost - explosiveList[i].GetComponent<Explosion>().cost;
            Destroy(explosiveList[i]);
        }
    }
}
