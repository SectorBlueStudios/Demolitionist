using UnityEngine;

public class HealthBar : MonoBehaviour {

    public float healthPercentage;

    private void Start()
    {
        Invoke("DeactivateHealthBar", 3);
    }

    void Update () {
        healthPercentage = GetComponentInParent<Properties>().currentDurability / GetComponentInParent<Properties>().initialDurability;
        transform.localScale = new Vector3(healthPercentage, transform.localScale.y, transform.localScale.z);
        transform.parent.position = new Vector3(transform.parent.parent.position.x, transform.parent.position.y, transform.parent.parent.position.z);
    }

    public void ActivateHealthBar()
    {
        CancelInvoke("DeactivateHealthBar");

        if (transform.parent.GetComponent<Animator>().GetBool("hide") == true)
        {
            transform.parent.GetComponent<Animator>().SetBool("hide", false);
        }
        Invoke("DeactivateHealthBar", 3);
    }

    public void DeactivateHealthBar()
    {
        transform.parent.GetComponent<Animator>().SetBool("hide", true);
    }
}
