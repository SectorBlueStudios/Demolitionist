using UnityEngine;

public class Deselect_Level : MonoBehaviour
{
    public GameObject level;
    public void NonLevelClick()
    {
        level.SetActive(false);
    }
}