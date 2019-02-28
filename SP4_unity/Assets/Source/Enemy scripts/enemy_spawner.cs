using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;

public class enemy_spawner : EnemySpawnerBehavior {
    
	public int enemyPrefabCount;

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
        // For client(if no enemy clear enemy_list)
        // sync the respawntimer always
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            enemyList.Clear();
            //TO DO: SHOW TIME LEFT TILL NEXT WAVE
            spawnTimer = networkObject.RespawnTimer;
        }


        if (NetworkManager.Instance.IsServer)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                enemyList.Clear();
                //TO DO: SHOW TIME LEFT TILL NEXT WAVE
                networkObject.RespawnTimer += Time.deltaTime;
                spawnTimer = networkObject.RespawnTimer;
            }

            //TO DO: SHOW ENEMIES LEFT

            if (networkObject.RespawnTimer >= 5)
            {
                for (int i = 0; i <= (int)SpawnerCalc(waveCount, 3, 37, 40); ++i)
                {
                    //Rigidbody newEnemy;
                    Vector3 randPos = new Vector3(Random.Range(0, 20), -1, Random.Range(0, 20));
                    var newEnemy = NetworkManager.Instance.InstantiateEnemy(Random.Range(0, enemyPrefabCount), randPos, transform.rotation);

                    enemyList.Add(newEnemy.gameObject);
                    networkObject.RespawnTimer = 0.0f;
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

	public void SpawnSpiderSwarm (Transform _transform)
	{
		// spawn 10 spiders in the area
		for (int i = 0; i < 10; ++i) 
		{
			var newEnemy = NetworkManager.Instance.InstantiateEnemy(enemyPrefabCount, _transform.position, _transform.rotation);

			enemyList.Add(newEnemy.gameObject);
		}
	}

    //public override void StartInstantiate(RpcArgs args)
    //{
    //    var newEnemy = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], args.GetNext<Vector3>(), transform.rotation);
    //    //var newEnemy = NetworkManager.Instance.InstantiateEnemy(Random.Range(0, enemyPrefab.Length), args.GetNext<Vector3>(), transform.rotation);

    //    enemyList.Add(newEnemy.gameObject);
    //}

    //public override void DestroyEnemy(RpcArgs args)
    //{
    //    int count = args.GetNext<int>();

    //    if (enemyList[count] != null)
    //    {
    //        Destroy(enemyList[count].gameObject);
    //    }
    //}

    //public void DestroyEnemy(int _index)
    //{
    //    networkObject.SendRpc(RPC_DESTROY_ENEMY, Receivers.All, _index);
    //}

    //public override void EnemyOnFire(RpcArgs args)
    //{
    //    int count = args.GetNext<int>();
    //    bool status = args.GetNext<bool>();

    //    if (enemyList[count] != null)
    //    {
    //        enemyList[count].gameObject.GetComponent<EnemyBase>().m_burning = status;
    //    }
    //}

    //public void EnemyOnFire(int _index, bool _status)
    //{
    //    object[] param = { _index, _status };
    //    networkObject.SendRpc(RPC_DESTROY_ENEMY, Receivers.All, param);
    //}
}
