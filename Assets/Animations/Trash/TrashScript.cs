using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScript : MonoBehaviour {

    private Animator anim;
    public bool toggle;

    // Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(toggle == true)
        {
            anim.SetBool("inTrashArea", true);
        }
        else
        {
            anim.SetBool("inTrashArea", false);
        }
	}
}
