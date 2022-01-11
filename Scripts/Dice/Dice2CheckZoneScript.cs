using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Dice2CheckZoneScript : MonoBehaviour {

	Vector3 diceVelocity;

    public GameObject dice;
    public DiceNumberTextScript dnts;

    public bool diceThrown = false;

// Update is called once per frame
    void FixedUpdate () {

        //diceVelocity = DiceScript.diceVelocity;
        dice = GameObject.FindWithTag("dice2");
        diceVelocity = dice.GetComponent<Rigidbody>().velocity;

        dnts = GameObject.FindWithTag("dnt").GetComponent<DiceNumberTextScript>();
    }

	void OnTriggerStay(Collider col)
	{
            
            if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f && diceThrown)
            {
                print("dice 2");
                switch (col.gameObject.name)
                {
                    case "Side1":
                        DiceNumberTextScript.diceNumber2 = 6;
                        break;
                    case "Side2":
                        DiceNumberTextScript.diceNumber2 = 5;
                        break;
                    case "Side3":
                        DiceNumberTextScript.diceNumber2 = 4;
                        break;
                    case "Side4":
                        DiceNumberTextScript.diceNumber2 = 3;
                        break;
                    case "Side5":
                        DiceNumberTextScript.diceNumber2 = 2;
                        break;
                    case "Side6":
                        DiceNumberTextScript.diceNumber2 = 1;
                        break;
                }
                dnts.dice2Ready = true;
                diceThrown = false;
            }
        

        
      
	}
}
