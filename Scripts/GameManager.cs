using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public String p1Name = "";
    public String p2Name = "";

    public int p1Points = 0;
    public int p2Points = 0;

    public int p1Lives = 3;
    public int p2Lives = 3;



    private Scene scene;

    // Start is called before the first frame update
    void Start()
    {

        photonView = PhotonView.Get(this);
        player = (int)PhotonNetwork.LocalPlayer.CustomProperties["Player"];


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

        
        //DontDestroyOnLoad(gameObject);

        //canvasManager.UpdatePointsP1(0);
        //canvasManager.UpdatePointsP2(0);
        //ChangeActivePlayer();
    }

    void Update()
    {

        if (p1Lives == 0||p2Lives==0)
        {
            GameEnded();
        }
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


    public void GameEnded()
    {
        scene = SceneManager.GetActiveScene();

        if (scene.name != "WinScene")
        {
            SceneManager.LoadScene("WinScene");
        }
        else
        {
                 if (p1Points > p2Points)
             {
                 WonPlayer(1);
             }
             else
             {
                 WonPlayer(2);
             }

        }

    }

    public void WonPlayer(int player)
    {
        print("Player "+ player+ " won");
        
    }



    public void setP1Points(int points)
    {
        p1Points = points;
    }
    public void setP2Points(int points)
    {
        p2Points = points;
    }

    public int getP1Points()
    {
        return p1Points;
    }

    public int getP2Points()
    {
        return p2Points;
    }



    public void setP1Lives(int lives)
    {
        p1Lives = lives;
    }
    public void setP2Lives(int lives)
    {
        p2Lives = lives;
    }

    public int getP1Lives()
    {
        return p1Lives;
    }

    public int getP2Lives()
    {
        return p2Lives;
    }
}
