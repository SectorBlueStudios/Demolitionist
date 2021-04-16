using UnityEngine;

public class DestructionLevels : MonoBehaviour {

    public GameObject tierValues;
    
    // Use this for initialization
	void Start () {
        transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(480 * (tierValues.GetComponent<DestructionValues>().bronzeTier / 100), transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.y);
        transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(480 * (tierValues.GetComponent<DestructionValues>().silverTier / 100), transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition.y);
        transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector2(480 * (tierValues.GetComponent<DestructionValues>().goldTier / 100), transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition.y);
	}
}