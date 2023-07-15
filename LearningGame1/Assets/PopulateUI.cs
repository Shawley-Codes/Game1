using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;
using TMPro;
using System.Linq;

public class PopulateUI : MonoBehaviour
{

    public TextMeshProUGUI Q1UI;
    public TextMeshProUGUI Q2UI;
    public TextMeshProUGUI Q3UI;
    public TextMeshProUGUI Q4UI;

    //False for practice, True for TimeTrial
    public bool gamemode = false;
    public int runs;

    public TextMeshProUGUI score;
    public TextMeshProUGUI timer;

    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    public GameObject obj5;
    public GameObject obj6;

    public GameObject snap1_1;
    public GameObject snap1_2;
    public GameObject snap1_3;
    public GameObject snap1_4;
    public GameObject snap2_1_easy;
    public GameObject snap2_2_easy;
    public GameObject snap2_3_easy;
    public GameObject snap2_4_easy;
    public GameObject snap2_1_medium;
    public GameObject snap2_2_medium;
    public GameObject snap2_3_medium;
    public GameObject snap2_4_medium;
    public GameObject snap2_1_hard;
    public GameObject snap2_2_hard;
    public GameObject snap2_3_hard;
    public GameObject snap2_4_hard;
    public GameObject snap3_1_easy;
    public GameObject snap3_2_easy;
    public GameObject snap3_3_easy;
    public GameObject snap3_4_easy;
    public GameObject snap3_1_medium;
    public GameObject snap3_2_medium;
    public GameObject snap3_3_medium;
    public GameObject snap3_4_medium;
    public GameObject snap3_1_hard;
    public GameObject snap3_2_hard;
    public GameObject snap3_3_hard;
    public GameObject snap3_4_hard;
    public GameObject snap4_1_easy;
    public GameObject snap4_2_easy;
    public GameObject snap4_3_easy;
    public GameObject snap4_4_easy;
    public GameObject snap4_1_medium;
    public GameObject snap4_2_medium;
    public GameObject snap4_3_medium;
    public GameObject snap4_4_medium;
    public GameObject snap4_1_hard;
    public GameObject snap4_2_hard;
    public GameObject snap4_3_hard;
    public GameObject snap4_4_hard;

    public void StartGameEasy()
    {
        GenerateProblems problems = gameObject.AddComponent<GenerateProblems>();
        Q1UI.text = problems.problem1;
        Q2UI.text = problems.problem2;
        Q3UI.text = problems.problem3;
        Q4UI.text = problems.problem4;

        string[] values = problems.valuesList;
        //randomize the order of the gameplay objects
        values = shuffle(values);

        obj1.GetComponentInChildren<TMP_Text>().text = values[0];
        obj2.GetComponentInChildren<TMP_Text>().text = values[1];
        obj3.GetComponentInChildren<TMP_Text>().text = values[2];
        obj4.GetComponentInChildren<TMP_Text>().text = values[3];
        obj5.GetComponentInChildren<TMP_Text>().text = values[4];
        obj6.GetComponentInChildren<TMP_Text>().text = values[5];

        SetSnapPoint(1, problems.RNGList);

        //if time trails increase runs
        if (gamemode)
        {
            runs += 1;
        }
    }
    public void StartGameMedium()
    {
        GenerateProblems problems = new GenerateProblems(2);
        Q1UI.text = problems.problem1;
        Q2UI.text = problems.problem2;
        Q3UI.text = problems.problem3;
        Q4UI.text = problems.problem4;

        string[] values = problems.valuesList;
        //randomize the order of the gameplay objects
        values = shuffle(values);

        obj1.GetComponentInChildren<TMP_Text>().text = values[0];
        obj2.GetComponentInChildren<TMP_Text>().text = values[1];
        obj3.GetComponentInChildren<TMP_Text>().text = values[2];
        obj4.GetComponentInChildren<TMP_Text>().text = values[3];
        obj5.GetComponentInChildren<TMP_Text>().text = values[4];
        obj6.GetComponentInChildren<TMP_Text>().text = values[5];

        SetSnapPoint(1, problems.RNGList);

        //if time trails increase runs
        if (gamemode)
        {
            runs += 1;
        }

    }
    public void StartGameHard()
    {
        GenerateProblems problems = new GenerateProblems(3);
        Q1UI.text = problems.problem1;
        Q2UI.text = problems.problem2;
        Q3UI.text = problems.problem3;
        Q4UI.text = problems.problem4;

        string[] values = problems.valuesList;
        //randomize the order of the gameplay objects
        values = shuffle(values);

        obj1.GetComponentInChildren<TMP_Text>().text = values[0];
        obj2.GetComponentInChildren<TMP_Text>().text = values[1];
        obj3.GetComponentInChildren<TMP_Text>().text = values[2];
        obj4.GetComponentInChildren<TMP_Text>().text = values[3];
        obj5.GetComponentInChildren<TMP_Text>().text = values[4];
        obj6.GetComponentInChildren<TMP_Text>().text = values[5];

        SetSnapPoint(1, problems.RNGList);

        //if time trails increase runs
        if (gamemode)
        {
            runs += 1;
        }
    }

