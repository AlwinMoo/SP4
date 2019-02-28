using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHand : MonoBehaviour {

    static float minAngle = 90.0f;
    static float maxAngle = -270.0f;

    static ClockHand timeHand;

    void Start()
    {
        timeHand = this;
    }


    public static void ShowSpeed(float speed, float min, float max)
    {
        float ang = Mathf.Lerp(minAngle, maxAngle, Mathf.InverseLerp(min, max, speed));
        timeHand.transform.eulerAngles = new Vector3(0, 0, ang);
    }
}
