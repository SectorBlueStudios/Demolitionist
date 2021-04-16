using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialEnableBombSelection : MonoBehaviour
{
    public GameObject standardBombButton;
    private void OnEnable()
    {
        standardBombButton.GetComponent<Button>().interactable = true;
        standardBombButton.GetComponent<Button>().transition = Selectable.Transition.ColorTint;

        standardBombButton.transform.GetChild(2).gameObject.GetComponent<MovingText>().enabled = true;
    }
}
