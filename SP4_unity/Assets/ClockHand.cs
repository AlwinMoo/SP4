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

    /// <summary>
    ///  rotate the hand like a clock
    /// </summary>
    /// <param name="speed"> the actual amount u want to traverse(if value of time is 24 hrs put it as 24) </param>
    /// <param name="min"> the starting pos </param>
    /// <param name="max"> the ending pos </param>
    public static void ShowSpeed(float speed, float min, float max)
    {
        float ang = Mathf.Lerp(minAngle, maxAngle, Mathf.InverseLerp(min, max, speed));
        timeHand.transform.eulerAngles = new Vector3(0, 0, ang);
    }
}
