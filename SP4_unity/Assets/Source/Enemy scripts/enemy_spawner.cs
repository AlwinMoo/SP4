using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

public class enemy_spawner : enemyBehavior {
    
    //public Rigidbody enemyPrefab;

    public static List<Rigidbody> enemyList;
    public static float spawnTimer;

    int waveCount;

    // Use this for initialization
    void Start ()
    {
        spawnTimer = 0.0f;
        enemyList = new List<Rigidbody>();
        Random.InitState((int)System.DateTime.Now.Ticks);

        //objectPooler = ObjectPooler.Instance;
        //if (!enemyPrefab.GetComponent<EnemyBase>())
        //{
        //    Debug.Break();
        //}

        waveCount = 1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (networkObject.IsServer)
        {
            if (enemyList.Count == 0)
            {
                //TO DO: SHOW TIME LEFT TILL NEXT WAVE
                spawnTimer += Time.deltaTime;
            }

            //TO DO: SHOW ENEMIES LEFT
            if (spawnTimer >= 5)
            {
                for (int i = 0; i <= (int)SpawnerCalc(waveCount, 3, 37, 40); ++i)
                {
                    Vector3 randPos = new Vector3(Random.Range(0, 20), 0, Random.Range(0, 20));
                    //newEnemy.transform.position = randPos;
                    var newEnemy = NetworkManager.Instance.Instantiateenemy(0, randPos, transform.rotation, true);

                    bool Boolean = (Random.value > 0.5f);
                    newEnemy.GetComponent<NormalEnemy>().enabled = Boolean;

                    newEnemy.GetComponent<TankEnemy>().enabled = !newEnemy.GetComponent<NormalEnemy>().enabled;

                    enemyList.Add(newEnemy.GetComponent<Rigidbody>());
                    spawnTimer = 0.0f;
                }

                ++waveCount;
            }
        }
    }

    float SpawnerCalc(float currentWave, float startMobCount, float MaxMinDiff, float MaxWave)
    {
        if ((currentWave /= MaxWave / 2) < 1)
            return MaxMinDiff / 2 * currentWave * currentWave * currentWave + startMobCount;

        return MaxMinDiff / 2 * ((currentWave -= 2) * currentWave * currentWave + 2) + startMobCount;
    }
}
