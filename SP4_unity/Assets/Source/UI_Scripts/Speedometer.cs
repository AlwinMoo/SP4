using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedometer : MonoBehaviour {

    static float minAngle = -118.0f;
    static float maxAngle = -376.0f;

    static Speedometer meter;

    void Start()
    {
        meter = this;
    }


    public static void ShowSpeed(float speed, float min, float max)
    {
        float ang = Mathf.Lerp(minAngle, maxAngle, Mathf.InverseLerp(min, max, speed));
        meter.transform.eulerAngles = new Vector3(0, 0, ang);
    }
}
