/*using UnityEngine;
using Lean.Touch;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class Drag : MonoBehaviour {

    public GameObject paused;
    public bool isPaused = false;

    public GameObject explosive;        //Explosive being dragged.
    public bool isTouching = false;     //Boolean valus to tell whether the collider of the explosive is being touched.

    [Tooltip("Ignore fingers with StartedOverGui?")]
    public bool IgnoreStartedOverGui = true;

    [Tooltip("Ignore fingers with IsOverGui?")]
    public bool IgnoreIsOverGui;

    [Tooltip("Ignore fingers if the finger count doesn't match? (0 = any)")]
    public int RequiredFingerCount;

    private Rigidbody rb;

    public Material material_color_red;
    public Material material_color_original;

    public Transform prefab;

    private Transform spawn;

    public Button trashButton;
    public Button trashButton2;

    public GameObject[] explosives;
    public Rect[] explosivesRect;

    public Canvas canvas;

    private Rect explosiveRect;
    private Rect trashRect;
    private Rect trashRect2;

    private float canvasScale;
    public int cost = 0;

    private Vector3 inBoundsPos = Vector3.zero;

    public GameObject trashGO;

    private void Start()
    {
        explosives = GameObject.FindGameObjectsWithTag("Button").OrderBy(go => go.name).ToArray();
        explosivesRect = new Rect[explosives.Length];
        RectangleGeneration();
    }

    void Update() {
        explosivesRect = new Rect[explosives.Length];
        RectangleGeneration();
        MouseEvents();
    }

    void RectangleGeneration()
    {
        canvasScale = canvas.GetComponent<RectTransform>().localScale.x;
        
        explosiveRect = RectCreator2();
        trashRect = RectCreator(trashButton);
        trashRect2 = RectCreator(trashButton2);
    }

    public Rect RectCreator(Button expButton)
    {
        var exp = expButton.GetComponent<RectTransform>();
        return new Rect((exp.anchoredPosition.x * canvasScale) - (exp.rect.width * canvasScale) / 2, (-exp.anchoredPosition.y * canvasScale) - (exp.rect.height * canvasScale) / 2, (exp.rect.width * canvasScale), (exp.rect.height * canvasScale));
    }

    public Rect RectCreator2() //Creates buttons for explosive dragging labeled "Button".
    {
        for(int i = 0; i < 1; i++)
        {
            var exp = explosives[i].GetComponent<RectTransform>();
            explosivesRect[i] = new Rect((exp.anchoredPosition.x * canvasScale) - (exp.rect.width * canvasScale) / 2, (-exp.anchoredPosition.y * canvasScale) - (exp.rect.height * canvasScale) / 2, (exp.rect.width * canvasScale), (exp.rect.height * canvasScale));
            //explosivesRect[i] = new Rect(exp.rect);
            Debug.Log(exp.anchoredPosition.y);
            Debug.Log(explosives[i].GetComponent<RectTransform>().anchoredPosition.y);
        }
        return explosiveRect;
        
    }

    //This function handles the main mouse events such as dragging the pieces. It handles when the mouse is held down and
    //  let go.
    void MouseEvents()
    {
        if (!paused.GetComponent<Pause>().isPaused)
        {
            if (Input.GetMouseButton(0) && spawn != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;
                if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, 1 << 10 | 1 << 11))
                {
                    isTouching = true;
                    //spawn.GetComponent<MeshRenderer>().material = material_color_original;
                    if (rayHit.collider.gameObject.layer == 11) //Is the explosive out of bounds?
                    {
                        //spawn.GetComponent<MeshRenderer>().material = material_color_red;
                        inBoundsPos = Vector3.zero;
                    }
                    else
                    {
                        inBoundsPos = spawn.GetComponent<Rigidbody>().transform.position;
                    }
                    spawn.position = rayHit.point;
                    trashGO.SetActive(true);
                }

            }

            if (Input.GetMouseButton(0))
            {
                if (isTouching == false)
                {
                    TouchObject();
                }
                if (isTouching == true)
                {
                    MoveObject();
                    trashGO.SetActive(true);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                spawn = null;
                isTouching = false;
                trashGO.SetActive(false);
            }
        }
    }

    //This function casts its own ray on explosives. This indicates whether the explosive is being touched by the user.
    //  It then is consistently changing the global variable isTouching according to if there is a hit or not.
    //  The bit shift (1 << 9) refers to the 9th layer in the scene [CurrentSelected] (explosive).
    void TouchObject()
    {
        // Get the fingers we want to use
        var fingers = LeanTouch.GetFingers(IgnoreStartedOverGui, IgnoreIsOverGui, RequiredFingerCount);

        if (fingers.Count == 1 && fingers[0].IsActive == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, 1 << 9 | 1 << 5))
            {                
                isTouching = true;
                explosive = rayHit.collider.gameObject;     //Sets the explosive to which ever one is being selected.
                rb = explosive.GetComponent<Rigidbody>();   //Sets the rigidbody to the selected explosive.
            }
        }
    }
    
    //This function moves the rigidbody of the explosive by first verifying the user is touching the explosive.
    //  Once the variable isTouching is true, the Rigidbody moves accordingly. The big shift (1 << 10) refers to the
    //  10th layer in the scene which is dedicated to Objects in the scene. 
    void MoveObject()
    {
        // Get the fingers we want to use
        var fingers = LeanTouch.GetFingers(IgnoreStartedOverGui, IgnoreIsOverGui, RequiredFingerCount);

        if (fingers.Count == 1 && fingers[0].IsActive == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, 1 << 10 | 1 << 11))
            {
                if (rb != null)
                {
                    Debug.DrawRay(ray.origin, ray.direction * 100);
                    explosive.transform.position = rayHit.point;
                    //explosive.GetComponent<MeshRenderer>().material = material_color_original;
                    if (rayHit.collider.gameObject.layer == 11) //Is the explosive out of bounds?
                    {
                        //Debug.Log("Out of bounds!");
                        //explosive.GetComponent<MeshRenderer>().material = material_color_red;
                        inBoundsPos = Vector3.zero;
                    }
                    else
                    {
                        inBoundsPos = rb.transform.position;
                    }
                }
            }
        }
    }

    void OnGUI()
    {
        if (!paused.GetComponent<Pause>().isPaused)
        {
            Event e = Event.current;

            if (e.type == EventType.MouseDown)
            {
                for (int i = 0; i < explosivesRect.Length; i++)
                {
                    if (explosivesRect[i].Contains(e.mousePosition))
                    {
                        var pos = Input.mousePosition;
                        spawn = Instantiate(explosives[i].GetComponent<TypeOfExplosive>().explosive.transform, pos, explosives[i].GetComponent<TypeOfExplosive>().explosive.transform.rotation) as Transform;
                        cost = cost + spawn.GetComponent<Explosion>().cost;
                        //Debug.Log("Mouse Down!");
                    }
                }
            }

            if (isTouching && e.type == EventType.MouseDrag) //If the mouse is in the trash rectangle box while dragging an explosive, it will set off animations.
            {
                if (trashRect2.Contains(e.mousePosition))
                {
                    trashGO.GetComponent<TrashScript>().toggle = true;
                }
                else
                {
                    trashGO.GetComponent<TrashScript>().toggle = false;
                }
            }

            if ((trashRect.Contains(e.mousePosition) && isTouching) && e.type == EventType.MouseUp)
            {
                if (explosive != null)
                {
                    cost = cost - explosive.GetComponent<Explosion>().cost;
                    Destroy(explosive);
                }
                if (spawn != null)
                {
                    cost = cost - spawn.GetComponent<Explosion>().cost;
                    Destroy(spawn.gameObject);
                }
            }
            else if (e.type == EventType.MouseUp)
            {
                if (inBoundsPos != Vector3.zero)
                { }
                else
                {
                    if (explosive != null)
                    {
                        cost = cost - explosive.GetComponent<Explosion>().cost;
                        Destroy(explosive);
                    }
                    if (spawn != null)
                    {
                        cost = cost - spawn.GetComponent<Explosion>().cost;
                        Destroy(spawn.gameObject);
                    }

                }
            }
            //GUI.Button(trashRect2, "SPAWN");
            GUI.Button(explosivesRect[0], "TRASH");
            GUI.Button(explosivesRect[1], "TRASH");
            GUI.Button(explosivesRect[2], "TRASH");
            GUI.Button(explosivesRect[3], "TRASH");
            GUI.Button(explosivesRect[4], "TRASH");
            GUI.Button(explosivesRect[5], "TRASH");
            GUI.Button(explosivesRect[6], "TRASH");
            GUI.Button(explosivesRect[7], "TRASH");
            GUI.Button(explosivesRect[8], "TRASH");
        }
    }
}
*/