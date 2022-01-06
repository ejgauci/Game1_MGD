using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    

    public GameManager gameManager;
    

    public void ChangeTopNames(string player1Name, string player2Name)
    {
        transform.Find("Player1Label").GetComponent<TextMeshProUGUI>().text = player1Name;
        transform.Find("Player2Label").GetComponent<TextMeshProUGUI>().text = player2Name;
    }

    public void UpdateCoinP1(int coinsP1)
    {
        transform.Find("Player1Coins").GetComponent<TextMeshProUGUI>().text = coinsP1.ToString();
    }
    public void UpdateCoinP2(int coinsP1)
    {
        transform.Find("Player2Coins").GetComponent<TextMeshProUGUI>().text = coinsP1.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
