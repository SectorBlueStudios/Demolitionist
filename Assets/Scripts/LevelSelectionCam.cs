using UnityEngine;

public class LevelSelectionCam : MonoBehaviour
{
    Camera cam;
    Vector3 posStart;
    Vector3 posEnd;

    public GameObject panel;
    public GameObject levelStartMenu;

    float journey = 0f;

    bool interpolation = false;

    public void SetPositions(GameObject endGameObject)
    {
        posEnd = endGameObject.GetComponent<RectTransform>().localPosition;
        posStart = GetComponent<RectTransform>().localPosition;

        float asp = cam.aspect;
        float ysize = cam.orthographicSize;

        posEnd.x += ((ysize * 2) * asp) / 6; //This algorithm takes the orthographic size of the camera (720) which is half of the height of the screen.
                                             //It then takes the aspect ratio and multiplies the orthographic size * 2 by the aspect ratio to fit all devices.
                                             //It finishes by moving it over by a sixth. (From the center of the screen)
        posEnd.z = -1;

        interpolation = true;
    }

    void Start()
    {
            cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (interpolation == true)
        {
            GetComponent<RectTransform>().localPosition = Vector3.Lerp(posStart, posEnd, journey);
            journey += 10 * Time.deltaTime;
        }
        if (journey >= 1)
        {
            interpolation = false;
            GetComponent<RectTransform>().localPosition = posEnd;
            journey = 0f;
        }
        HideIfClickedOutside();

    }

    private void HideIfClickedOutside()
    {
        if (Input.GetMouseButton(0) && panel.activeSelf && !RectTransformUtility.RectangleContainsScreenPoint(panel.GetComponent<RectTransform>(), Input.mousePosition, Camera.main))
        {
            levelStartMenu.SetActive(false);
        }
    }
}
