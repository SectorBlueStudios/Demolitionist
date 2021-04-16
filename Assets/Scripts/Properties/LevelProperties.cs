using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelProperties : MonoBehaviour {

    public int region;
    public int level;

    public TextMeshProUGUI visualText;
    

    public void Start()
    {

    }

    private void OnValidate()
    {
        if(SceneManager.GetActiveScene().name.Length < 5)
            level = SceneManager.GetActiveScene().name[3] - 48; //ASCII (-48)
        
        gameObject.name = "Level " + level;
        if(visualText != null)
            visualText.text = level.ToString();
    }
}
