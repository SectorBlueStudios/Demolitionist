using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            ScreenCapture.CaptureScreenshot(Application.dataPath + "/Scenes/Levels/Level_Screenshots" + "/CameraScreenshot.png", 2);
        }
    }
}
