using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Sprite fruitApple;
    public Sprite fruitStrawberry;
    public Sprite fruitEmpty;

    public GameManager gameManager;
    public NetworkManager networkManager;

    // Start is called before the first frame update
    void Start()
    {
        AssignBoardPieceClick();

        //temp code {{just for testing - we will delete soon}}
        //GameObject one = GameObject.Find("Loc0-0");
        //GameObject two = GameObject.Find("Loc0-1");

        //one.GetComponent<BoardPiece>().SetFruit(Fruit.FruitType.Apple);
        //two.GetComponent<BoardPiece>().SetFruit(Fruit.FruitType.Strawberry);

        //BoardPaint(one);
        //BoardPaint(two);

        //ChangeBottomLabel("test");
        //ChangeTopNames("android", "pc");
        //--end of temp code




    }

    private void AssignBoardPieceClick()
    {
        for(int i = 1; i <= 9; i++) //tiles
        {
            //we are loading the boardpiece
                GameObject boardPiece = this.transform.Find("Tiles/Tile" + i).gameObject;

                //we are changing the row and colum of the component (class) BoardPiece
                boardPiece.GetComponent<BoardPiece>().tileValue = i;

                //we are making the image clickable
                EventTrigger eventTrigger = boardPiece.GetComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();

                entry.eventID = EventTriggerType.PointerClick;
                entry.callback = new EventTrigger.TriggerEvent();

                UnityEngine.Events.UnityAction<BaseEventData> callback =
                    new UnityEngine.Events.UnityAction<BaseEventData>(GameBoardPieceEventMethod);

                entry.callback.AddListener(callback);
                eventTrigger.triggers.Add(entry);

            
        }
    }


    //this method is being called automatically when we click the boardpiece
    public void GameBoardPieceEventMethod(UnityEngine.EventSystems.BaseEventData baseEvent)
    {
        PointerEventData pointerEventData = (PointerEventData)baseEvent;
        print(pointerEventData.pointerClick.gameObject.name);
        //call the SelectBoardPiece() in GameManager
        //gameManager.SelectBoardPiece(pointerEventData.pointerClick.gameObject);



        //this method is no longer in use..
        //if tile can be pressed
        if (pointerEventData.pointerClick.gameObject.GetComponent<BoardPiece>().canBePressed == true)
        {
            print("---can be pressed---to notify");
            //networkManager.NotifySelectBoardPiece(pointerEventData.pointerClick.gameObject);
        }
        
        
        

    }

    /// <summary>
    /// Draw/render the correct fruit assigned for boardpiece
    /// </summary>
    /// <param name="gameObjBoardPiece">the gameobject representing the boardpiece Ex:Loc0-0</param>
    public void BoardPaint(GameObject gameObjBoardPiece)
    {
        BoardPiece boardPiece = gameObjBoardPiece.GetComponent<BoardPiece>();

        if (gameManager.player == gameManager.currentActivePlayerID)
        {
            if (boardPiece.GetIsShut())
            {
                //print("is shut");
                gameObjBoardPiece.GetComponent<Image>().sprite = fruitApple;
                boardPiece.setCanBePressed(false);
            }
            else
            {
                //print("is not shut");
                gameObjBoardPiece.GetComponent<Image>().sprite = fruitEmpty;
            }
        }
        else
        {
            gameObjBoardPiece.GetComponent<Image>().sprite = fruitApple;
        }

            
            
            
    }

    public void ResetTiles()
    {
        for (int i = 1; i <= 9; i++)
        {
            GameObject boardPiece = this.transform.Find("Tiles/Tile" + i).gameObject;
            boardPiece.GetComponent<Image>().sprite = fruitEmpty;

        }
    }
    
    public void ChangeBottomLabel(string message)
    {
        transform.Find("PanelBottom/LblMessage").GetComponent<TextMeshProUGUI>().text = message;
    }

    public void ChangeTopNames(string player1Name, string player2Name)
    {
        transform.Find("PanelTop/Player1Label").GetComponent<TextMeshProUGUI>().text = player1Name;
        transform.Find("PanelTop/Player2Label").GetComponent<TextMeshProUGUI>().text = player2Name;
    }

    public void setP1Score(int score)
    {
        transform.Find("PanelTop/Player1Score").GetComponent<TextMeshProUGUI>().text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
