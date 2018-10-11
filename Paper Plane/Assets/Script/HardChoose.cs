using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class HardChoose : MonoBehaviour {
    public AudioSource setSound;
    public void reset()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        setSound.Play();
    }
	public void easy()
    {
        PlayerPrefs.SetInt("HardCheck", 5);
        PlayerPrefs.SetInt("HardValue", 50);
        setSound.Play();
        SceneManager.LoadScene("Play");
    }
    public void normal()
    {
        PlayerPrefs.SetInt("HardCheck", 11);
        PlayerPrefs.SetInt("HardValue", 100);
        setSound.Play();
        SceneManager.LoadScene("Play");
    }
    public void hell()
    {
        PlayerPrefs.SetInt("HardCheck", 20);
        PlayerPrefs.SetInt("HardValue", 300);
        setSound.Play();
        SceneManager.LoadScene("Play");
    }
}
