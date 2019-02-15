using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour {

    bool isActive;

    public GameObject HoldOutObject;

    public static string Title;
    public static string Description;
    public static int ID;
    private float temp;

    private float HoldOutTime;
    public static float KillsLeft;
    private float randomTime;

    private bool Done;

    void Start()
    {
        isActive = false;
        HoldOutTime = 10;
        KillsLeft = 3;
    }

    void Update()
    {

        if(isActive)
        {
            switch (ID)
            {
                case 1:
                    {
                        if(!Done)
                        {
                            HoldOutObject.SetActive(true);
                            HoldOutObject.transform.position = new Vector3(Random.Range(-30, 30), -3, Random.Range(-30, 30));
                            Title = "Hold the objective";
                            Done = true;
                        }
                        Description = "hold out: " + HoldOutTime.ToString("0");
                        HoldOutTime -= Time.deltaTime;
                        if (HoldOutTime <= 0)
                        {
                            isActive = false;
                            HoldOutTime = 10;
                            Title = "Success!!";
                            Description = null;
                            HoldOutObject.SetActive(false);
                            randomTime = Random.Range(5, 15);
                            Done = false;
                        }
                    }
                    break;
                case 2:
                    {
                        if(!Done)
                        {
                            Title = "Kill 3 Enemies";
                            Done = true;
                        }
                        Description = "Kills Left: " + KillsLeft.ToString();
                        if (KillsLeft <= 0)
                        {
                            isActive = false;
                            KillsLeft = 3;
                            Title = "Success!!";
                            Description = null;
                            randomTime = Random.Range(5, 15);
                            Done = false;
                        }
                    }
                    break;

            }
        }
        else
        {
            temp += Time.deltaTime;
            if(temp >= randomTime)
            {
                isActive = true;
                temp = 0;
                ID = Random.Range(1,2);
            }
        }
        
        

    }



}
