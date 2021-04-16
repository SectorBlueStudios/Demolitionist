using UnityEngine;

public class LevelNumber : MonoBehaviour {

    public GameObject LevelController;
    public int level;
    public int region;

    public void SendLevel()
    {
        //LevelController.GetComponent<LevelController>().StartControl(); //Sends the user-input level to the Level Controller in the scene.
    }
}
