using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.WSA;

public class GameManager : MonoBehaviourPun
{
    public List<Player> players = new List<Player>(); //going to store 2 players
    public Player currentActivePlayer; //current player's turn
    public int currentActivePlayerID;

    public BoardPiece[,] BoardMap = new BoardPiece[3, 3]; //2D Array
    public CanvasManager canvasManager;
    public NetworkManager networkManager;


    public int player = 0;

    // Start is called before the first frame update
    void Start()
    {

        Photon.Realtime.Player[] allPlayers = PhotonNetwork.PlayerList;
        player = (int)PhotonNetwork.LocalPlayer.CustomProperties["Player"];


        foreach (Photon.Realtime.Player player in allPlayers)
        {
            if (player.ActorNumber == 1)
                players.Add(new Player()
                {
                    id = Player.Id.Player1,
                    nickname = player.NickName
                });
            else if (player.ActorNumber == 2)
                players.Add(new Player() { id = Player.Id.Player2, nickname = player.NickName});
        }

        ChangeTopNames();

        ChangeActivePlayer();
    }

    private void ChangeTopNames()
    {
        canvasManager.ChangeTopNames(players.Find(x => x.id == Player.Id.Player1).nickname,
            players.Find(x => x.id == Player.Id.Player2).nickname);
    }

    public void ChangeActivePlayer()
    {
        if(currentActivePlayer == null)
        {
            currentActivePlayer = players.Find(x => x.id == Player.Id.Player1);//by default set player1 as active player
            currentActivePlayerID = 1;
        }
        else if(currentActivePlayer.id == Player.Id.Player1)
        {
            currentActivePlayer = players.Find(x => x.id == Player.Id.Player2);
            currentActivePlayerID = 2;
        }
        else if(currentActivePlayer.id == Player.Id.Player2)
        {
            currentActivePlayer = players.Find(x => x.id == Player.Id.Player1);
            currentActivePlayerID = 1;
        }

        //notify canvasmanager that player is changed
        canvasManager.ChangeBottomLabel("Player Turn:" + currentActivePlayer.nickname);
    }

    //called when the player clicks on one of the board pieces
    public void SelectBoardPiece(GameObject gameObjBoardPiece)
    {
        BoardPiece boardPiece = gameObjBoardPiece.GetComponent<BoardPiece>();


        if (player == currentActivePlayerID)
        {
            //print("i am the player");
            //if (boardPiece.GetIsShut() == false && boardPiece.canBePressed == true)
            //{
                //print("not shut and can be pressed");
                //set fruit according to current active player
                boardPiece.setShut();

                int value = boardPiece.getValue();

                //notify the canvas manager to render/update board
                canvasManager.BoardPaint(gameObjBoardPiece);
                networkManager.NotifySelectBoardPiece(gameObjBoardPiece);

            //}
            //else
            //{
            //print("kif gejt hawn?");
            //}
        }
        else
        {
                //notify the canvas manager to render/update board
                print("i am not the current player but i need to paint");
                canvasManager.BoardPaint(gameObjBoardPiece);
            
        }



           /*
            bool win = CheckWinner(boardPiece);
            if (win)
            {
                print("Detected Win by:" + currentActivePlayer.nickname);
                canvasManager.ChangeBottomLabel("Winner:" + currentActivePlayer.nickname);
            }
            else
            {
                if (IsGameDraw())
                {
                    print("Game Is Draw");
                    canvasManager.ChangeBottomLabel("Game Draw");
                }
                else
                {
                    print("Game is not draw. Continue playing...");
                    ChangeActivePlayer();
                }
            }*/
    }

   

    private bool CheckWinner(BoardPiece boardPiece)
    {
        return false;
        
    }

    private bool IsGameDraw()
    {
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }




     public List<int> numb = new List<int>{ 1, 2, 3, 4, 5, 6, 7, 8, 9 };
     public ArrayList combinations = new ArrayList();



     public List<GameObject> FlipCards;

    //public SumCombinations sumCombos;

    public int totalInThisTurn = 0;
    public int totalDiceValue = 0;
    public int diceValue;
    public int combo;

    public bool throwReady = true;

    public bool switchPlayer = false;
    public void setTotal(int totalDice)
    {

        //FlipCards
        diceValue = totalDice;

        combinations.Clear();
        for (int i = 0; i < FlipCards.Count; i++)
        {
            FlipCards[i].GetComponent<BoardPiece>().canBePressed = false;
        }

        Debug.Log("testing (2)");
        PossibleSumCombinations(numb.ToArray(), totalDice);

        foreach (List<int> combo in combinations)
        {
            foreach (int item in combo)
            {
                FlipCards[item - 1].GetComponent<BoardPiece>().canBePressed = true;
            }
        }

        StartCoroutine(checkRoundReady());

        

    }

