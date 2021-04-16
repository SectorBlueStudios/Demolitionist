using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnableTrash : MonoBehaviour
{
    public GameObject bombAnimation;
    public GameObject mainBomb;
    public void OnEnable()
    {
        mainBomb.SetActive(false);
        bombAnimation.SetActive(true);
    }
}
