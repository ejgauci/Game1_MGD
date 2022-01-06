using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviour
{
   
    //public int points =0;
    public int coin = 0;


    public CanvasManager canvasManager;

    PhotonView photonView;
    private int player = 0;

    public string playerTag;


    void Start()
    {
        photonView = PhotonView.Get(this);
        player = (int)PhotonNetwork.LocalPlayer.CustomProperties["Player"];
    }

    void Awake()
    {
        canvasManager = GameObject.FindWithTag("Canvas").GetComponent<CanvasManager>();
        playerTag = gameObject.tag;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag=="Coin")
        {
            coin++;
            Destroy(col.gameObject);

            if (playerTag == "Player1")
            {
                canvasManager.UpdateCoinP1(coin);
                print("player 1 got a coin " + coin);
            }
            else
            {
                canvasManager.UpdateCoinP2(coin);
                print("player 2 got a coin "+ coin);
            }

        }
        
    }
    
    





}
