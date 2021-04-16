using UnityEngine;

public class debugCam : MonoBehaviour
{
    void Start()
    {
        //This script is used to fix the weird camera reset bug after restarting a scene.
        this.gameObject.SetActive(false);
        this.gameObject.SetActive(true);
    }
}
