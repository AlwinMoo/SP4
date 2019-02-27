using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour {

    //public Text HighScore;

    public Text Speed;
    public Text Armour;
    public Text Health;
    public Text EnemiesLeft;
    public Text NextWave;
    public Text ObjectiveTitle;
    public Text ObjectiveDescription;
    public static GameObject CarBase;
    private int TimeRemainingTillNextWave;

    void Start()
    {
        TimeRemainingTillNextWave = 5;
    }

    // Update is called once per frame
    void Update ()
    {

        if (CarBase == null)
            return;

        //HighScore.text = playerKills.ToString();
        Health.text = "Health: " + CarBase.GetComponent<VehicleBase>().HealthSlider.value.ToString();
        Speedometer.ShowSpeed(CarBase.GetComponent<Rigidbody>().velocity.magnitude, 0, 30);
        Armour.text = "Armour: " + CarBase.GetComponent<VehicleBase>().armour.ToString();
        EnemiesLeft.text = "Enemies Remaining: " + GameObject.FindGameObjectsWithTag("Enemy").Length.ToString();
        NextWave.text = "Time Till Next Wave: " + (TimeRemainingTillNextWave - enemy_spawner.spawnTimer).ToString("0");
        ObjectiveTitle.text = QuestSystem.Title;
        ObjectiveDescription.text = QuestSystem.Description;
    }
}
