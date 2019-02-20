using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMapGenerator : MonoBehaviour
{
    // Diff types of obstacles in the map
    public GameObject[] Obstacles;
    // Collider of one tile of the map (for map generation)
    public GameObject floorPlane;

    // Scale of the plane
    public Vector3 planeScale;
    // Positions of the obstacles
    private Vector3[] obstaclePos;

    private GameObject plane;

    private BoxCollider tileCollider;

    private static Random.State seedGenerator;
    // seed to generate seed 
    private static int seedGeneratorSeed = 1;
    private static bool seedGeneratorInitialised = false;

	// Use this for initialization
	void Start ()
    {
        //Random.InitState((int)System.DateTime.Now.Ticks);
        Random.InitState(GenerateSeed());

        GenerateMap();
    }

    void GenerateMap()
    {
        int rand = Random.Range(10, 20);

        plane = Instantiate(floorPlane);
        tileCollider = plane.GetComponentInChildren<BoxCollider>();
        CreateObstacles(rand);
    }

    // Creating obstacles on random parts of the tile
    void CreateObstacles(int numOfObstacles)
    {
       
        for (int i = 0; i < numOfObstacles; ++i)
        {
            Vector3 randPos = new Vector3(Random.Range(tileCollider.bounds.max.x, tileCollider.bounds.min.x), plane.transform.position.y + 1.0f, Random.Range(tileCollider.bounds.min.z, tileCollider.bounds.max.z));

            //Debug.Log("randomPos: " + randPos);
           
            int obstacleType = Random.Range(0, Obstacles.Length);

            switch(obstacleType)
            {
                case 0:
                    GameObject obstacle = Instantiate(Obstacles[0]);
                    obstacle.transform.position = randPos;
                    //ObjectPooler.Instance.SpawnFromPool("Trees", randPos, gameObject.transform.rotation);
                    break;
                case 1:
                    obstacle = Instantiate(Obstacles[1]);
                    obstacle.transform.position = randPos;
                    //ObjectPooler.Instance.SpawnFromPool("Boulders", randPos, gameObject.transform.rotation);
                    break;
            }
        }


    }

    public static int GenerateSeed()
    {
        // remember the old seed
        var temp = Random.state;

        // initialise generator if not generated
        if(!seedGeneratorInitialised)
        {
            Random.InitState(seedGeneratorSeed);
            seedGenerator = Random.state;
            seedGeneratorInitialised = true;
        }

        // set generator state to seed generator
        Random.state = seedGenerator;
        // generate new seed
        var newSeed = Random.Range(int.MinValue, int.MaxValue);
        // remember generator state
        seedGenerator = Random.state;

        // set the original state back so that normal random generation can continue where it left off
        Random.state = temp;

        Debug.Log(" " + newSeed);

        return newSeed;
    }
}
