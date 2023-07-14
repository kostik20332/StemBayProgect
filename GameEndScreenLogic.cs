using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameEndScreenLogic : MonoBehaviour
{
    private GameObject player;
    private GameObject canvas;

    public TMP_Text playerScoreText;

    private Playerlogic scriptOnPlayer;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameEnded");
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        this.transform.SetParent(canvas.transform, false);
        scriptOnPlayer = player.GetComponent<Playerlogic>();
        playerScoreText.text = player.name + " win with score " + scriptOnPlayer.moneyAmount;
    }
}
