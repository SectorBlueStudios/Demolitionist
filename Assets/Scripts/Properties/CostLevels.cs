using UnityEngine;

public class CostLevels : MonoBehaviour {

    public GameObject tierValues;
    
    // Use this for initialization
	void Start () {
        transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(480 * (tierValues.GetComponent<CostValues>().bronzeTier / 100), transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.y);
        transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(480 * (tierValues.GetComponent<CostValues>().silverTier / 100), transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition.y);
        transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector2(480 * (tierValues.GetComponent<CostValues>().goldTier / 100), transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition.y);
	}
}