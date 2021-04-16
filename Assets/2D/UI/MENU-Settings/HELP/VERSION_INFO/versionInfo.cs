using UnityEngine;
using TMPro;

public class versionInfo : MonoBehaviour
{
    private string currentVersion;
        
    void Start()
    {
        currentVersion = Application.version;
        GetComponent<TextMeshProUGUI>().text = "v" + currentVersion;
    }
}
