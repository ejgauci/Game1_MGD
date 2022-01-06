using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviour
{
    

    public int points = 0;
    public CanvasManager canvasManager;

    PhotonView photonView;
    private int player = 0;

    public string playerTag;

    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;

    private Vector3 SpawnPoint1 = new Vector3(50.75f, 1.5f, 0);
    


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();

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
            points++;
            Destroy(col.gameObject);

            if (playerTag == "Player1")
            {
                canvasManager.UpdatePointsP1(points);
                print("player 1 got a coin " + points);
            }
            else
            {
                canvasManager.UpdatePointsP2(points);
                print("player 2 got a coin "+ points);
            }

        }

        if (col.transform.tag == "DangerZone")
        {

            StartCoroutine(DangerZone());
        }


    }

    IEnumerator DangerZone()
    {
        spriteRenderer.enabled = false;
        playerMovement.enabled = false;

        transform.position = SpawnPoint1;

        yield return new WaitForSeconds(2);

        spriteRenderer.enabled = true;
        playerMovement.enabled = true;
    }







}
