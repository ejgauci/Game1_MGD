using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviour
{
   
    //public int points =0;
    public float coin = 0;



    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag=="Coin")
        {
            coin++;
            Destroy(col.gameObject);

        }
        
    }
    
    





}
