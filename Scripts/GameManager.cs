using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
{
    public List<Player> players = new List<Player>(); //going to store 2 players
    public Player currentActivePlayer; //current player's turn

    public BoardPiece[,] BoardMap = new BoardPiece[3, 3]; //2D Array
    public CanvasManager canvasManager;

    // Start is called before the first frame update
    void Start()
    {

        Photon.Realtime.Player[] allPlayers = PhotonNetwork.PlayerList;
        foreach(Photon.Realtime.Player player in allPlayers)
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
        }
        else if(currentActivePlayer.id == Player.Id.Player1)
        {
            currentActivePlayer = players.Find(x => x.id == Player.Id.Player2);
        }
        else if(currentActivePlayer.id == Player.Id.Player2)
        {
            currentActivePlayer = players.Find(x => x.id == Player.Id.Player1);
        }

        //notify canvasmanager that player is changed
        canvasManager.ChangeBottomLabel("Player Turn:" + currentActivePlayer.nickname);
    }

    //called when the player clicks on one of the board pieces
    public void SelectBoardPiece(GameObject gameObjBoardPiece)
    {
        BoardPiece boardPiece = gameObjBoardPiece.GetComponent<BoardPiece>();

        if(boardPiece.GetIsShut() == false)
        {
            //set fruit according to current active player
            boardPiece.setShut();

            int value = boardPiece.getValue();

            //notify the canvas manager to render/update board
            canvasManager.BoardPaint(gameObjBoardPiece);

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


    /*public bool Validation(int tileToBeFlipped)
    {

        PossibleSumCombinations(numb.ToArray(), totalInThisTurn);
        return true;
    } */





    public void Test2()
    {
        foreach (List<int> combination in combinations)
        {
            print(String.Join(",", combination.ConvertAll(i => i.ToString()).ToArray()));
        }
    }

    public void whatCanBeFlipped(int total)
    {
        PossibleSumCombinations(numb.ToArray(), total);
        List<int> possibleCombo = new List<int>();

        foreach (List<int> combo in combinations)
        {
            foreach (int number in combo)
            {
                if (!possibleCombo.Contains(number))
                {
                    possibleCombo.Add(number);
                }
            } 
        }

        //print(String.Join(",", possibleCombo.ConvertAll(i => i.ToString()).ToArray()));

        for (int i = 0; i < FlipCards.Count; i++)
        {
            FlipCards[i].GetComponent<BoardPiece>().canBePressed = false;
        }

        foreach (int combo in possibleCombo)
        {
            FlipCards[combo - 1].GetComponent<BoardPiece>().canBePressed = true;
        }
        combinations.Clear();
        if (possibleCombo.Count == 0)
        {
            resetTiles();
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
}
