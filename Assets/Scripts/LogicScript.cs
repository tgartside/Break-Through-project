using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{

    public bool canPause = true;
    public int levelGoalCount;
    public int collectedGoalCount;
    public List<GameObject> inactiveObjects;

    [Header("Object references")]
    public GameObject pauseMenu;
    public PlayerScript player;
    public GameObject gameOverScreen;
    public TextMeshProUGUI lifeCountText;
    public TextMeshProUGUI goalCountText;
    public GameObject exitDoor;

    [Header("Scriptable objects")]
    public VectorValue playerStorage;
    public StringValue sceneToLoad;


    private void Start()
    {
        Time.timeScale = 1.0f;
        if (SceneManager.GetActiveScene().name.Equals("Level1"))
        {
            player.lifeCount.initialValue = 5;
        }
        if (!SceneManager.GetActiveScene().name.Equals("TitleScreen") && !SceneManager.GetActiveScene().name.Equals("TransitionScene"))
        {
            collectedGoalCount = 0;
            lifeCountText.text = " Lives: " + player.lifeCount.initialValue;
            goalCountText.text = collectedGoalCount + "/" + levelGoalCount;
            StartCoroutine(FadeIn());
        }
        
    }

    void Update()
    {
        if (!SceneManager.GetActiveScene().name.Equals("TitleScreen") && !SceneManager.GetActiveScene().name.Equals("TransitionScene"))
        {
            if (player.lifeCount.initialValue == 0)
            {
                GameOver();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (canPause)
                {
                    if (pauseMenu.activeSelf)
                    {
                        Unpause();
                    }

                    else
                    {
                        pauseMenu.SetActive(true);
                        Time.timeScale = 0f;
                    }
                }
            }
        }
    }

    public void Unpause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        //player.playerIsAlive = false;
        gameOverScreen.SetActive(true);
        canPause = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene("level1");
        player.transform.position = player.spawnPoint;
        player.lifeCount.initialValue = 5;
        player.rb.velocity = new Vector2(0,0);
        lifeCountText.text = " Lives: " + player.lifeCount.initialValue;
        gameOverScreen.SetActive(false);
        //player.playerIsAlive= true;
        Time.timeScale = 1f;
        canPause = true;
        player.gameObject.SetActive(true);

    }
    /*public void LoadScene(string levelName, Vector2 playerPosition)
    {
        playerStorage.initialValue = playerPosition;
        SceneManager.LoadScene(levelName);
    }*/

    public void ChangeScene(string levelName)
    {
        sceneToLoad.initialValue = levelName;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFade>().StartFadeIn();
        yield return new WaitForSeconds(1);
    }

    IEnumerator FadeOut()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFade>().StartFadeOut();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("TransitionScene");
    }

}
