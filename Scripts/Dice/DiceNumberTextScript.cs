using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceNumberTextScript : MonoBehaviour {

	Text text;
	public static int diceNumber1;
    public static int diceNumber2;
    public static int total;


    public int dice1;
    public int dice2;

    public bool dice1Ready = false;
    public bool dice2Ready = false;
    
    public GameManager gameManager;


    // Use this for initialization
    void Start () {
		text = GetComponent<Text> ();
	}
	

	// Update is called once per frame
	void Update ()
    {
        if (dice1Ready && dice2Ready)
        {

            dice1 = diceNumber1;
            dice2 = diceNumber2;
            total = diceNumber1 + diceNumber2;
            
            text.text = total.ToString();

            dice1Ready = false;
            dice2Ready = false;

            gameManager.setTotal(total);
            //gameManager.Test(total);

        }
        
	}
}
