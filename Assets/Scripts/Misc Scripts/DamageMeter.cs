using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageMeter : MonoBehaviour {

    public float totalDamage;
    public float currentDamage;
    public GameObject[] destructables;
    public Slider slider;
    public TextMeshProUGUI text;
    public RectTransform gradient;

    public float percent;
	void Start () {

        destructables = GameObject.FindGameObjectsWithTag("Destructible");

        for (int i = 0; i < destructables.Length; i++)
        {
            if (destructables[i].GetComponent<Properties>() != null)
            {
                totalDamage = totalDamage + destructables[i].GetComponent<Properties>().initialDurability;
            }
        }
        //Debug.Log(totalDamage);

        slider = GetComponent<Slider>();
        slider.maxValue = totalDamage;

        text = GetComponentInChildren<TextMeshProUGUI>();
	}
	
	void Update () {
		destructables = GameObject.FindGameObjectsWithTag("Destructible");
        currentDamage = 0;
        for (int i = 0; i < destructables.Length; i++)
        {
            if (destructables[i].GetComponent<Properties>() != null)
            {
                currentDamage = currentDamage + destructables[i].GetComponent<Properties>().currentDurability;
            }
        }        
        slider.value = currentDamage;
        gradient.offsetMax = new Vector2(-Mathf.Lerp(34,486,slider.value / totalDamage),gradient.offsetMax.y);
        percent = (100 - Mathf.RoundToInt((currentDamage / totalDamage) * 100));
        text.text = "" + percent + "%";
    }
}