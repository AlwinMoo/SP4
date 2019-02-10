using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private static SceneChanger instance = new SceneChanger();

    private SceneChanger() { }

    public static SceneChanger GetInstance()
    {
        return instance;
    }

	public void SceneCharSel()
    {
        Camera.allCameras[0].Reset();
        SceneManager.LoadScene("CharSel", LoadSceneMode.Single);
    }

    public void SceneMain()
    {
        SceneManager.LoadScene("Main Game", LoadSceneMode.Single);
    }

    public void SceneCharCreation()
    {
        SceneManager.LoadScene("NewChar", LoadSceneMode.Single);
    }

    public void SceneSplashscreen()
    {
        SceneManager.LoadScene("SplashScreen", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
