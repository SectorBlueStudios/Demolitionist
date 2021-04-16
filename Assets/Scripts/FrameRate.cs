using UnityEngine;

public class FrameRate : MonoBehaviour
{
    void Start()
    {
        // Make the game run up to 60fps.
        Application.targetFrameRate = 144;
    }
}