using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public class TransitionToUnlockExplosive : MonoBehaviour
{

    private void Start()
    {
        TransitionToUnlock();
    }
    /*public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            TransitionToUnlock();

    }*/
    public void TransitionToUnlock()
    {
        GetComponent<Animator>().SetBool("Transition", true);
    }
}
