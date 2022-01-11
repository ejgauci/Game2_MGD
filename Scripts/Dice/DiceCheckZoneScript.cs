using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DiceCheckZoneScript : MonoBehaviour {

	Vector3 diceVelocity;
    public GameObject dice;
    public DiceNumberTextScript dnts;

    public bool diceThrown = false;

// Update is called once per frame
    void FixedUpdate () {
		//diceVelocity = DiceScript.diceVelocity;
        dice = GameObject.FindWithTag("dice");
        diceVelocity = dice.GetComponent<Rigidbody>().velocity;


        dnts = GameObject.FindWithTag("dnt").GetComponent<DiceNumberTextScript>();
    }

	void OnTriggerStay(Collider col)
	{
        
            if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f && diceThrown)
            {
                print("dice1");

                switch (col.gameObject.name)
                {
                    case "Side1":
                        DiceNumberTextScript.diceNumber1 = 6;
                        break;
                    case "Side2":
                        DiceNumberTextScript.diceNumber1 = 5;
                        break;
                    case "Side3":
                        DiceNumberTextScript.diceNumber1 = 4;
                        break;
                    case "Side4":
                        DiceNumberTextScript.diceNumber1 = 3;
                        break;
                    case "Side5":
                        DiceNumberTextScript.diceNumber1 = 2;
                        break;
                    case "Side6":
                        DiceNumberTextScript.diceNumber1 = 1;
                        break;
                }
                dnts.dice1Ready = true;
                diceThrown = false;

            }
        

        
      
	}
}
