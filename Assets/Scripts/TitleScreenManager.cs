using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    public GameObject startButton;
    public GameObject quitButton;
    public GameObject creditsButton;
    public Animator animator;

    public GameObject textBoxGroup;

    public void startCredits()
    {
        startButton.SetActive(false);
        quitButton.SetActive(false);
        creditsButton.SetActive(false);

        textBoxGroup.SetActive(true);

        animator.SetTrigger("ButtonClicked");
        //animator["CreditAnimation"].wrapMode = WrapMode.Once;
        //animator.Play("CreditAnimation");
    }

    public void CloseTextBox()
    {
        textBoxGroup.SetActive(false);
        startButton.SetActive(true);
        quitButton.SetActive(true);
        creditsButton.SetActive(true);
        animator.ResetTrigger("ButtonClicked");
    }
}
