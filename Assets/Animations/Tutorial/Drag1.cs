using UnityEngine;
using Lean.Touch;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class Drag1 : MonoBehaviour {

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

    public GameObject[] explosives;
    public Rect[] explosivesRect;

    public Canvas canvas;

    //private Rect[] explosiveRect;


    //TRASH BUTTON VARIABLES
    public Button trashButton;
    public Button trashButtonOpen;

    private Rect trashButtonRect;
    private Rect trashButtonOpenRect;

    public GameObject trashGameObject;

    //GLOBAL COST
    public int cost = 0;

    //IN BOUND POSITION
    private Vector3 inBoundsPos = Vector3.zero;



    public void GenerateExplosives()
    {
        explosives = GameObject.FindGameObjectsWithTag("Button").OrderBy(go => go.name).ToArray();
        explosivesRect = new Rect[explosives.Length];
        RectangleGeneration();
    }
    
    private void Start()
    {
        explosives = GameObject.FindGameObjectsWithTag("Button").OrderBy(go => go.name).ToArray();
        explosivesRect = new Rect[explosives.Length];
        RectangleGeneration();
       
    }

    void Update() {
        RectangleGeneration();
        MouseEvents();
    }

    void RectangleGeneration()
    {      
        ExplosiveBarRectCreator();

        //trashButtonRect = TrashButtonRectCreator(trashButton);
        //trashButtonOpenRect = TrashButtonRectCreator(trashButtonOpen);
    }

    public Rect TrashButtonRectCreator(Button rectTRANS)
    {
        var exp = rectTRANS.GetComponent<RectTransform>();
        Rect testRECT = exp.rect;

        testRECT.size = testRECT.size * canvas.scaleFactor; //Scales the buttons to the current screen resolution/size.
        testRECT.center = new Vector2(exp.position.x, Screen.height + -exp.position.y);
        return new Rect(testRECT);
    }

    public void ExplosiveBarRectCreator() //Creates buttons for explosive dragging labeled "Button".
    {
        for(int i = 0; i < explosives.Length; i++)
        {
            Button butt = explosives[i].GetComponent<Button>();
            Rect exp = butt.GetComponent<RectTransform>().rect;
            RectTransform testRECT = butt.GetComponent<RectTransform>();

            exp.size = exp.size * canvas.scaleFactor; //Scales the buttons to the current screen resolution/size.
            exp.center = new Vector2(testRECT.position.x, Screen.height + -testRECT.position.y); //Sets the position of the center of the rectangle.           

            explosivesRect[i] = exp;
        }     
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
                   // trashGameObject.SetActive(true);
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
                    //trashGameObject.SetActive(true);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                spawn = null;
                isTouching = false;
                //trashGameObject.SetActive(false);
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

            /*if (isTouching && e.type == EventType.MouseDrag) //If the mouse is in the trash rectangle box while dragging an explosive, it will set off animations.
            {
                if (trashButtonOpenRect.Contains(e.mousePosition))
                {
                    trashGameObject.GetComponent<TrashScript>().toggle = true;
                }
                else
                {
                    trashGameObject.GetComponent<TrashScript>().toggle = false;
                }
            }
            

            if ((trashButtonRect.Contains(e.mousePosition) && isTouching) && e.type == EventType.MouseUp)
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
            */
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
            //BUTTONS
            /*
            
            GUI.Button(explosivesRect[0], "TRASH");
            GUI.Button(explosivesRect[1], "TRASH");
            GUI.Button(explosivesRect[2], "TRASH");
            GUI.Button(explosivesRect[3], "TRASH");
            GUI.Button(explosivesRect[4], "TRASH");
            GUI.Button(explosivesRect[5], "TRASH");
            GUI.Button(explosivesRect[6], "TRASH");
            GUI.Button(explosivesRect[7], "TRASH");
            GUI.Button(explosivesRect[8], "TRASH");

            GUI.Button(trashButtonRect, "TRASH");
            GUI.Button(trashButtonOpenRect, "TRASH");
            
            */
        }
    }
}