    public int cannotBePressed = 0;
    public int score = 0;
    IEnumerator checkRoundReady(){
        yield return new WaitForSeconds(2);

        cannotBePressed = 0;

        for (int i = 0; i < FlipCards.Count; i++)
        {
            if (FlipCards[i].GetComponent<BoardPiece>().canBePressed == false)
            {
                cannotBePressed++;
            }
        }

        if (cannotBePressed == 9)
        {
            for (int i = 0; i < FlipCards.Count; i++)
            {
                if (FlipCards[i].GetComponent<BoardPiece>().isShut == false)
                {
                    score = score + FlipCards[i].GetComponent<BoardPiece>().tileValue;
                }
               
            }

            networkManager.NotifyPlayerChanged(score);
            switchPlayer = true;
            //ChangeActivePlayer();
            
        }
        

    }


    public void Test(int total)
    {
        int[] numb = new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        combinations.Clear();

        Debug.Log("testing method");
        PossibleSumCombinations(numb, total);
    }


    public void GetCombinations(int value)
    {
        int[]numb = new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        PossibleSumCombinations(numb, value);
    }

    public void PossibleSumCombinations(int[] numbers, int diceValue, List<int> p = null)
    {
        if (p == null)
            p = new List<int>();

        int s = p.Sum();

        if (s == diceValue)
        {
            
            Debug.Log("Total " + diceValue + " = " + String.Join(",", new List<int>(p).ConvertAll(i => i.ToString()).ToArray()));
            combinations.Add(p);
        }
            

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


    public bool Validation(int tileToBeFlipped)
    {

        PossibleSumCombinations(numb.ToArray(), diceValue-totalInThisTurn);
        List<int> possCombos = new List<int>();
        print("DiceValue- totalIn: "+ (diceValue -totalInThisTurn));

        foreach (List<int> item in combinations)
        {
            foreach (int number in item)
            {
                if (!possCombos.Contains(number))
                {
                    possCombos.Add(number);
                } 
            }

        }
        
        print(String.Join(",", possCombos.ConvertAll(i => i.ToString()).ToArray()));

        for (int i = 0; i < FlipCards.Count; i++)
        {
            FlipCards[i].GetComponent<BoardPiece>().canBePressed = false;
        }

        foreach (int combo in possCombos)
        {
            FlipCards[combo - 1].GetComponent<BoardPiece>().canBePressed = true;
        }

        combinations.Clear();

        if (possCombos.Contains(tileToBeFlipped))
        {
            totalInThisTurn += tileToBeFlipped;
            numb.Remove(tileToBeFlipped);
            PossibleSumCombinations(numb.ToArray(), diceValue-totalInThisTurn);

            GameObject.Find("CantFlip").GetComponent<Text>().text = "can be flipped";

            print(totalInThisTurn);
            //print("POssibleCombos list count before else if: " + possCombos.Count);



            //make not in combo canbepressed = false
            
            for(int i = 0; i < FlipCards.Count; i++)
            {
                FlipCards[i].GetComponent<BoardPiece>().canBePressed = false;
            }

            Debug.Log("testing (2)");

            foreach (List<int> combo in combinations)
            {
                foreach (int item in combo)
                {
                    FlipCards[item - 1].GetComponent<BoardPiece>().canBePressed = true;
                }
            }





            if (totalInThisTurn == diceValue)
            {
                GameObject.Find("CantFlip").GetComponent<Text>().text = "No more combinations, roll dice again";
                possCombos.Clear();
                combinations.Clear();
                totalInThisTurn = 0;
                resetTiles();
                print("POssibleCombos list count before else if: " + possCombos.Count);

                throwReady = true;

            }

            return true;
        }
        else if(possCombos.Count ==0)
        {
            Debug.Log("POssibleCombos list count: " + possCombos.Count);
            GameObject.Find("CantFlip").GetComponent<Text>().text = "no more combinations, roll dice again";
            totalInThisTurn = 0;
            resetTiles();
            Debug.Log("hello");
        }

        return false;

        
    } 

    

    public void Test2()
    {
        foreach (List<int> combination in combinations)
        {
            print(String.Join(",", combination.ConvertAll(i => i.ToString()).ToArray()));
        }
    }


    public void resetTiles()
    {
        for (int i = 0; i < FlipCards.Count; i++)
        {
            if (!FlipCards[i].GetComponent<BoardPiece>().isShut)
            {
                FlipCards[i].GetComponent<BoardPiece>().canBePressed = true;
            }
        }
    }

    public void resetCanvas()
    {
        canvasManager.ResetTiles();
    }

    public void setScore(int score)
    {
        canvasManager.setP1Score(score);
    }


}
