using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour {

    //public Text HighScore;

    public Text Armour;
    public Text Health;
    public Text EnemiesLeft;
    public Text NextWave;
    public Text ObjectiveTitle;
    public Text ObjectiveDescription;
    public static GameObject CarBase;
    private int TimeRemainingTillNextWave;

    public static GameObject GUIpanel;
    public static GameObject RespawnPanel;

    public GameObject speechBubble;

    void Start()
    {
        GUIpanel = GameObject.FindWithTag("GUIpanel");
        RespawnPanel = GameObject.FindWithTag("RespawnPanel");
        RespawnPanel.SetActive(false);

        TimeRemainingTillNextWave = 5;
    }

    // Update is called once per frame
    void Update()
    {

        if (CarBase == null)
            return;

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            speechBubble.SetActive(true);
        }
        else
        {
            speechBubble.SetActive(false);
        }

        //HighScore.text = playerKills.ToString();
        Health.text = "Health: " + CarBase.GetComponent<VehicleBase>().HealthSlider.value.ToString();
        Speedometer.ShowSpeed(CarBase.GetComponent<Rigidbody>().velocity.magnitude, 0, 30);
        Armour.text = (1 - CarBase.GetComponent<VehicleBase>().armour).ToString("2");
        EnemiesLeft.text = "X" + GameObject.FindGameObjectsWithTag("Enemy").Length.ToString();
        NextWave.text = (TimeRemainingTillNextWave - enemy_spawner.spawnTimer).ToString("0");
        ObjectiveTitle.text = QuestSystem.Title;
        ObjectiveDescription.text = QuestSystem.Description;

        

    }

    public static void Isdead()
    {
        GUIpanel.SetActive(false);
        RespawnPanel.SetActive(true);
    }

    public static void IsAlive()
    {
        GUIpanel.SetActive(true);
        RespawnPanel.SetActive(false);
    }
}
