using UnityEngine;
using TMPro;

public class LevelText : MonoBehaviour
{
    TextMeshProUGUI text;
    public void SetLevelNumber(int level)
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "" + level;
    }
}
