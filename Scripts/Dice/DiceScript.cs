using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour {

	static Rigidbody rb;
	public static Vector3 diceVelocity;

    public DiceCheckZoneScript dczs;
    public Dice2CheckZoneScript dczs2;

    public GameManager gm;
    public Player currentActivePlayer; //current player's turn
    

// Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody> ();
        

        if (gameObject.tag == "dice")
        {
            dczs = GameObject.FindWithTag("dcz").GetComponent<DiceCheckZoneScript>();

		}
        else
        {
            dczs2 = GameObject.FindWithTag("dcz2").GetComponent<Dice2CheckZoneScript>();
        }



        if (gm.player == gm.currentActivePlayerID)
        {
            ThrowDice();
        }
        else
        {
            rb.isKinematic = true;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		diceVelocity = rb.velocity;

		if (Input.GetKeyDown (KeyCode.Space))
        {
            
            ThrowDice();
		}
	}

    void ThrowDice()
    {
        gm.throwReady = false;

        if (gameObject.tag == "dice")
        {
            DiceNumberTextScript.diceNumber1 = 0;
            dczs.diceThrown = true;

        }
        else
        {
            DiceNumberTextScript.diceNumber2 = 0;
            dczs2.diceThrown = true;

        }

        float dirX = Random.Range(0, 500);
        float dirY = Random.Range(0, 500);
        float dirZ = Random.Range(0, 500);
        transform.position = new Vector3(0, 2, 0);
        transform.rotation = Quaternion.identity;
        rb.AddForce(transform.up * 500);
        rb.AddTorque(dirX, dirY, dirZ);
    }
}
