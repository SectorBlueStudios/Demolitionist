using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetZoom : MonoBehaviour
{
    public void Zoom(float zoom)
    {
        float newZoom = -zoom / 100;
        gameObject.transform.localScale = new Vector3(newZoom, newZoom, newZoom);
    }
}