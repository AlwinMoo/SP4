using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleBase : MonoBehaviour{

    public float health { get; set; }
    public float mass { get; set; }

    public Slider HealthSlider;

    // Use this for initialization
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        HealthSlider.value = health;
    }
}
