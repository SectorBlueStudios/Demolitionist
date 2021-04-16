using UnityEngine;

public class Legal : MonoBehaviour
{
    public void OpenLegal()
    {
        string URL = "https://sectorbluestudios.com/terms-and-conditions/";
        Application.OpenURL(URL);
    }
}