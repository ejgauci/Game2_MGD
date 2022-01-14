using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPiece : MonoBehaviour
{
    public bool isShut = false;
    public int tileValue = 0;
    public bool canBePressed = false;

    public GameManager gm;

    void Start()
    {
        //gm = GameObject.FindGameObjectWithTag("Scripts").GetComponent<GameManager>();
    }

    public void setValue(int val)
    {
        tileValue = val;
    }
    public int getValue()
    {
        return tileValue;
    }

    public void setShut()
    {
        isShut = true;
    }
    public bool GetIsShut()
    {
        return isShut;
    }

    public void setCanBePressed(bool cbp)
    {
        canBePressed = cbp;
    }

    public void Click()
    {
        gm.Validation(tileValue);
    }


}
