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

    PhotonView photonView;
    public GameObject player1;
    public GameObject player2;

    public GameObject p1Camera;
    public GameObject p2Camera;

    private int player = 0;



    // Start is called before the first frame update
    void Start()
    {

        photonView = PhotonView.Get(this);
        player = (int)PhotonNetwork.LocalPlayer.CustomProperties["Player"];


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


        if (!photonView.IsMine)
        {
            PhotonNetwork.Instantiate(this.player2.name, new Vector2(-2, 3), Quaternion.identity, 0);
            print("not mine");
        }
        else
        {
            PhotonNetwork.Instantiate(this.player1.name, new Vector2(-4, 3), Quaternion.identity, 0);
            print("mine");

        }

        StartCoroutine(removeOtherPlayerCam());

        ChangeTopNames();

        canvasManager.UpdatePointsP1(0);
        canvasManager.UpdatePointsP2(0);
        //ChangeActivePlayer();
    }

    private void ChangeTopNames()
    {
        canvasManager.ChangeTopNames(players.Find(x => x.id == Player.Id.Player1).nickname,
            players.Find(x => x.id == Player.Id.Player2).nickname);

        
    }

   

    
    IEnumerator removeOtherPlayerCam()
    {
        yield return new WaitForSeconds(0.5f);

        p1Camera = GameObject.FindWithTag("Player1Cam");
        p2Camera = GameObject.FindWithTag("Player2Cam");

        if (player ==1)
        {
            p2Camera.SetActive(false);
        }
        else
        {
            p1Camera.SetActive(false);
        }
        
    }
}
