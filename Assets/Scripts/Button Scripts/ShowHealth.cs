using UnityEngine;
using UnityEngine.UI;

public class ShowHealth : MonoBehaviour {

    public Button button;
    public Animator anim;
    
	void Start () {
        button = GetComponent<Button>();        
	}
	
    public void ShowHealthBars()
    {
        GameObject[] bars = GameObject.FindGameObjectsWithTag("Destructible");
        for(int i = 0; i < bars.Length; i++)
        {
            if (bars[i].transform.GetChild(0).transform.GetChild(0) != null && bars[i].transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Animator>() != null)
            {
                bars[i].transform.GetChild(0).gameObject.SetActive(true);
                bars[i].transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("hide", false);
            }
        }
    }

    public void HideHealthBars()
    {
        GameObject[] bars = GameObject.FindGameObjectsWithTag("Destructible");
        for (int i = 0; i < bars.Length; i++)
        {
            if (bars[i].transform.GetChild(0).transform.GetChild(0) != null && bars[i].transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Animator>() != null)
            {
                bars[i].transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("hide", true);
            }
        }
    }
}