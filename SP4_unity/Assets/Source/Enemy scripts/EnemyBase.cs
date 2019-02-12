using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour {

    public Vector3 u1 { get; set; }
    public Vector3 u2 { get; set; }
    public Vector3 v1 { get; set; }
    public Vector3 v2 { get; set; }
    public float m1 { get; set; }
    public float m2 { get; set; }

    public float health { get; set; }
    public float mass { get; set; }

    GameObject target;
    public NavMeshAgent agent { get; set; }

    private float m_countDown;

    public enum enemytype
    {
        ENEMY_NORMAL,
        ENEMY_TANK,
    } 
    public enemytype enemyType{get; set;}

    // Use this for initialization
    public virtual void Awake()
    {
        m_countDown = 0.0f;
        agent = this.GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void Start()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {
        m_countDown += Time.deltaTime;

        if (m_countDown >= 3.0f && GetComponent<NavMeshAgent>().enabled == true && GetComponent<Rigidbody>().isKinematic == true)
        {
            m_countDown = 0.0f;
            agent.SetDestination(target.transform.position);
            Debug.Log("updated destination");
        }
        else if (m_countDown >= 7.0f && GetComponent<NavMeshAgent>().enabled == false && GetComponent<Rigidbody>().isKinematic == false)
        {
            m_countDown = 0.0f;
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        
    }
}
