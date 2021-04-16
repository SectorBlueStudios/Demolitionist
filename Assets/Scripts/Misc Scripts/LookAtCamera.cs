using UnityEngine;

public class LookAtCamera : MonoBehaviour {
    private void Start()
    {
        #region //Fix so the health bar rotation is set immediately instead of the update frame
        float angle = Camera.main.transform.localEulerAngles.y;
        angle = (angle > 180) ? angle - 360 : angle;

        float angle2 = Camera.main.transform.parent.localEulerAngles.y;
        angle2 = (angle2 > 180) ? angle2 - 360 : angle2;

        Quaternion rotation = Quaternion.Euler(Camera.main.transform.eulerAngles.x, angle + angle2, Camera.main.transform.eulerAngles.z);
        transform.rotation = rotation;

        ChangeHealthBarPosition();
        #endregion 
    }
    void Update() {
        float angle = Camera.main.transform.localEulerAngles.y;
        angle = (angle > 180) ? angle - 360 : angle;

        float angle2 = Camera.main.transform.parent.localEulerAngles.y;
        angle2 = (angle2 > 180) ? angle2 - 360 : angle2;

        Quaternion rotation = Quaternion.Euler(Camera.main.transform.eulerAngles.x, angle + angle2, Camera.main.transform.eulerAngles.z);
        transform.rotation = rotation;

        ChangeHealthBarPosition();
    }

    void ChangeHealthBarPosition()
    {
        if (transform.GetComponentInParent<Renderer>() != null || 
            transform.GetComponentInParent<MeshCollider>() != null || 
            transform.GetComponentInParent<CapsuleCollider>() != null ||
            transform.GetComponentInParent<SphereCollider>() != null ||
            transform.GetComponentInParent<BoxCollider>() != null
            )
        {
            Vector3 center;
            float radius;

            if (transform.GetComponentInParent<Renderer>() != null) //Is there a regular Mesh Renderer?
            {
                center = transform.GetComponentInParent<Renderer>().bounds.center;
                radius = transform.GetComponentInParent<Renderer>().bounds.extents.magnitude;
                transform.position = new Vector3(center.x, center.y + radius, center.z);
            }
            else if (transform.GetComponentInParent<CapsuleCollider>() != null) //If there is no regular Mesh Renderer, is there a Capsule Collider?
            {
                center = transform.GetComponentInParent<CapsuleCollider>().bounds.center;
                radius = transform.GetComponentInParent<CapsuleCollider>().bounds.extents.magnitude;
                transform.position = new Vector3(center.x, center.y + radius, center.z);
            }            
            else if (transform.GetComponentInParent<SphereCollider>() != null) //If there is no regular Capsule Collider, is there a Sphere Collider?
            {
                center = transform.GetComponentInParent<SphereCollider>().bounds.center;
                radius = transform.GetComponentInParent<SphereCollider>().bounds.extents.magnitude;
                transform.position = new Vector3(center.x, center.y + radius, center.z);
            }            
            else if (transform.GetComponentInParent<BoxCollider>() != null) //If there is no regular Sphere Collider, is there a Box Collider?
            {
                center = transform.GetComponentInParent<BoxCollider>().bounds.center;
                radius = transform.GetComponentInParent<BoxCollider>().bounds.extents.magnitude;
                transform.position = new Vector3(center.x, center.y + radius, center.z);
            }
            else if (transform.GetComponentInParent<MeshCollider>() != null) //If there is no regular Box Collider, is there a Mesh Collider?
            {
                center = transform.GetComponentInParent<MeshCollider>().bounds.center;
                radius = transform.GetComponentInParent<MeshCollider>().bounds.extents.magnitude;
                transform.position = new Vector3(center.x, center.y + radius, center.z);
            }
        }
    }
}