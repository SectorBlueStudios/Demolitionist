using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnableBombMove : MonoBehaviour
{
    public GameObject animationBomb;
    public GameObject bomb;

    public Animator anim;
    public void OnEnable()
    {
        animationBomb.SetActive(true);
        bomb.SetActive(false);
    }
    public void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("still"))
        {
            animationBomb.SetActive(false);
            bomb.SetActive(true);
            GetComponent<TutorialChangeText>().NextText();
        }
    }
}
