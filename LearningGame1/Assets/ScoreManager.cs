using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ScoreManager : MonoBehaviour
{
    public int currentScore;
    public int points;
    int pointValue;
    //public int currentdifficulty;
    public PopulateUI gameInfo;
    public ArrayList OrinalProblems;
    public string[] OriginalStrings;
    public string[] valuesList;
    SortedDictionary<Transform, string> sortedDict;
    public int[] rngList;

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

    public GameObject right1;
    public GameObject right2;
    public GameObject right3;
    public GameObject right4;
    public GameObject wrong1;
    public GameObject wrong2;
    public GameObject wrong3;
    public GameObject wrong4;
    public GameObject instructionText;


    bool sol1;
    bool sol2;
    bool sol3;
    bool sol4;

    float currentTime;
    public int MaxTime = 30;

    public GameObject ResetButton;

    public TextMeshProUGUI Q1;
    public TextMeshProUGUI Q2;
    public TextMeshProUGUI Q3;
    public TextMeshProUGUI Q4;
    public TextMeshProUGUI OrigTXT;

    public AudioSource src;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip chalk1;
    public AudioClip chalk2;
    public AudioClip chalk3;
    public AudioClip chalk4;
    public AudioClip chalk5;

    IEnumerator Upload(int points)
    {
        string url = "https://capstone.rasinnovation.org:8080/process_points";
        string game = "game1";

        WWWForm form = new WWWForm();
        form.AddField("game_name", game);
        form.AddField("points", points);

        UnityWebRequest request = UnityWebRequest.Post(url, form);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error sending form: " + request.error);
        } else
        {
            Debug.Log("Form Data sent successfully");
        }
    }

    public void Submit()
    {
        //function that calls to check problem solutions.
        pointValue = 1000;
        snaps = gameObject.GetComponent<SnapController>();
        gameInfo = gameObject.GetComponentInChildren<PopulateUI>();
        //Debug.Log(gameInfo.gamemode);
        OrinalProblems = gameInfo.ProblemList;
        rngList = gameInfo.rngList;
        valuesList = gameInfo.values;
        timer.enabled = false;
        //OrinalProblems = gameInfo.ProblemList;
        
        if (gameInfo.gamemode)
        {
            //if time trials, generate a new set of problems and award score for that level.
            Debug.Log("Submit Time Trials");
            OriginalStrings = gameInfo.Originals;

            //sorted dictionary will not work so a method instead is to check for which snaps will correlate to each problem
            //Debug.Log(snaps.valueDict);
            Transform snapPoint1 = null;
            Transform snapPoint2 = null; // q1
            Transform snapPoint3 = null;
            Transform snapPoint4 = null; // q2
            Transform snapPoint5 = null; // q3
            Transform snapPoint6 = null; // q4
            string snapValue1 = "";
            string snapValue2 = "";
            string snapValue3 = "";
            string snapValue4 = "";
            string snapValue5 = "";
            string snapValue6 = "";
            bool q1e = false;
            bool q2e = false;
            bool q3e = false;
            bool q4e = false;

            foreach (Transform key in snaps.valueDict.Keys)
            {
                //Debug.Log(key.name);
                if (key.name == "SnapPoint1_1" || key.name == "SnapPoint2_1_easy" || key.name == "SnapPoint2_1_medium" || key.name == "SnapPoint2_1_hard" || 
                    key.name == "SnapPoint3_1_easy" || key.name == "SnapPoint3_1_medium" || key.name == "SnapPoint3_1_hard")
                {
                    //Debug.Log("name comp worked");
                    if (snapPoint1 == null)
                    {
                        //Debug.Log("Snap point 1 should have a key");
                        snapPoint1 = key;
                        snapValue1 = snaps.valueDict[key];
                    } else
                    {
                        //Debug.Log("Snap point 2 should have a key");
                        snapPoint2 = key;
                        snapValue2 = snaps.valueDict[key];
                        q1e = true;
                    }
                }

                if (key.name == "SnapPoint1_2" || key.name == "SnapPoint2_2_easy" || key.name == "SnapPoint2_2_medium" || key.name == "SnapPoint2_2_hard" ||
                    key.name == "SnapPoint3_2_easy" || key.name == "SnapPoint3_2_medium" || key.name == "SnapPoint3_2_hard")
                {
                    if (snapPoint3 == null)
                    {
                        snapPoint3 = key;
                        snapValue3 = snaps.valueDict[key];
                    }
                    else
                    {
                        snapPoint4 = key;
                        snapValue4 = snaps.valueDict[key];
                        q2e = true;
                    }
                }

                if (key.name == "SnapPoint1_3" || key.name == "SnapPoint2_3_easy" || key.name == "SnapPoint2_3_medium" || key.name == "SnapPoint2_3_hard" ||
                    key.name == "SnapPoint3_3_easy" || key.name == "SnapPoint3_3_medium" || key.name == "SnapPoint3_3_hard")
                {
                    snapPoint5 = key;
                    snapValue5 = snaps.valueDict[key];
                    q3e = true;
                }

                if (key.name == "SnapPoint1_4" || key.name == "SnapPoint2_4_easy" || key.name == "SnapPoint2_4_medium" || key.name == "SnapPoint2_4_hard" ||
                    key.name == "SnapPoint3_4_easy" || key.name == "SnapPoint3_4_medium" || key.name == "SnapPoint3_4_hard")
                {
                    snapPoint6 = key;
                    snapValue6 = snaps.valueDict[key];
                    q4e = true;
                }
            }

            
            
            //get question 1 correct
            if (q1e)
            {
                Debug.Log("Q1");
                sol1 = Correct(snapValue1, snapPoint1, snapValue2, snapPoint2, (ArrayList)OrinalProblems[0], rngList);
            } else
            {
                sol1 = false;
            }
            if (q2e)
            {
                Debug.Log("Q2");
                sol2 = Correct(snapValue3, snapPoint3, snapValue4, snapPoint4, (ArrayList)OrinalProblems[1], rngList);
            }
            else
            {
                sol2 = false;
            }
            if (q3e)
            {
                Debug.Log("Q3");
                sol3 = Correct(snapValue5, snapPoint5, (ArrayList)OrinalProblems[2], rngList);
            }
            else
            {
                sol3 = false;
            }
            if (q4e)
            {
                Debug.Log("Q4");
                sol4 = Correct(snapValue6, snapPoint6, (ArrayList)OrinalProblems[3], rngList);
            }
            else
            {
                sol4 = false;
            }

            //Debug.Log(sol1);
            //Debug.Log(sol2);
            //Debug.Log(sol3);
            //Debug.Log(sol4);


            if (sol1)
            {
                right1.SetActive(true);
            } else
            {
                wrong1.SetActive(true);
            }
            if (sol2)
            {
                right2.SetActive(true);
            }
            else
            {
                wrong2.SetActive(true);
            }
            if (sol3)
            {
                right3.SetActive(true);
            }
            else
            {
                wrong3.SetActive(true);
            }
            if (sol4)
            {
                right4.SetActive(true);
            }
            else
            {
                wrong4.SetActive(true);
            }


            //if questions are correct, active next set button
            if (sol1 && sol2 && sol3 && sol4)
            {
                points += pointValue + (pointValue * (Mathf.FloorToInt(currentTime % 60) / 10));
                score.text = "Score: " + points.ToString();
                NextSetButton.SetActive(true);
                src.clip = win;
                src.Play();
            } else
            {
                //send score to server via postrequest
                if (points > 0)
                {
                    Debug.Log("Sending: " + points);
                    Upload(points);
                }
                this.showOriginals();
                
                MenuButton.SetActive(true);
                src.clip = lose;
                src.Play();
            }
            //else if questions were incorrect, activate return to menu button

        
        } else
        {
            //if practice set a new button to active (return to main menu)
            MenuButton.SetActive(true);
            instructionText.SetActive(false);
            //sorted dictionary will not work so a method instead is to check for which snaps will correlate to each problem
            //Debug.Log(snaps.valueDict);
            Transform snapPoint1 = null;
            Transform snapPoint2 = null; // q1
            Transform snapPoint3 = null;
            Transform snapPoint4 = null; // q2
            Transform snapPoint5 = null; // q3
            Transform snapPoint6 = null; // q4
            string snapValue1 = "";
            string snapValue2 = "";
            string snapValue3 = "";
            string snapValue4 = "";
            string snapValue5 = "";
            string snapValue6 = "";
            bool q1e = false;
            bool q2e = false;
            bool q3e = false;
            bool q4e = false;

            foreach (Transform key in snaps.valueDict.Keys)
            {
                //Debug.Log(key.name);
                if (key.name == "SnapPoint1_1" || key.name == "SnapPoint2_1_easy" || key.name == "SnapPoint2_1_medium" || key.name == "SnapPoint2_1_hard" ||
                    key.name == "SnapPoint3_1_easy" || key.name == "SnapPoint3_1_medium" || key.name == "SnapPoint3_1_hard")
                {
                    //Debug.Log("name comp worked");
                    if (snapPoint1 == null)
                    {
                        //Debug.Log("Snap point 1 should have a key");
                        snapPoint1 = key;
                        snapValue1 = snaps.valueDict[key];
                    }
                    else
                    {
                        //Debug.Log("Snap point 2 should have a key");
                        snapPoint2 = key;
                        snapValue2 = snaps.valueDict[key];
                        q1e = true;
                    }
                }

                if (key.name == "SnapPoint1_2" || key.name == "SnapPoint2_2_easy" || key.name == "SnapPoint2_2_medium" || key.name == "SnapPoint2_2_hard" ||
                    key.name == "SnapPoint3_2_easy" || key.name == "SnapPoint3_2_medium" || key.name == "SnapPoint3_2_hard")
                {
                    if (snapPoint3 == null)
                    {
                        snapPoint3 = key;
                        snapValue3 = snaps.valueDict[key];
                    }
                    else
                    {
                        snapPoint4 = key;
                        snapValue4 = snaps.valueDict[key];
                        q2e = true;
                    }
                }

                if (key.name == "SnapPoint1_3" || key.name == "SnapPoint2_3_easy" || key.name == "SnapPoint2_3_medium" || key.name == "SnapPoint2_3_hard" ||
                    key.name == "SnapPoint3_3_easy" || key.name == "SnapPoint3_3_medium" || key.name == "SnapPoint3_3_hard")
                {
                    snapPoint5 = key;
                    snapValue5 = snaps.valueDict[key];
                    q3e = true;
                }

                if (key.name == "SnapPoint1_4" || key.name == "SnapPoint2_4_easy" || key.name == "SnapPoint2_4_medium" || key.name == "SnapPoint2_4_hard" ||
                    key.name == "SnapPoint3_4_easy" || key.name == "SnapPoint3_4_medium" || key.name == "SnapPoint3_4_hard")
                {
                    snapPoint6 = key;
                    snapValue6 = snaps.valueDict[key];
                    q4e = true;
                }
            }



            //get question 1 correct
            if (q1e)
            {
                Debug.Log("Q1");
                sol1 = Correct(snapValue1, snapPoint1, snapValue2, snapPoint2, (ArrayList)OrinalProblems[0], rngList);
                Debug.Log(sol1);
            }
            else
            {
                sol1 = false;
            }
            if (q2e)
            {
                Debug.Log("Q2");
                sol2 = Correct(snapValue3, snapPoint3, snapValue4, snapPoint4, (ArrayList)OrinalProblems[1], rngList);
                Debug.Log(sol2);
            }
            else
            {
                sol2 = false;
            }
            if (q3e)
            {
                Debug.Log("Q3");
                sol3 = Correct(snapValue5, snapPoint5, (ArrayList)OrinalProblems[2], rngList);
                Debug.Log(sol3);
            }
            else
            {
                sol3 = false;
            }
            if (q4e)
            {
                Debug.Log("Q4");
                sol4 = Correct(snapValue6, snapPoint6, (ArrayList)OrinalProblems[3], rngList);
                Debug.Log(sol4);
            }
            else
            {
                sol4 = false;
            }

            //Debug.Log(sol1);
            //Debug.Log(sol2);
            //Debug.Log(sol3);
            //Debug.Log(sol4);


            if (sol1)
            {
                right1.SetActive(true);
            }
            else
            {
                wrong1.SetActive(true);
            }
            if (sol2)
            {
                right2.SetActive(true);
            }
            else
            {
                wrong2.SetActive(true);
            }
            if (sol3)
            {
                right3.SetActive(true);
            }
            else
            {
                wrong3.SetActive(true);
            }
            if (sol4)
            {
                right4.SetActive(true);
            }
            else
            {
                wrong4.SetActive(true);
            }

            ResetButton.SetActive(true);
            if (sol1 && sol2 && sol3 && sol4)
            {
                src.clip = win;
                src.Play();
            } else
            {
                src.clip = lose;
                src.Play();
            }


        }
    }

    public bool Correct(string snapValue, Transform snapPoint, string snapValue2, Transform snapPoint2, ArrayList Problem, int[] rng)
    {

        int x;
        int y;
        //int z;
        string op;
        //x op y = z
        

        //get x from snap or original problem
        if ((rng[0] == 0 || rng[1] == 0 || rng[2] == 0 || rng[3] == 0 || rng[4] == 0 || rng[5] == 0) && Problem[0] != " " && Problem[0] != "  " && Problem[0] != "   ")
        {
            //Debug.Log("x = " + Problem[0]);
            x = (int)Problem[0];
        } else if(snapPoint.name == "SnapPoint1_1" || snapPoint.name == "SnapPoint1_2" || snapPoint.name == "SnapPoint1_3" || snapPoint.name == "SnapPoint1_4")
        {
            x = int.Parse(snapValue);
        } else if(snapPoint2.name == "SnapPoint1_1" || snapPoint2.name == "SnapPoint1_2" || snapPoint2.name == "SnapPoint1_3" || snapPoint2.name == "SnapPoint1_4")
        {
            x = int.Parse(snapValue2);
        } else
        {
            //x = 0; //x cannot be 0 so z will always be incorrect
            x = (int)Problem[0];
        }

        //get op
        if ((rng[0] == 1 || rng[1] == 1 || rng[2] == 1 || rng[3] == 1 || rng[4] == 1 || rng[5] == 1) && Problem[1] != " " && Problem[1] != "  " && Problem[1] != "   ")
        {
            //Debug.Log(Problem[1]);
            op = (string)Problem[1];
        }
        else if (snapPoint.name == "SnapPoint2_1_easy" || snapPoint.name == "SnapPoint2_2_easy" || snapPoint.name == "SnapPoint2_3_easy" || snapPoint.name == "SnapPoint2_4_easy"
            || snapPoint.name == "SnapPoint2_1_medium" || snapPoint.name == "SnapPoint2_2_medium" || snapPoint.name == "SnapPoint2_3_medium" || snapPoint.name == "SnapPoint2_4_medium"
            || snapPoint.name == "SnapPoint2_1_hard" || snapPoint.name == "SnapPoint2_2_hard" || snapPoint.name == "SnapPoint2_3_hard" || snapPoint.name == "SnapPoint2_4_hard")
        {
            op = snapValue;
        }
        else if (snapPoint2.name == "SnapPoint2_1_easy" || snapPoint2.name == "SnapPoint2_2_easy" || snapPoint2.name == "SnapPoint2_3_easy" || snapPoint2.name == "SnapPoint2_4_easy"
            || snapPoint2.name == "SnapPoint2_1_medium" || snapPoint2.name == "SnapPoint2_2_medium" || snapPoint2.name == "SnapPoint2_3_medium" || snapPoint2.name == "SnapPoint2_4_medium"
            || snapPoint2.name == "SnapPoint2_1_hard" || snapPoint2.name == "SnapPoint2_2_hard" || snapPoint2.name == "SnapPoint2_3_hard" || snapPoint2.name == "SnapPoint2_4_hard")
        {
            op = snapValue2;
        }
        else
        {
            //op = " "; //this will trigger the default case
            op = (string)Problem[1];
        }

        //get y
        if ((rng[0] == 2 || rng[1] == 2 || rng[2] == 2 || rng[3] == 2 || rng[4] == 2 || rng[5] == 2) && Problem[2] != " " && Problem[2] != "  " && Problem[2] != "   ")
        {
            //Debug.Log(Problem[2]);
            y = (int)Problem[2];
        }
        else if (snapPoint.name == "SnapPoint3_1_easy" || snapPoint.name == "SnapPoint3_2_easy" || snapPoint.name == "SnapPoint3_3_easy" || snapPoint.name == "SnapPoint3_4_easy" 
            || snapPoint.name == "SnapPoint3_1_medium" || snapPoint.name == "SnapPoint3_2_medium" || snapPoint.name == "SnapPoint3_3_medium" || snapPoint.name == "SnapPoint3_4_medium"
            || snapPoint.name == "SnapPoint3_1_hard" || snapPoint.name == "SnapPoint3_2_hard" || snapPoint.name == "SnapPoint3_3_hard" || snapPoint.name == "SnapPoint3_4_hard")
        {
            y = int.Parse(snapValue);
        }
        else if (snapPoint2.name == "SnapPoint3_1_easy" || snapPoint2.name == "SnapPoint3_2_easy" || snapPoint2.name == "SnapPoint3_3_easy" || snapPoint2.name == "SnapPoint3_4_easy"
          || snapPoint2.name == "SnapPoint3_1_medium" || snapPoint2.name == "SnapPoint3_2_medium" || snapPoint2.name == "SnapPoint3_3_medium" || snapPoint2.name == "SnapPoint3_4_medium"
          || snapPoint2.name == "SnapPoint3_1_hard" || snapPoint2.name == "SnapPoint3_2_hard" || snapPoint2.name == "SnapPoint3_3_hard" || snapPoint2.name == "SnapPoint3_4_hard")
        {
            y = int.Parse(snapValue2);
        }
        else
        {
            //y = 0; //y cannot be 0 so z will always be incorrect
            y = (int)Problem[2];
        }

        // For Debugging
        Debug.Log(x);
        Debug.Log(y);
        Debug.Log(op);

        //compare to z
        switch (op)
        {
            case "+":
                if (x != 0 && y != 0 && (int)Problem[3] == x + y)
                {
                    return true;
                }
                break;
            case "-":
                if (x != 0 && y != 0 && (int)Problem[3] == x - y)
                {
                    return true;
                }
                break;
            case "x":
                if (x != 0 && y != 0 && (int)Problem[3] == x * y)
                {
                    return true;
                }
                break;
            case "/":
                if (x != 0 && y != 0 && (int)Problem[3] == x / y) // currently this case is not called
                {
                    return true;
                }
                break;
            default:
                return false;
        }
        return false;
    }

    //for questions 3 and 4
    public bool Correct(string snapValue, Transform snapPoint, ArrayList Problem, int[] rng)
    {

        int x;
        int y;
        //int z;
        string op;
        //x op y = z

        //get x from snap or original problem
        if ((rng[0] == 0 || rng[1] == 0 || rng[2] == 0 || rng[3] == 0 || rng[4] == 0 || rng[5] == 0) && Problem[0] != " " && Problem[0] != "  " && Problem[0] != "   ")
        {
            x = (int)Problem[0];
        }
        else if (snapPoint.name == "SnapPoint1_1" || snapPoint.name == "SnapPoint1_2" || snapPoint.name == "SnapPoint1_3" || snapPoint.name == "SnapPoint1_4")
        {
            x = int.Parse(snapValue);
        }
        else
        {
            //x = 0; //x cannot be 0 so z will always be incorrect
            x = (int)Problem[0];
        }

        //get op
        if ((rng[0] == 1 || rng[1] == 1 || rng[2] == 1 || rng[3] == 1 || rng[4] == 1 || rng[5] == 1) && Problem[1] != " " && Problem[1] != "  " && Problem[1] != "   ")
        {
            op = (string)Problem[1];
        }
        else if (snapPoint.name == "SnapPoint2_1_easy" || snapPoint.name == "SnapPoint2_2_easy" || snapPoint.name == "SnapPoint2_3_easy" || snapPoint.name == "SnapPoint2_4_easy"
            || snapPoint.name == "SnapPoint2_1_medium" || snapPoint.name == "SnapPoint2_2_medium" || snapPoint.name == "SnapPoint2_3_medium" || snapPoint.name == "SnapPoint2_4_medium"
            || snapPoint.name == "SnapPoint2_1_hard" || snapPoint.name == "SnapPoint2_2_hard" || snapPoint.name == "SnapPoint2_3_hard" || snapPoint.name == "SnapPoint2_4_hard")
        {
            op = snapValue;
        }
        else
        {
            //op = " "; //this will trigger the default case
            op = (string)Problem[1];
        }

        //get y
        if ((rng[0] == 2 || rng[1] == 2 || rng[2] == 2 || rng[3] == 2 || rng[4] == 2 || rng[5] == 2) && Problem[2] != " " && Problem[2] != "  " && Problem[2] != "   ")
        {
            y = (int)Problem[2];
        }
        else if (snapPoint.name == "SnapPoint3_1_easy" || snapPoint.name == "SnapPoint3_2_easy" || snapPoint.name == "SnapPoint3_3_easy" || snapPoint.name == "SnapPoint3_4_easy"
            || snapPoint.name == "SnapPoint3_1_medium" || snapPoint.name == "SnapPoint3_2_medium" || snapPoint.name == "SnapPoint3_3_medium" || snapPoint.name == "SnapPoint3_4_medium"
            || snapPoint.name == "SnapPoint3_1_hard" || snapPoint.name == "SnapPoint3_2_hard" || snapPoint.name == "SnapPoint3_3_hard" || snapPoint.name == "SnapPoint3_4_hard")
        {
            y = int.Parse(snapValue);
        }
        // Bug is here
        // when no bubbles are in the right operand position, y always defaults to 0
        // works in every other condition
        else
        {
            //y = 0; //y cannot be 0 so z will always be incorrect
            y = (int)Problem[2];
        }

        // For Debugging
        Debug.Log(x);
        Debug.Log(y);
        Debug.Log(op);

        //compare to z
        switch (op)
        {
            case "+":
                if (x != 0 && y != 0 && (int)Problem[3] == x + y)
                {
                    return true;
                }
                break;
            case "-":
                if (x != 0 && y != 0 && (int)Problem[3] == x - y)
                {
                    return true;
                }
                break;
            case "x":
                if (x != 0 && y != 0 && (int)Problem[3] == x * y)
                {
                    return true;
                }
                break;
            case "/":
                if (x != 0 && y != 0 && (int)Problem[3] == x / y) // currently this case is not called
                {
                    return true;
                }
                break;
            default:
                return false;
        }
        return false;
    }

    public void Reset()
    {
        src.clip = chalk5;
        src.Play();
        moveable1.transform.localPosition = moveable1.GetComponentInChildren<DragableObject>().OriginalPosition;
        moveable2.transform.localPosition = moveable2.GetComponentInChildren<DragableObject>().OriginalPosition;
        moveable3.transform.localPosition = moveable3.GetComponentInChildren<DragableObject>().OriginalPosition;
        moveable4.transform.localPosition = moveable4.GetComponentInChildren<DragableObject>().OriginalPosition;
        moveable5.transform.localPosition = moveable5.GetComponentInChildren<DragableObject>().OriginalPosition;
        moveable6.transform.localPosition = moveable6.GetComponentInChildren<DragableObject>().OriginalPosition;

        foreach(GameObject snaps in gameInfo.activeSnaps)
        {
            snaps.active = false;
        }

        snaps.Reset();
        right1.SetActive(false);
        right2.SetActive(false);
        right3.SetActive(false);
        right4.SetActive(false);
        wrong1.SetActive(false);
        wrong2.SetActive(false);
        wrong3.SetActive(false);
        wrong4.SetActive(false);

        Q1.text = "";
        Q2.text = "";
        Q3.text = "";
        Q4.text = "";
        OrigTXT.text = "";

        
    }

    public void End()
    {
        gameInfo.gamemode = false;
        gameInfo.runs = 0;
        points = 0;
        score.text = "";
        score.enabled = false;
        currentTime = MaxTime;
        Debug.Log("Resetting");
    }

    public void Start()
    {
        currentTime = MaxTime;
        //gameInfo = gameObject.AddComponent<PopulateUI>();
    }


    public void NextRun()
    {
        currentTime = MaxTime;
        Reset();
        timer.enabled = true;

        if (gameInfo.runs > 10)
        {
            gameInfo.StartGameHard();
        }
        else if (gameInfo.runs > 4)
        {
            gameInfo.StartGameMedium();
        } else
        {
            gameInfo.StartGameEasy();
        }

    }

    public void ResetButtonClicked()
    {
        //move dragables
        moveable1.transform.localPosition = moveable1.GetComponentInChildren<DragableObject>().OriginalPosition;
        moveable2.transform.localPosition = moveable2.GetComponentInChildren<DragableObject>().OriginalPosition;
        moveable3.transform.localPosition = moveable3.GetComponentInChildren<DragableObject>().OriginalPosition;
        moveable4.transform.localPosition = moveable4.GetComponentInChildren<DragableObject>().OriginalPosition;
        moveable5.transform.localPosition = moveable5.GetComponentInChildren<DragableObject>().OriginalPosition;
        moveable6.transform.localPosition = moveable6.GetComponentInChildren<DragableObject>().OriginalPosition;
        //clear dragables dict
        snaps.occupiedSnapPoints.Clear();
        snaps.valueDict.Clear();

        right1.SetActive(false);
        right2.SetActive(false);
        right3.SetActive(false);
        right4.SetActive(false);
        wrong1.SetActive(false);
        wrong2.SetActive(false);
        wrong3.SetActive(false);
        wrong4.SetActive(false);
        //reset submit button
        MenuButton.SetActive(false);
        ResetButton.SetActive(false);

        src.clip = chalk4;
        src.Play();
    }

    private void Update()
    {
        if (timer.enabled)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                //Debug.Log(currentTime);
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

        timer.text = string.Format("{0:00}", seconds);
    }

    public void showOriginals()
    {
        //Debug.Log("Showing Originals");
        //ArrayList q1 = (ArrayList)OrinalProblems[0];
        //ArrayList q2 = (ArrayList)OrinalProblems[1];
        //ArrayList q3 = (ArrayList)OrinalProblems[2];
        //ArrayList q4 = (ArrayList)OrinalProblems[3];
        //Debug.Log(q1[0]);


        //string q1s = (int)q1[0] + " " + (string)q1[1] + " " + (int)q1[2] + " = " + (int)q1[3];
        //string q2s = (int)q2[0] + " " + (string)q2[1] + " " + (int)q2[2] + " = " + (int)q2[3];
        //string q3s = (int)q3[0] + " " + (string)q3[1] + " " + (int)q3[2] + " = " + (int)q3[3];
        //string q4s = (int)q4[0] + " " + (string)q4[1] + " " + (int)q4[2] + " = " + (int)q4[3];
        //Debug.Log(q1s);
        Q1.text = OriginalStrings[0];
        Q2.text = OriginalStrings[1];
        Q3.text = OriginalStrings[2];
        Q4.text = OriginalStrings[3];
        OrigTXT.text = "Original Problems";
    }
}
