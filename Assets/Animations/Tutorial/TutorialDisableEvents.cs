using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialDisableEvents : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<EventTrigger>().enabled = false;
    }
}
