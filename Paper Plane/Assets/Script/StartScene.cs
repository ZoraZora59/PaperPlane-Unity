using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class StartScene : MonoBehaviour {

    // Use this for initialization
    public void hard()
    {
        if (!PlayerPrefs.HasKey("HardCheck"))
            PlayerPrefs.SetInt("HardCheck", 1);
        if(PlayerPrefs.HasKey("HardValue"))
            PlayerPrefs.SetInt("HardValue", 50);
        if (!PlayerPrefs.HasKey("HighScore"))
            PlayerPrefs.SetInt("HighScore", 0);
        SceneManager.LoadScene("HardCheck");
    }
    public void start()
    {
        SceneManager.LoadScene("Start");
    }
    public void quit()
    {
        Application.Quit();
    }
}