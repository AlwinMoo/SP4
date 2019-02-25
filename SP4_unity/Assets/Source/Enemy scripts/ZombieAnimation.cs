using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimation : MonoBehaviour {

	public void Walking()
    {
        gameObject.GetComponent<Animation>().Play("walk");
    }

    public void Attack()
    {
        gameObject.GetComponent<Animation>().Play("attack");
    }

    public void Idle()
    {
        gameObject.GetComponent<Animation>().Play("idle");
    }

    public void FallOver()
    {
        gameObject.GetComponent<Animation>().Play("idle");
    }
}
