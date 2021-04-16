using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
public class LevelPanelMove : MonoBehaviour, IDragHandler, IEndDragHandler
{
    float mouseX = 0;
    float mouseY = 0;

    float panelX = 0;
    float panelY = 0;

    bool firstrun = false;

    void Start()
    {
        panelX = transform.position.x;
        panelY = transform.position.y;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (firstrun == true)
        {
            float newmouseX = 0;
            float newmouseY = 0;

            newmouseX = Input.mousePosition.x;
            newmouseY = Input.mousePosition.y;

            panelY = panelY + (newmouseY - mouseY);
            panelX = panelX + (newmouseX - mouseX);

            transform.position = new Vector3(panelX, panelY, 0);
        }

        mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;
        firstrun = true;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        firstrun = false;
    }
}