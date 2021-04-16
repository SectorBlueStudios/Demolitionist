using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowMoveDEBUG : MonoBehaviour {

    public float testHoriz;
	void Start () {
        //testHoriz = 
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<RectTransform>().position = new Vector3(GetComponent<RectTransform>().position.x + 0.5f, GetComponent<RectTransform>().position.y);
	}
}
