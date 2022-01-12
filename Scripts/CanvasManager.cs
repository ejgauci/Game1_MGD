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
        gameManager.setP1Points(pointsP1);
    }
    public void UpdatePointsP2(int pointsP2)
    {
        transform.Find("Player2Points").GetComponent<TextMeshProUGUI>().text = pointsP2.ToString();
        gameManager.setP1Points(pointsP2);
    }


    public void UpdateLivesP1(int livesP1)
    {
        transform.Find("Player1Lives").GetComponent<TextMeshProUGUI>().text = livesP1.ToString();
        gameManager.setP1Lives(livesP1);
    }
    public void UpdateLivesP2(int livesP2)
    {
        transform.Find("Player2Lives").GetComponent<TextMeshProUGUI>().text = livesP2.ToString();
        gameManager.setP2Lives(livesP2);
    }


    void Start()
    {
        StartCoroutine(CountdownToStart());
    }


    void Update()
    {

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
        gameManager.GameEnded();
    }

    static string formatTime(int secs)
    {
        int mins = (secs % 3600) / 60;
        secs = secs % 60;
        return string.Format("{0:D2}:{1:D2}",mins, secs);
    }


}
