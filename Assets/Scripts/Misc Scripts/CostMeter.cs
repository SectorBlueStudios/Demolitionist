using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CostMeter : MonoBehaviour {

    public int totalCost;

    public GameObject costSource;
    public GameObject maxCost;

    public TextMeshProUGUI text;
    public Slider slider;

    public RectTransform gradient;

    public void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        slider = GetComponent<Slider>();
        slider.maxValue = maxCost.GetComponent<CostValues>().costMax;
    }

    private void Update()
    {
        totalCost = costSource.GetComponent<Drag>().cost;
        text.text = "$" + totalCost;
        slider.value = totalCost;

        gradient.offsetMax = new Vector2(-Mathf.Lerp(486, 34, slider.value / maxCost.GetComponent<CostValues>().costMax), gradient.offsetMax.y);
    }
}