using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenerateProblems : MonoBehaviour
{

    
    int diff;
    public string problem1;
    public string problem2;
    public string problem3;
    public string problem4;
    public int[] RNGList = new int[6];
    public string[] valuesList = new string[6];
    public ArrayList OriginalProblems = new ArrayList(); //list of original problems used for solution submission

    public GenerateProblems()
    {
        diff = 1;
    }

    public GenerateProblems(int difficulty)
    {
        diff = difficulty;
        
    }

    public void setDifficulty(int difficulty)
    {
        diff = difficulty;
    }

    public void Awake()
    {
        //createNewProblems(diff);
    }

    // Start is called before the first frame update
    public void createNewProblems(int diff)
    {
        ArrayList prob1 = GenerateProblem(diff);
        ArrayList prob2 = GenerateProblem(diff);
        ArrayList prob3 = GenerateProblem(diff);
        ArrayList prob4 = GenerateProblem(diff);

        OriginalProblems.Add(prob1);
        OriginalProblems.Add(prob2);
        OriginalProblems.Add(prob3);
        OriginalProblems.Add(prob4);

        //break pointer to original list
        ArrayList p1 = prob1;
        ArrayList p2 = prob2;
        ArrayList p3 = prob3;
        ArrayList p4 = prob4;

        choose6(p1, p2, p3, p4);

    }

    string returnOperator(int num)
    {
        string op;
        switch (num)
        {
            case 0:
                op = "+";
                break;
            case 1:
                op = "-";
                break;
            case 2:
                op = "*";
                break;
            case 3:
                op = "/";
                break;
            default:
                op = "error invalid operator case";
                break;
        }
        return op;
    }

    int solveZ(int x, int y, string op, int difficultymax)
    {
        //solves for a correct z, or generates a random number if eq is !=
        int z = 0;
        switch (op)
        {
            case "+":
                z = x + y;
                break;
            case "-":
                z = x - y;
                break;
            case "*":
                z = x * y;
                break;
            case "/":
                z = x / y;
                break;
            default:
                z = 0;
                break;
        }
        return z;
    }

    ArrayList GenerateProblem(int difficulty)
    {
        ArrayList problem = new ArrayList();

        int x;
        int y;
        string op;
        int rng;
        int z;

        switch (difficulty)
        {
            case 1:
                //generate x op y eq z within 10, starts at 1 so there is no * by 0 or / by 0 scenarios
                x = Random.Range(1, 10);
                y = Random.Range(1, 10);
                op = returnOperator(Random.Range(0,3));
                z = solveZ(x, y, op, 10);

                //add items to arraylist for storage
                problem.Add(x);
                problem.Add(op);
                problem.Add(y);
                problem.Add(z);
                break;
            case 2:
                //generate x op y eq z within 100
                x = Random.Range(1, 25);
                y = Random.Range(1, 25);
                op = returnOperator(Random.Range(0, 3));
                z = solveZ(x, y, op, 25);

                //add items to arraylist for storage
                problem.Add(x);
                problem.Add(op);
                problem.Add(y);
                problem.Add(z);
                break;
            case 3:
                //generate x op y eq z withinm 1000
                x = Random.Range(1, 25);
                y = Random.Range(1, 25);
                op = returnOperator(Random.Range(0, 3));
                z = solveZ(x, y, op, 25);

                //add items to arraylist for storage
                problem.Add(x);
                problem.Add(op);
                problem.Add(y);
                problem.Add(z);
                break;
            default:
                break;
        }

        return problem;
    }

    public int RandomRangeExcept(int min, int max, int except) {
        int result = except;
        while (result == except)
        {
            result = Random.Range(min, max);
        }
        return result;
    }

    void choose6(ArrayList prob1, ArrayList prob2, ArrayList prob3, ArrayList prob4)
    {
        int rng1 = Random.Range(0, 3);
        string value1 = prob1[rng1].ToString();
        prob1[rng1] = " ";

        int rng2 = RandomRangeExcept(0, 3, rng1);
        string value2 = prob1[rng2].ToString();
        prob1[rng2] = " ";

        int rng3 = Random.Range(0, 3);
        string value3 = prob2[rng3].ToString();
        prob2[rng3] = "  ";

        int rng4 = RandomRangeExcept(0, 3, rng3);
        string value4 = prob2[rng4].ToString();
        prob2[rng4] = "  ";

        int rng5 = Random.Range(0, 3);
        string value5 = prob3[rng5].ToString();
        prob3[rng5] = "   ";

        int rng6 = Random.Range(0, 3);
        string value6 = prob4[rng6].ToString();
        prob4[rng6] = "   ";

        RNGList[0] = rng1;
        RNGList[1] = rng2;
        RNGList[2] = rng3;
        RNGList[3] = rng4;
        RNGList[4] = rng5;
        RNGList[5] = rng6;

        valuesList[0] = value1;
        valuesList[1] = value2;
        valuesList[2] = value3;
        valuesList[3] = value4;
        valuesList[4] = value5;
        valuesList[5] = value6;

        problem1 = prob1[0].ToString() + "   " + prob1[1] + "   " + prob1[2].ToString() + "   =   " + prob1[3].ToString();
        problem2 = prob2[0].ToString() + "   " + prob2[1] + "   " + prob2[2].ToString() + "   =   " + prob2[3].ToString();
        problem3 = prob3[0].ToString() + "   " + prob3[1] + "   " + prob3[2].ToString() + "   =   " + prob3[3].ToString();
        problem4 = prob4[0].ToString() + "   " + prob4[1] + "   " + prob4[2].ToString() + "   =   " + prob4[3].ToString();
    }
}
