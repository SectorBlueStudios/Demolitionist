using UnityEngine;

public class RotateCamera : MonoBehaviour {

    public float speed;

    public GameObject paused;

    Quaternion initialRotation;
    Quaternion rotation;
    float t;

    public void RotateRight()
    {
        if (!paused.GetComponent<Pause>().isPaused)
        {
            float rot = Mathf.RoundToInt(transform.eulerAngles.y);
            if (rot % 90 == 0)
            {
                initialRotation = transform.localRotation;
                rotation = Quaternion.Euler(0, rot - 90, 0);
                t = 0.0f;
            }
        }
    }

    public void RotateLeft()
    {
        if (!paused.GetComponent<Pause>().isPaused)
        {
            int rot = Mathf.RoundToInt(transform.eulerAngles.y);
            if (rot % 90 == 0)
            {
                initialRotation = transform.localRotation;
                rotation = Quaternion.Euler(0, rot + 90, 0);
                t = 0.0f;
            }
        }
    }

    public void Update()
    {
        transform.localRotation = Quaternion.Slerp(initialRotation, rotation, t);
        t = t + Time.deltaTime * speed;
    }
}
