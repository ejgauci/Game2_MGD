using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{

    public GameObject cheatPanel;
    public float startTimer;
    public bool isShown;


    void Start()
    {
        cheatPanel = GameObject.FindGameObjectWithTag("CheatPanel");
        cheatPanel.SetActive(false);
        isShown = false;
    }

    // Update is called once per frame
    void Update()
    {
        OpenPanel();
        ClosePanel();
        
       
    }

    public void OpenPanel()
    {

        if (!isShown)
        {
            //print("hafna ");
            if (Input.GetMouseButtonDown(1))
            {
                startTimer += Time.time;

            }
            else if (Input.GetMouseButton(1))
            {

                print(Time.time - startTimer);
                if (Time.time - startTimer >= 3f)
                {
                    cheatPanel.SetActive(true);
                    isShown = true;

                }
                // panelActive = true;
            }
            else
            {
                startTimer = 0;
            }
        }


    }

    public void ClosePanel()
    {
        if (isShown)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                startTimer += Time.time;

            }
            else if (Input.GetKey(KeyCode.A))
            {
                print(Time.time - startTimer);
                if (Time.time - startTimer >= 3f)
                {
                    cheatPanel.SetActive(false);
                    isShown = false;
                }
                // panelActive = true;
            }
            else
            {
                startTimer = 0;
            }
        }
    }
}
