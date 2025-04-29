using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class interactableObject : CollidableObject
{
    public TextBoxManager textBoxManager;
    public string levelName;
    public Vector2 playerPosition;
    public string textToDisplay;
    public StringValue sceneToLoad;


    //public BooleanValue changeSceneAfterFade;

    protected override void OnCollided(GameObject collidedObject)
    {
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("collided tag " + this.tag);
            OnInteract();
        }
    }

    private void OnInteract()
    {
        if (this.tag == "Scene Changer")
        {
            sceneToLoad.initialValue = levelName;
            logic.ChangeScene(levelName);
        }

        else if (this.tag == "Collectable")
        {
            logic.collectedGoalCount += 1;
            logic.goalCountText.text = logic.collectedGoalCount + "/" + logic.levelGoalCount;
            this.gameObject.SetActive(false);
            logic.inactiveObjects.Add(this.gameObject);

            textBoxManager.DisplayText(textToDisplay);

            if (logic.collectedGoalCount == logic.levelGoalCount)
            {
                logic.exitDoor.SetActive(true);
            }
        }

    }
}
