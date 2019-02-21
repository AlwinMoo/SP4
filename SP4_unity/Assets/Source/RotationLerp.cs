using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLerp : MonoBehaviour
{
    Vector3 directionVector;
    Quaternion targetRotation;

    private Transform target;
    public float speed = 0.1f;

    //bool rotating = false;

    //float rotaionTime = 0;

    // Update is called once per frame
    void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, this.transform.position);
        float distToPlane;
        //target.position = Input.mousePosition;
        if (plane.Raycast(ray, out distToPlane))
        {
            Vector3 hitPos = ray.GetPoint(distToPlane);

            Vector3 dir = hitPos - transform.position;
            dir.y = 0;

            targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1.0f);
        }
        
	}
}
