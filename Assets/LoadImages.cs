using UnityEngine;
using UnityEngine.UI;
//using UnityEditor;

public class LoadImages : MonoBehaviour
{
    public Sprite[] sprites;
    public GameObject levelData;
    
    // Start is called before the first frame update
    void Start()
    {
        int region = levelData.GetComponent<LevelController>().region;
        int level = levelData.GetComponent<LevelController>().level;

        for (int i = 1; i < 26; i++)
        {
            var tempSprite = Resources.Load<Sprite>("Level_Screenshots/" + "Region " + region + "/R" + region + "L" + i);
            if(tempSprite != null)
                sprites[i - 1] = tempSprite;
        }
        gameObject.GetComponent<Image>().sprite = sprites[level - 1];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