    public void StartTimeTrial()
    {
        //set gamemode for button, and reset runs
        gamemode = true;
        runs = 0;
        score.enabled = true;
        timer.enabled = true;


        StartGameEasy();
    }

    string[] shuffle(string[] texts)
    {
        // Knuth shuffle algorithm
        for (int t = 0; t < texts.Length; t++)
        {
            string tmp = texts[t];
            int r = Random.Range(t, texts.Length);
            texts[t] = texts[r];
            texts[r] = tmp;
        }
        return texts;
    }

    public void SetSnapPoint(int difficulty, int[] rng)
    {
        
        switch (difficulty){
            case 1:
                switch (rng[0])
                {
                    case 0:
                        snap1_1.active = true;
                        break;
                    case 1:
                        snap2_1_easy.active = true;
                        break;
                    case 2:
                        snap3_1_easy.active = true;
                        break;
                    case 3:
                        snap4_1_easy.active = true;
                        break;
                    default:
                        break;
                }
                switch (rng[1])
                {
                    case 0:
                        snap1_1.active = true;
                        break;
                    case 1:
                        snap2_1_easy.active = true;
                        break;
                    case 2:
                        snap3_1_easy.active = true;
                        break;
                    case 3:
                        snap4_1_easy.active = true;
                        break;
                    default:
                        break;
                }
                switch (rng[2])
                {
                    case 0:
                        snap1_2.active = true;
                        break;
                    case 1:
                        snap2_2_easy.active = true;
                        break;
                    case 2:
                        snap3_2_easy.active = true;
                        break;
                    case 3:
                        snap4_2_easy.active = true;
                        break;
                    default:
                        break;
                }
                switch (rng[3])
                {
                    case 0:
                        snap1_2.active = true;
                        break;
                    case 1:
                        snap2_2_easy.active = true;
                        break;
                    case 2:
                        snap3_2_easy.active = true;
                        break;
                    case 3:
                        snap4_2_easy.active = true;
                        break;
                    default:
                        break;
                }
                switch (rng[4])
                {
                    case 0:
                        snap1_3.active = true;
                        break;
                    case 1:
                        snap2_3_easy.active = true;
                        break;
                    case 2:
                        snap3_3_easy.active = true;
                        break;
                    case 3:
                        snap4_3_easy.active = true;
                        break;
                    default:
                        break;
                }
                switch (rng[5])
                {
                    case 0:
                        snap1_4.active = true;
                        break;
                    case 1:
                        snap2_4_easy.active = true;
                        break;
                    case 2:
                        snap3_4_easy.active = true;
                        break;
                    case 3:
                        snap4_4_easy.active = true;
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch (rng[0])
                {
                    case 0:
                        snap1_1.active = true;
                        break;
                    case 1:
                        snap2_1_medium.active = true;
                        break;
                    case 2:
                        snap3_1_medium.active = true;
                        break;
                    case 3:
                        snap4_1_medium.active = true;
                        break;
                    default:
                        break;
                }
                switch (rng[1])
                {
                    case 0:
                        snap1_1.active = true;
                        break;
                    case 1:
                        snap2_1_medium.active = true;
                        break;
                    case 2:
                        snap3_1_medium.active = true;
                        break;
                    case 3:
                        snap4_1_medium.active = true;
                        break;
                    default:
                        break;
                }
                switch (rng[2])
                {
                    case 0:
                        snap1_2.active = true;
                        break;
                    case 1:
                        snap2_2_medium.active = true;
                        break;
                    case 2:
                        snap3_2_medium.active = true;
                        break;
                    case 3:
                        snap4_2_medium.active = true;
                        break;
                    default:
                        break;
                }
                switch (rng[3])
                {
                    case 0:
                        snap1_2.active = true;
                        break;
                    case 1:
                        snap2_2_medium.active = true;
                        break;
                    case 2:
                        snap3_2_medium.active = true;
                        break;
                    case 3:
                        snap4_2_medium.active = true;
                        break;
                    default:
                        break;
                }
                switch (rng[4])
                {
                    case 0:
                        snap1_3.active = true;
                        break;
                    case 1:
                        snap2_3_medium.active = true;
                        break;
                    case 2:
                        snap3_3_medium.active = true;
                        break;
                    case 3:
                        snap4_3_medium.active = true;
                        break;
                    default:
                        break;
                }
                switch (rng[5])
                {
                    case 0:
                        snap1_4.active = true;
                        break;
                    case 1:
                        snap2_4_medium.active = true;
                        break;
                    case 2:
                        snap3_4_medium.active = true;
                        break;
                    case 3:
                        snap4_4_medium.active = true;
                        break;
                    default:
                        break;
                }
                break;
            case 3:
                switch (rng[0])
                {
                    case 0:
                        snap1_1.active = true;
                        break;
                    case 1:
                        snap2_1_hard.active = true;
                        break;
                    case 2:
                        snap3_1_hard.active = true;
                        break;
                    case 3:
                        snap4_1_hard.active = true;
                        break;
                    default:
                        break;
                }
                switch (rng[1])
                {
                    case 0:
                        snap1_1.active = true;
                        break;
                    case 1:
                        snap2_1_hard.active = true;
                        break;
                    case 2:
                        snap3_1_hard.active = true;
                        break;
                    case 3:
                        snap4_1_hard.active = true;
                        break;
                    default:
                        break;
                }
                switch (rng[2])
                {
                    case 0:
                        snap1_2.active = true;
                        break;
                    case 1:
                        snap2_2_hard.active = true;
                        break;
                    case 2:
                        snap3_2_hard.active = true;
                        break;
                    case 3:
                        snap4_2_hard.active = true;
                        break;
                    default:
                        break;
                }
                switch (rng[3])
                {
                    case 0:
                        snap1_2.active = true;
                        break;
                    case 1:
                        snap2_2_hard.active = true;
                        break;
                    case 2:
                        snap3_2_hard.active = true;
                        break;
                    case 3:
                        snap4_2_hard.active = true;
                        break;
                    default:
                        break;
                }
                switch (rng[4])
                {
                    case 0:
                        snap1_3.active = true;
                        break;
                    case 1:
                        snap2_3_hard.active = true;
                        break;
                    case 2:
                        snap3_3_hard.active = true;
                        break;
                    case 3:
                        snap4_3_hard.active = true;
                        break;
                    default:
                        break;
                }
                switch (rng[5])
                {
                    case 0:
                        snap1_4.active = true;
                        break;
                    case 1:
                        snap2_4_hard.active = true;
                        break;
                    case 2:
                        snap3_4_hard.active = true;
                        break;
                    case 3:
                        snap4_4_hard.active = true;
                        break;
                    default:
                        break;
                }
                break;
            default:

                break;
        }





    }


}
