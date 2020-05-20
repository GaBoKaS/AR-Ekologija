using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject PlaneFinder;
    public bool PlacingEnabled;

    void Start()
    {
        MenuWindow.gameObject.SetActive(false);
        MenuOpened = false;
        Time.timeScale = 0.0f;
        GameObject.FindGameObjectWithTag("Environment").GetComponent<MirrorRotation>().mirror = true;
    }
    // Game Platform placing ----------------------------------
    public GameObject PlacementButton;
    public GameObject PlacementText;
    public void EnablePlacing()
    {
        PlacementButton.gameObject.SetActive(true);
        PlacementText.gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("Environment").GetComponent<MirrorRotation>().mirror = true;
        PlaneFinder.SetActive(true);
        PlacingEnabled = true;
        MenuWindow.gameObject.SetActive(false);
        MenuOpened = false;
        Time.timeScale = 0.0f;
    }
    public void DisablePlacing()
    {
        PlacementButton.gameObject.SetActive(false);
        PlacementText.gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Environment").GetComponent<MirrorRotation>().mirror = false;
        PlaneFinder.SetActive(false);
        PlacingEnabled = false;
        MenuOpened = false;
        Time.timeScale = 1.0f;
        GameObject WaveManager = GameObject.FindGameObjectWithTag("GameController");
        if ( !WaveManager.GetComponent<WaveManager>().GameStarted )
        WaveManager.GetComponent<WaveManager>().StartWaveMode();
    }
    // Open Menu ----------------------------------------------
    public GameObject MenuWindow;
    public bool MenuOpened;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { // Open and Exit menu
            if (MenuOpened)
            {
                MenuWindow.gameObject.SetActive(false);
                MenuOpened = false;
                Time.timeScale = 1.0f;
            }
            else
            {
                MenuWindow.gameObject.SetActive(true);
                MenuOpened = true;
                Time.timeScale = 0.0f;
            }
        }
    }

    // Menu Buttons--------------------------------------------
    public void ExitMenu()
    { //Exit menu and resume the game
        MenuWindow.gameObject.SetActive(false);
        MenuOpened = false;
        Time.timeScale = 0.0f;
    }
    public void ResetScene()
    { //Reset game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("cene name is:" + SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

}
