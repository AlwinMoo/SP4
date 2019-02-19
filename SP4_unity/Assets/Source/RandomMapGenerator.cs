using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMapGenerator : MonoBehaviour
{
    // Diff types of obstacles in the map
    public GameObject[] Obstacles;
    // Collider of one tile of the map (for map generation)
    public GameObject floorPlane;

    // tile prefab for the map
    //public Transform plane;
    // Scale of the plane
    public Vector3 planeScale;

    // tile collider prefab to spawn obstacles
    //public Transform planeCollider;

    // how often a gameobject spawns
    //float frequency = 5.0f; 

    //public int xSize = 5;
    //public int zSize = 5;

    private GameObject plane;

    private BoxCollider tileCollider;

	// Use this for initialization
	void Start ()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        GenerateMap();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void GenerateMap()
    {
        int rand = Random.Range(10, 20);

        plane = Instantiate(floorPlane);
        tileCollider = plane.GetComponentInChildren<BoxCollider>();
        CreateObstacles(rand);

        //plane.transform.localScale = planeScale;
        //Instantiate(plane);
        //tileCollider = plane.GetComponentInChildren<BoxCollider>();
        //CreateObstacles(rand);
    }

    // Creating obstacles on random parts of the tile
    void CreateObstacles(int numOfObstacles)
    {
        Debug.Log("minX: " + tileCollider.bounds.max.x);
        Debug.Log("maxX: " + tileCollider.bounds.min.x);
        Debug.Log("minZ: " + tileCollider.bounds.max.z);
        Debug.Log("maxZ: " + tileCollider.bounds.min.z);

        for (int i = 0; i < numOfObstacles; ++i)
        {
            //BoxCollider Collider = tileCollider.GetComponent<BoxCollider>();
            Vector3 randPos = new Vector3(Random.Range(tileCollider.bounds.max.x, tileCollider.bounds.min.x), plane.transform.position.y + 1.0f, Random.Range(tileCollider.bounds.min.z, tileCollider.bounds.max.z));

            //Debug.Log("randomPos: " + randPos);
           
            int obstacleType = Random.Range(0, Obstacles.Length);

            switch(obstacleType)
            {
                case 0:
                    GameObject obstacle = Instantiate(Obstacles[0]);
                    obstacle.transform.position = randPos;
                    break;
                case 1:
                    obstacle = Instantiate(Obstacles[1]);
                    obstacle.transform.position = randPos;
                    break;
            }
        }


    }
}
