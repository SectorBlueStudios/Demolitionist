using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F8))
        {
            ScreenshotHandler.TakeScreenshot_Static(1024, 1024);
        }
    }
}
