using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour {

    public Text text;

	void Start () {
        text = GetComponent<Text>();
        text.text = "$" + transform.parent.GetComponent<TypeOfExplosive>().explosive.gameObject.GetComponent<Explosion>().cost;
        //Get the explosive's cost.
	}
}
