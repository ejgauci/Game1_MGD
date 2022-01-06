using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    public int seconds = 20;
    public GameManager gameManager;
    




    public void ChangeTopNames(string player1Name, string player2Name)
    {
        transform.Find("Player1Label").GetComponent<TextMeshProUGUI>().text = player1Name;
        transform.Find("Player2Label").GetComponent<TextMeshProUGUI>().text = player2Name;
    }

    public void UpdatePointsP1(int pointsP1)
    {
        transform.Find("Player1Points").GetComponent<TextMeshProUGUI>().text = pointsP1.ToString();
    }
    public void UpdatePointsP2(int pointsP2)
    {
        transform.Find("Player2Points").GetComponent<TextMeshProUGUI>().text = pointsP2.ToString();
    }

    
    void Start()
    {
        StartCoroutine(CountdownToStart());
    }


    IEnumerator CountdownToStart()
    {
        while (seconds > 0)
        {
            transform.Find("CountdownTimer").GetComponent<TextMeshProUGUI>().text = formatTime(seconds);
            yield return new WaitForSeconds(1f);
            seconds--;
        }

        transform.Find("CountdownTimer").GetComponent<TextMeshProUGUI>().text = "GAME OVER";
    }

    static string formatTime(int secs)
    {
        int mins = (secs % 3600) / 60;
        secs = secs % 60;
        return string.Format("{0:D2}:{1:D2}",mins, secs);
    }


}
