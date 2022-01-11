using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
}
