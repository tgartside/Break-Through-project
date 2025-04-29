using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public TextBoxManager textBoxManager;
    public StringValue sceneToLoad;
    public GameObject textBoxGroup;
    public GameObject textBoxButton;
    public GameObject xButton;
    public GameObject confirmButton;

    void Start()
    {
        StartCoroutine(FadeIn());

        if (sceneToLoad.initialValue == "Intro")
        {
            textBoxManager.DisplayText("You are a woman in the game development industry working as a programmer." +
                                        "\nYou must confront and clear various difficulties you encounter throughout the day without burning out." +
                                        "\n\nControls:" +
                                        "\nA,D => Move left/right" +
                                        "\nSpace => Jump" +
                                        "\nE => Interact" +
                                        "\nLeft click => Grapple" +
                                        "\nEsc => Pause");
            xButton.SetActive(false);
            confirmButton.SetActive(true);
            Time.timeScale = 1.0f;
            sceneToLoad.initialValue = "Level1";
        }

        else
        {
            if(sceneToLoad.initialValue == "Level1")
            {
                textBoxManager.DisplayText("Level 1 - Before Work");
                Time.timeScale = 1.0f;
            }
            else if (sceneToLoad.initialValue == "Level2")
            {
                textBoxManager.DisplayText("Level 2 - During Work");
                Time.timeScale = 1.0f;
            }
            else if (sceneToLoad.initialValue == "Level3")
            {
                textBoxManager.DisplayText("Level 3 - End of day\n\n Watch out for the gossip!");
                Time.timeScale = 1.0f;
            }
            else if(sceneToLoad.initialValue == "Outro")
            {
                textBoxManager.DisplayText("“I’ve finished all my tasks for they day." +
                                            "\n\n Time to head home." +
                                            "\n\nI wonder if my day would have been different if I worked remotely...”");
                Time.timeScale = 1.0f;
                sceneToLoad.initialValue = "TitleScreen";
            }

            else
            {
                textBoxManager.DisplayText(sceneToLoad.initialValue);
                Time.timeScale = 1.0f;
            }

            textBoxButton.SetActive(false);
            StartCoroutine(FadeOut());
        }
         
    }

    public void closeIntro()
    {
        StartCoroutine(FadeOutIntro());
        //textBoxGroup.SetActive(false);
    }

    
    IEnumerator FadeIn()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFade>().StartFadeIn();
        yield return new WaitForSeconds(1);
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(3);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFade>().StartFadeOut();
        yield return new WaitForSeconds(1);
        textBoxGroup.SetActive(false);
        //Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneToLoad.initialValue);
    }

    IEnumerator FadeOutIntro()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFade>().StartFadeOut();
        yield return new WaitForSeconds(2);
        textBoxGroup.SetActive(false);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("TransitionScene");
    }


}
