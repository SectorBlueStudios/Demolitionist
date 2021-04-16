using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RectTESTING : MonoBehaviour {

    public Rect testRECT;
    public Button button;
    private RectTransform butrect;
    
    // Use this for initialization
	void Start () {
        button = GetComponent<Button>();
        testRECT = button.GetComponent<RectTransform>().rect;
        //testRECT = new Rect(0, 0, 20, 20);
        butrect = button.GetComponent<RectTransform>();

	}
	
	// Update is called once per frame
	void Update () {
        //testRECT.center = button.transform.position;
        //testRECT.center = new Vector2(button.transform.position.x, button.transform.position.y);
        testRECT = button.GetComponent<RectTransform>().rect;
        testRECT.center = new Vector2(butrect.position.x, Screen.height + -butrect.position.y);

        Debug.Log(testRECT.center);
        Debug.Log(butrect.position);
        Debug.Log(butrect.localPosition);



    }

    private void OnGUI()
    {
        GUI.Button(testRECT, "TRASH");
    }


}
