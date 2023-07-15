using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public int currentScore;
    public int points;
    int pointValue;
    //public int currentdifficulty;
    public PopulateUI gameInfo;

    public TextMeshProUGUI score;
    public TextMeshProUGUI timer;
    public bool TimerOn = false;

    public GameObject MenuButton;
    public GameObject NextSetButton;

    public SnapController snaps;

    public GameObject moveable1;
    public GameObject moveable2;
    public GameObject moveable3;
    public GameObject moveable4;
    public GameObject moveable5;
    public GameObject moveable6;

    bool sol1;
    bool sol2;
    bool sol3;
    bool sol4;

    float currentTime;
    int MaxTime = 30;

    public void Submit()
    {
        //function that calls to check problem solutions.
        pointValue = 1000;

        if (gameInfo.gamemode)
        {
            //if time trials, generate a new set of problems and award score for that level.
            Debug.Log("Submit Time Trials");
            points += pointValue;
            score.text = "Score: " + points.ToString();

            foreach(Transform snapPoint in snaps.snapPoints)
            {
                //get value from snap point
            }

            for (int i = 0; i > 3; i++)
            {
                //solve for each problem


                //set correct or false booleans
            }

            //if questions are correct, active next set button

            NextSetButton.SetActive(true);
            //else if questions were incorrect, activate return to menu button


        } else
        {
            //if practice set a new button to active (return to main menu)
            MenuButton.SetActive(true);

            foreach (Transform snapPoint in snaps.snapPoints)
            {
                //get value from snap point
            }

            for (int i = 0; i > 3; i++)
            {
                //solve for each problem


                //set correct or false booleans
            }

            

        }
    }

    public void Reset()
    {
        moveable1.transform.localPosition = moveable1.GetComponentInChildren<DragableObject>().OriginalPosition;
        moveable2.transform.localPosition = moveable2.GetComponentInChildren<DragableObject>().OriginalPosition;
        moveable3.transform.localPosition = moveable3.GetComponentInChildren<DragableObject>().OriginalPosition;
        moveable4.transform.localPosition = moveable4.GetComponentInChildren<DragableObject>().OriginalPosition;
        moveable5.transform.localPosition = moveable5.GetComponentInChildren<DragableObject>().OriginalPosition;
        moveable6.transform.localPosition = moveable6.GetComponentInChildren<DragableObject>().OriginalPosition;
    }

    public void Start()
    {
        currentTime = 30;
        gameInfo = gameObject.AddComponent<PopulateUI>();
    }


    public void NextRun()
    {
        currentTime = 30;
    }

    private void Update()
    {
        if (timer.enabled)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                Debug.Log(currentTime);
                updateTimer(currentTime);
            }
            else
            {
                timer.enabled = false;
                Submit();
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime * 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timer.text = string.Format("{0:00} : {1:00}", seconds, minutes);
    }

}
