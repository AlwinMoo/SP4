using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spawner : MonoBehaviour {
    
    public Rigidbody enemyPrefab;
    //ObjectPooler objectPooler;

    List<Rigidbody> enemyList;
    float spawnTimer;

    // Use this for initialization
    void Start () {
        spawnTimer = 0.0f;
        enemyList = new List<Rigidbody>();
        Random.InitState((int)System.DateTime.Now.Ticks);

        //objectPooler = ObjectPooler.Instance;
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
            Rigidbody newEnemy;
            newEnemy = Instantiate(enemyPrefab) as Rigidbody;
            //Vector3 randPos = new Vector3(Random.Range(0, 20), 0, Random.Range(0, 20));
            newEnemy.transform.position.Set(Random.Range(0, 20), 0, Random.Range(0, 20));

            bool Boolean = (Random.value > 0.5f);
            newEnemy.GetComponent<NormalEnemy>().enabled = Boolean;
            newEnemy.GetComponent<TankEnemy>().enabled = !newEnemy.GetComponent<NormalEnemy>().enabled;
            //if (Boolean)
            //{
            //    GameObject newEnemy = objectPooler.SpawnFromPool("Enemy_Normal", randPos, transform.rotation);
            //    newEnemy.GetComponent<NormalEnemy>().enabled = true;
            //    newEnemy.GetComponent<TankEnemy>().enabled = !newEnemy.GetComponent<NormalEnemy>().enabled;
            //}
            //else
            //{
            //    GameObject newEnemy = objectPooler.SpawnFromPool("Enemy_Big", randPos, transform.rotation);
            //    newEnemy.GetComponent<NormalEnemy>().enabled = false;
            //    newEnemy.GetComponent<TankEnemy>().enabled = !newEnemy.GetComponent<NormalEnemy>().enabled;
            //}

            enemyList.Add(newEnemy);
            spawnTimer = 0.0f;
        }
	}
}
