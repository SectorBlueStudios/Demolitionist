using UnityEngine;

public class SwitchPrefab : MonoBehaviour {

    public GameObject damagedVersion;
    public GameObject destroyedVersion;

	public void Switch1()
    {
        gameObject.SetActive(false);

        damagedVersion.transform.localScale = gameObject.transform.localScale;
        damagedVersion.transform.position = gameObject.transform.position;
        damagedVersion.transform.rotation = gameObject.transform.rotation;

        damagedVersion.GetComponent<Properties>().currentDurability = gameObject.GetComponent<Properties>().currentDurability;
        damagedVersion.GetComponent<Properties>().explosives = gameObject.GetComponent<Properties>().explosives;
        damagedVersion.GetComponent<Properties>().objectstate = Properties.objectState.state2;
        damagedVersion.GetComponent<Properties>().timeSave = gameObject.GetComponent<Properties>().timeSave;
        damagedVersion.GetComponent<Properties>().timeTrigger = gameObject.GetComponent<Properties>().timeTrigger;

        gameObject.GetComponent<Collider>().enabled = false;

        damagedVersion.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;

        Instantiate(damagedVersion);
        Destroy(gameObject);
    }

    public void Switch2(bool particles)
    {
        gameObject.SetActive(false);

        destroyedVersion.transform.localScale = gameObject.transform.localScale;
        destroyedVersion.transform.position = gameObject.transform.position;
        destroyedVersion.transform.rotation = gameObject.transform.rotation;

        destroyedVersion.GetComponent<Properties>().currentDurability = gameObject.GetComponent<Properties>().currentDurability;
        destroyedVersion.GetComponent<Properties>().currentDurability = 0f;
        destroyedVersion.GetComponent<Properties>().explosives = gameObject.GetComponent<Properties>().explosives;
        destroyedVersion.GetComponent<Properties>().objectstate = Properties.objectState.state3;
        destroyedVersion.GetComponent<Properties>().timeSave = gameObject.GetComponent<Properties>().timeSave;
        destroyedVersion.GetComponent<Properties>().timeTrigger = gameObject.GetComponent<Properties>().timeTrigger;

        gameObject.GetComponent<Collider>().enabled = false;

        destroyedVersion.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;

        if (particles && destroyedVersion.GetComponent<Properties>().particlesystem != null)
            destroyedVersion.GetComponent<Properties>().particlesystem.SetActive(true);
        else if(destroyedVersion.GetComponent<Properties>().particlesystem != null)
            destroyedVersion.GetComponent<Properties>().particlesystem.SetActive(false);

        Instantiate(destroyedVersion);

        Destroy(gameObject);
    }
}