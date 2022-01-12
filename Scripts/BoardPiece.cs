using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPiece : MonoBehaviour
{
    public bool isShut = false;
    public int tileValue = 0;
    public bool canBePressed = true;


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
}
