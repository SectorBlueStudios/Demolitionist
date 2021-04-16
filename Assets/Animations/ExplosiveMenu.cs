using UnityEngine;
using UnityEngine.UI;

public class ExplosiveMenu : MonoBehaviour {

    public Animator anim;
    public GameObject panel;

    public GameObject[] explosiveTriggers;

    void Start()
    {
        anim = GetComponent<Animator>();
        explosiveTriggers = GameObject.FindGameObjectsWithTag("Button");

        ExplosiveLocked();
    }

    public void ExplosiveLocked()
    {
        //This is tied to Explosives -- If the LOCK (GetChild(0)) is turned on, that means the level is locked and it must
        //  turn off the tag of "Button" so no Rects are created. Also, it must turn off the button itself and the subtracting text
        if (transform.childCount != 0 && transform.GetChild(0).gameObject.activeSelf == true && tag == "Button")
        {
            tag = "Untagged";
            GetComponent<Button>().enabled = false;
            transform.GetChild(2).gameObject.SetActive(false); // Subtracting text
        }
    }
    public void ExplosiveUnlocked()
    {
        tag = "Button";
        GetComponent<Button>().enabled = true;
        transform.GetChild(2).gameObject.SetActive(true); // Subtracting text
        transform.GetChild(0).gameObject.SetActive(false); //turn off unlock image
    }


    public void ToggleMenu()
    {
        /*
        for(int i = 0; i < explosiveTriggers.Length; i++)
        {
            if (explosiveTriggers[i].GetComponent<Animator>().GetBool("toggle") == true)
            {
                explosiveTriggers[i].GetComponent<Animator>().SetBool("toggle", false);
            }
            else
            {
                explosiveTriggers[i].GetComponent<Animator>().SetBool("toggle", true);
            }
        }*/

        if(panel.GetComponent<Animator>().GetBool("toggle") == true)
            panel.GetComponent<Animator>().SetBool("toggle", false);
        else
            panel.GetComponent<Animator>().SetBool("toggle", true);

        /*
        if (panel.GetComponent<ExplosiveMenu>().anim.GetBool("toggle") == true)
        {
            panel.GetComponent<ExplosiveMenu>().anim.SetBool("toggle", false);
        }
        else
        {
            panel.GetComponent<ExplosiveMenu>().anim.SetBool("toggle", true);
        }
        */
    }
    public void ToggleMenuDown()
    {
        panel.GetComponent<Animator>().SetBool("toggle", false);
    }
}
