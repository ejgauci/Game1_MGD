using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
{
    public List<Player> players = new List<Player>(); //going to store 2 players
    //public Player currentActivePlayer; //current player's turn

   /* public BoardPiece[,] BoardMap = new BoardPiece[3, 3]; //2D Array */
    public CanvasManager canvasManager;

    // Start is called before the first frame update
    void Start()
    {
        //--temp code
        //players.Add(new Player() { id = Player.Id.Player1, nickname = "P1", assignedFruit = Fruit.FruitType.Apple });
        //players.Add(new Player() { id = Player.Id.Player2, nickname = "P2", assignedFruit = Fruit.FruitType.Strawberry });
        //-end temp code

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

        //ChangeActivePlayer();
    }

    private void ChangeTopNames()
    {
        canvasManager.ChangeTopNames(players.Find(x => x.id == Player.Id.Player1).nickname,
            players.Find(x => x.id == Player.Id.Player2).nickname);
    }

    /*public void ChangeActivePlayer()
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

    
    //called when the player clicks on one of the board pieces ex:Loc0-0
    public void SelectBoardPiece(GameObject gameObjBoardPiece)
    {
        BoardPiece boardPiece = gameObjBoardPiece.GetComponent<BoardPiece>();

        if(boardPiece.GetFruit() == Fruit.FruitType.None)
        {
            //set fruit according to current active player
            boardPiece.SetFruit(currentActivePlayer.assignedFruit);

            //update boardMap
            BoardMap[boardPiece.row, boardPiece.column] = boardPiece;

            //notify the canvas manager to render/update board
            canvasManager.BoardPaint(gameObjBoardPiece);

            //change active player
            //ChangeActivePlayer();

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
            }

        }
    }
     
    
    private bool CheckWinner(BoardPiece boardPiece)
    {
        //check row
        int rowCounter = 0;
        for(int i=0; i < 3; i++)
        {
            BoardPiece tmpBoardPiece = BoardMap[boardPiece.row, i];
            if(tmpBoardPiece != null)
            {
                if (tmpBoardPiece.GetFruit() == boardPiece.GetFruit())
                    rowCounter += 1;
            }
   
        }

        if(rowCounter == 3)
        {
            print("similar in row");
            return true;
        }

        //check column
        int colCounter = 0;
        for(int i = 0; i < 3; i++)
        {
            BoardPiece tmpBoardPiece = BoardMap[i, boardPiece.column];
            //Loc0-0
            //BoardMap[2,0]
            if (tmpBoardPiece != null)
            {
                if (tmpBoardPiece.GetFruit() == boardPiece.GetFruit())
                    colCounter += 1;
            }
  
        }

        if(colCounter == 3)
        {
            print("similar in column");
            return true;
        }

        //check diagonal 1
        int diagOneCounter = 0;
        int diagCol1 = -1;
        for (int i = 0; i < 3; i++)
        {
            diagCol1 += 1;
            BoardPiece tmpBoardPiece = BoardMap[i, diagCol1];
            if (tmpBoardPiece != null)
            {
                if (tmpBoardPiece.GetFruit() == boardPiece.GetFruit())
                    diagOneCounter += 1;
            }
        }

        if(diagOneCounter == 3)
        {
            print("similar in diagonal 1");
            return true;
        }

        //check diagonal 2
        int diagTwoCounter = 0;
        int diagCol2 = 3;
        for(int i = 0; i < 3; i++)
        {
            diagCol2 -= 1;
            BoardPiece tmpBoardPiece = BoardMap[i, diagCol2];
            if(tmpBoardPiece != null)
            {
                if (tmpBoardPiece.GetFruit() == boardPiece.GetFruit())
                    diagTwoCounter += 1;
            }
        }

        if(diagTwoCounter == 3)
        {
            print("Similar in diagonal 2");
            return true;
        }

        return false;


    }

    
    private bool IsGameDraw()
    {
        foreach(BoardPiece boardPiece in BoardMap)
        {
            if (boardPiece == null)
                return false;
        }

        return true;
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
