using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spawner : MonoBehaviour {

    [SerializeField]
    GameObject enemyPrefab;

    List<GameObject> enemyList;
    float spawnTimer;

    // Use this for initialization
    void Start () {
        spawnTimer = 0.0f;
        enemyList = new List<GameObject>();
        Random.InitState((int)System.DateTime.Now.Ticks);

        if (!enemyPrefab.GetComponent<EnemyBase>())
        {
            Debug.Break();
        }
	}
	
	// Update is called once per frame
	void Update () {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= 3)
        {
            GameObject newEnemy = new GameObject();
            newEnemy = enemyPrefab;
            newEnemy.transform.position = new Vector3(Random.Range(0, 20), 0, Random.Range(0, 20));

            bool Boolean = (Random.value > 0.5f);
            newEnemy.GetComponent<NormalEnemy>().enabled = Boolean;
            newEnemy.GetComponent<TankEnemy>().enabled = !newEnemy.GetComponent<NormalEnemy>().enabled;

            GameObject.Instantiate(newEnemy);
            enemyList.Add(newEnemy);
            spawnTimer = 0.0f;
        }
	}
}
