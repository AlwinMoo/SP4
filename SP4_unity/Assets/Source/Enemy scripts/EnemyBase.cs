using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour{

    public float health { get; set; }
    public float mass { get; set; }

    GameObject target;
    NavMeshAgent agent;

    private float m_countDown;
    private const float cooldown = 3.0f;

    // Use this for initialization
    public virtual void Awake()
    {
        m_countDown = cooldown;
        agent = this.GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void Start()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {
        m_countDown -= Time.deltaTime;

        if (m_countDown <= 0.0f)
        {
            m_countDown += cooldown;
            agent.SetDestination(target.transform.position);
            Debug.Log("updated destination");
        }
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        
    }
}
