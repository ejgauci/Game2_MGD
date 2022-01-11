using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SumCombinations : MonoBehaviour
{
    private int[] numb;



    // Start is called before the first frame update
    void Start()
    {
        numb = new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        //PossibleSumCombinations(numb, 12);
    }

    public void GetCombinations(int value)
    {
        PossibleSumCombinations(numb, value);
    }

    public void PossibleSumCombinations(int[] numbers, int diceValue, List<int> p = null)
    {
        if (p == null)
            p = new List<int>();

        int s = p.Sum();

        if (s == diceValue)
            Debug.Log("Total " + diceValue + " = " + String.Join(",", new List<int>(p).ConvertAll(i => i.ToString()).ToArray()));

        if (s >= diceValue)
            return;

        for (int i = 0; i < numbers.Length; i++)
        {
            int n = numbers[i];
            int[] remaining = numbers.Skip(i + 1).ToArray();
            List<int> tmpP = new List<int>(p);
            tmpP.Add(n);
            PossibleSumCombinations(remaining, diceValue, tmpP);
        }

    }
}