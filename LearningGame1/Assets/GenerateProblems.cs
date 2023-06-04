using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;

public class GenerateProblems : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        ArrayList prob1 = GenerateProblem(1);
        ArrayList prob2 = GenerateProblem(1);
        ArrayList prob3 = GenerateProblem(1);
        ArrayList prob4 = GenerateProblem(1);




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

    int solveZ(int x, int y, string op, string eq, int difficultymax)
    {
        //solves for a correct z, or generates a random number if eq is !=
        int z = 0;
        switch (op)
        {
            case "+":
                if (eq == "=")
                {
                    z = x + y;
                } else
                {
                    z = x + y;
                    z = RandomRangeExcept(difficultymax ,z);
                }
                break;
            case "-":
                break;
            case "*":
                break;
            case "/":
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

        switch (difficulty)
        {
            case 1:
                //generate x op y eq z 
                int x = Random.Range(0, 10);
                int y = Random.Range(0, 10);
                string op = returnOperator(Random.Range(0,3));
                int rng = Random.Range(0, 10);
                string eq;
                //1 in 10 chance to be !=
                if (rng > 1)
                {
                    eq = "=";
                }else
                {
                    eq = "!=";
                }
                int z = solveZ(x, y, op, eq, 10);

                //add items to arraylist for storage
                problem.Add(x);
                problem.Add(op);
                problem.Add(y);
                problem.Add(eq);
                problem.Add(z);
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }

        return problem;
    }

    int RandomRangeExcept(int max, int except) {
        int min = 0;
        int result = except;
        while (result == except)
        {
            result = Random.Range(min, max);
        }
        return result;
    }


}
