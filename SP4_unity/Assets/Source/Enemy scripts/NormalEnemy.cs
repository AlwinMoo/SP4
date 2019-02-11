using UnityEngine;
using UnityEngine.AI;
public class NormalEnemy : MonoBehaviour {
	public GameObject target;
	public NavMeshAgent agent;

	private float m_countDown;
	private const float cooldown = 3.0f;

	// Use this for initialization
	void Start () {
		m_countDown = cooldown;
	}
	
	// Update is called once per frame
	void Update () {
		m_countDown -= Time.deltaTime;

		if (m_countDown <= 0.0f) {
			m_countDown += cooldown;
			agent.SetDestination (target.transform.position);
			Debug.Log("updated destination");
		}
	}
}
