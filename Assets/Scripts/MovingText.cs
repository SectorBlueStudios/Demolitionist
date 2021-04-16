using UnityEngine;
using UnityEngine.UI;

public class MovingText : MonoBehaviour {

    public Text text;

    public float positionX;
    public float positionY;

    public float t = 0f;
    public float p = 1f;

    public bool activate = false;
    public bool addOne = false;

    public Color col;

    void Start () {
        text = GetComponent<Text>();
        t = 0;
        p = 1;
        Random.InitState(System.DateTime.Now.Millisecond);
        positionX = transform.parent.position.x + Random.Range(-20f, 20f);
        positionY = transform.parent.position.y + 75;
        col = text.color;
    }

	void Update ()
    {
        if (activate == true)
        {
            if (transform.parent != null)
            {
                text.text = ("-$" + transform.parent.GetComponent<TypeOfExplosive>().explosive.gameObject.GetComponent<Explosion>().cost);
            }
            GetComponent<RectTransform>().position = new Vector2(positionX, Mathf.Lerp(positionY, positionY + 35, t));
            text.color = new Color(1, 0, 0, Mathf.Lerp(0, 1, p));

            p -= 1 * Time.deltaTime;
            t += 1 * Time.deltaTime;

            if(t >= 1)
            {
                activate = false;
                t = 0;
                text.text = (" ");
            }

            if(p <= 0)
            {
                p = 1;
            }
        }
	}

    public void Activate()
    {
        if(activate == true)
        {
            ActivateAnother();
        }
        else
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            positionX = transform.parent.position.x + Random.Range(-20f, 20f);
            positionY = transform.parent.position.y + 75;
        }
        activate = true;
    }

    public void ActivateAnother()
    {
        var here = Instantiate(gameObject); 
        here.transform.SetParent(gameObject.transform.parent);
    }
}
