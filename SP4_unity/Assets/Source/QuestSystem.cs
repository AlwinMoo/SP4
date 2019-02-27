using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;


public class QuestSystem : QuestSystemBehavior
{

    private Vector3 gameObjectPosition;
    GameObject theObj;

    public static string Title;
    public static string Description;
    public static int QuestID;
    private float temp;

    private float HoldOutTime;
    private float randomTime;
    private bool Done;

    void Start()
    {
        // If IsActive == false, the quest will not start
        networkObject.IsActive = false;
        temp = 0;
        if (networkObject.IsServer)
        {
            randomTime = Random.Range(5, 10);
        }
    }

    void Update()
    {
        if (networkObject.IsActive)
        {
            switch (networkObject.QuestID)
            {
                case 1:
                    {
                        if (networkObject.IsServer)
                        {
                            // server side
                            // This is the Init of the quests
                            if (!Done)
                            {
                                networkObject.HoldOutTime = 30;
                                gameObjectPosition = new Vector3(Random.Range(-30, 30), -3, Random.Range(-30, 30));

                                var newGO = NetworkManager.Instance.InstantiateObjectiveObject(0, gameObjectPosition, transform.rotation, true);

                                theObj = newGO.gameObject;
                                Done = true;
                            }

                            if (networkObject.HoldOutTime <= 0)
                            {
                                networkObject.IsPassed = true;
                                // Set the time for the next quest
                                randomTime = Random.Range(5, 15);
                                // Destroy the objective object
                                if(theObj != null)
                                    theObj.GetComponent<ObjectiveObject>().SendRemove();

                                for(int i = 0; i < Random.Range(1,5); ++i)
                                {
                                    int random = Random.Range(1, 3);
                                    if(random == 1)
                                    {
                                        var newGO = NetworkManager.Instance.InstantiateHealthPotion(0, new Vector3(Random.Range(-100, 101), -5, Random.Range(-100, 101)), transform.rotation, true);
                                    }
                                    else
                                    {
                                        var newGO = NetworkManager.Instance.InstantiateArmour(0, new Vector3(Random.Range(-100, 101), -5, Random.Range(-100, 101)), transform.rotation, true);
                                    }
                                }
                                networkObject.IsActive = false;

                            }
                            else if (theObj.GetComponent<ObjectiveObject>().HealthSlider.value <= 0)
                            {
                                networkObject.IsFailed = true;
                                // Set the time for the next quest
                                randomTime = Random.Range(5, 15);
                                // Destroy the objective object
                                if (theObj != null)
                                    theObj.GetComponent<ObjectiveObject>().SendRemove();

                                networkObject.IsActive = false;
                            }

                            networkObject.HoldOutTime -= Time.deltaTime;    
                        }
                    }
                    break;
                case 2:
                    {
                        if (networkObject.IsServer)
                        {
                            // server side
                            // This is the Init of the quests
                            if (!Done)
                            {
                                networkObject.HoldOutTime = 20;
                                Done = true;
                            }

                            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                            {
                                networkObject.IsPassed = true;

                                for (int i = 0; i < Random.Range(1, 5); ++i)
                                {
                                    int random = Random.Range(1, 3);
                                    if (random == 1)
                                    {
                                        var newGO = NetworkManager.Instance.InstantiateHealthPotion(0, new Vector3(Random.Range(-100, 101), -5, Random.Range(-100, 101)), transform.rotation, true);
                                    }
                                    else
                                    {
                                        var newGO = NetworkManager.Instance.InstantiateArmour(0, new Vector3(Random.Range(-100, 101), -5, Random.Range(-100, 101)), transform.rotation, true);
                                    }
                                }
                                networkObject.IsActive = false;

                            }
                            else if (networkObject.HoldOutTime <= 0)
                            {
                                networkObject.IsFailed = true;
                                networkObject.IsActive = false;
                            }

                            networkObject.HoldOutTime -= Time.deltaTime;
                        }
                    }
                    break;
            }
        }
        else if (networkObject.IsServer && !networkObject.IsActive)
        {
            temp += Time.deltaTime;
            if (temp >= randomTime)
            {
                temp = 0;
                QuestID = Random.Range(1, 3);
                networkObject.QuestID = QuestID;
                networkObject.IsActive = true;
                networkObject.IsFailed = false;
                networkObject.IsPassed = false;
                Done = false;
            }
        }

        // This part below is the printing out of stuff

        if (networkObject.IsPassed)
        {
            Title = "Success!!";
            Description = null;
        }
        else if (networkObject.IsFailed)
        {
            Title = "Objective Failed";
            Description = null;
        }
        else
        {
            switch (networkObject.QuestID)
            {
                case 1:
                    {
                        Title = "Hold the objective";
                        Description = "hold out: " + networkObject.HoldOutTime.ToString("0");
                    }
                    break;
                case 2:
                    {
                        Title = "Cleaing the Area";
                        Description = "Kill " + GameObject.FindGameObjectsWithTag("Enemy").Length.ToString() + "Enemies in: " + networkObject.HoldOutTime.ToString("0");
                    }
                    break;
            }


        }
    }
}

