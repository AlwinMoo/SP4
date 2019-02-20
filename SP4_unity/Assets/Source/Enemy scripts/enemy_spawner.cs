using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;

public class enemy_spawner : MonoBehaviour {
    
	//public List<GameObject[]> enemyPrefab;

	public static List<GameObject> enemyList;
    public static float spawnTimer;

    int waveCount;

    // Use this for initialization
    void Start ()
    {
        spawnTimer = 0.0f;
		enemyList = new List<GameObject>();
        Random.InitState((int)System.DateTime.Now.Ticks);

        waveCount = 1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (enemyList.Count == 0)
        {
            //TO DO: SHOW TIME LEFT TILL NEXT WAVE
            spawnTimer += Time.deltaTime;
        }

        //TO DO: SHOW ENEMIES LEFT
        if (spawnTimer >= 5)
        {
            if (NetworkManager.Instance.IsServer)
            {
                for (int i = 0; i <= (int)SpawnerCalc(waveCount, 3, 37, 40); ++i)
                {
                    //Rigidbody newEnemy;
                    Vector3 randPos = new Vector3(Random.Range(0, 20), 0, Random.Range(0, 20));
                    var newEnemy = NetworkManager.Instance.InstantiateEnemy(Random.Range(0, 1), randPos, transform.rotation, true);//Instantiate(enemyPrefab) as Rigidbody;

                    enemyList.Add(newEnemy.gameObject);
                    spawnTimer = 0.0f;
                }
            }

            ++waveCount;
        }
    }

    float SpawnerCalc(float currentWave, float startMobCount, float MaxMinDiff, float MaxWave)
    {
        if ((currentWave /= MaxWave / 2) < 1)
            return MaxMinDiff / 2 * currentWave * currentWave * currentWave + startMobCount;

        return MaxMinDiff / 2 * ((currentWave -= 2) * currentWave * currentWave + 2) + startMobCount;
    }
}
