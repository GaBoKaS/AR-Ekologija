using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject textField;
    void Start()
    {
        if(PlayerPrefs.GetInt("HighScore").ToString() != null)
            textField.GetComponent<TMPro.TextMeshProUGUI>().text = PlayerPrefs.GetInt("HighScore").ToString();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
}
