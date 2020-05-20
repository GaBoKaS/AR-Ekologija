using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int TotalTime; //         Total time for a wave
    public int timePassed; //        Time passed
    public int timeLeft; //          Time left until next wave

    private int currentWave; //         number of current wave

    public int StartingItemCount; //   how many items are at the beginning of the wave
    public int itemIncrease; //        increase of items between each wave
    public int CurrentItemCount; //    how many items will be spawned at the beggining of the wave

    public int extraSpawnCount; //     how many extra items will be spawned every couple of seconds
    public int ExtraSpawnFrequency; // how often extra items are spawned
    private GameObject GameController;
    public bool GameStarted;

    void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController").gameObject;
        currentWave = 1;
        StartingItemCount = 10;
        GameStarted = false;
    }
    private void Update()
    {
        GameController.GetComponent<DisplayScore>().SecondsLeft = timeLeft;
        GameController.GetComponent<DisplayScore>().Wave = currentWave;
        if (GameOver) {
            if(GameController.GetComponent<DisplayScore>().Score < 0)
            {
                GameController.GetComponent<DisplayScore>().titleString = "Praradote visus taškus";
            }
            else
            {
                GameController.GetComponent<DisplayScore>().titleString = "Laikas Baigėsi";
            }
        }
    }
    public void StartWaveMode()
    {
        GameStarted = true;
        StartCoroutine(ItemSpawner(1f));
    }
    public bool GameOver;
    IEnumerator ItemSpawner(float waitTime)
    {
        while (true)
        {
            SpawnItems(StartingItemCount + itemIncrease * currentWave);
            
            while (true)
            {
                timePassed++;
                timeLeft = TotalTime - timePassed;
                if (timePassed % ExtraSpawnFrequency == 0 && timeLeft > 10)
                { //Spawns extra items
                    SpawnItems(extraSpawnCount-4+currentWave);
                }
                // timer exit conditions
                if (!(ItemExists("Plastic") || ItemExists("Paper") || ItemExists("Glass")))
                    break;
                if( GameController.GetComponent<DisplayScore>().Score < 0)
                {
                    GameOver = true;
                    break;
                }
                if (timeLeft == 0)
                {
                    if ( ItemExists("Plastic") || ItemExists("Paper") || ItemExists("Glass") )
                    {
                        GameOver = true;
                        break;
                    }
                    else break;
                }
                else yield return new WaitForSeconds(waitTime);
            }
            if (GameOver)
            {
                Time.timeScale = 0.0f;
                GameController.GetComponent<DisplayScore>().ChangeColor("Default");
                if(GameController.GetComponent<DisplayScore>().Score > PlayerPrefs.GetInt("HighScore"))
                {
                    PlayerPrefs.SetInt("HighScore", GameController.GetComponent<DisplayScore>().Score);
                }
                yield break;
            }
            else
            {
                currentWave++;
                timePassed = 0;
                TotalTime = TotalTime + currentWave;
                timeLeft = TotalTime;
                yield return new WaitForSeconds(waitTime);
            } 
        }
    }
    bool ItemExists(string item)
    {
        if (GameObject.FindGameObjectsWithTag(item).Length == 0) Debug.Log(item + " Does not Exist");
        else Debug.Log(item + " Does Exist");
        if (GameObject.FindGameObjectsWithTag(item).Length == 0) return false;
        else return true;
    }
    void SpawnItems(int Number)
    { //Spawns Number of items
        for(int i = 0; i<Number; i++)
        {
            //SpawnItems(CurrentItemCount);
            GameController.GetComponent<GameController>().SpawnObjects();
        }
    }
}
