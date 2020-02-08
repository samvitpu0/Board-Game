using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Player[] player;
    public Dice dice;

    public GameObject[] playerTurnsUI;
    public GameObject[] playerRankUI;


    public int playerTurn = 0;

    List<int> playersWon;

    private int playerRank = 0;

    private void OnEnable()
    {
        dice.DiceRolled += HandleDiceRoll;

        for (int i = 0; i < player.Length; i++)
        {
            player[i].PlayerMoved += HandlePlayerMoved;
            player[i].PlayerWon += HandlePlayerWon;
        }

        
    }


    void Start()
    {
        playersWon = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HandleDiceRoll()
    {
        player[playerTurn].targetPosition = player[playerTurn].waypointIndex + dice.diceNumber;
        player[playerTurn].allowMovement = true;
    }

    private void HandlePlayerMoved()
    {
        playerTurnsUI[playerTurn].SetActive(false);

        playerTurn++;

        for(int i=0;i<playersWon.Count;i++)
        if (playersWon.Contains(playerTurn))
            playerTurn++;
        
          

        if(playersWon.Count == 3)
        {
            Debug.Log("Game Over");
            dice.allowRoll = false;
        }

        if (playerTurn > 3)
            playerTurn = 0;

        playerTurnsUI[playerTurn].SetActive(true);


        dice.allowRoll = true;
        Debug.Log("Player Moved");
    }

    private void HandlePlayerWon()
    {
            playersWon.Add(playerTurn);
            playerRankUI[playerRank].GetComponent<TextMeshProUGUI>().text = "#"+ (playerRank+1) + " - Player " + (playerTurn+1);
            playerRankUI[playerRank].SetActive(true);
            playerRank++;
    }

    private void OnDisable()
    {
        dice.DiceRolled -= HandleDiceRoll;

        for (int i = 0; i < player.Length; i++)
            player[i].PlayerMoved -= HandlePlayerMoved;

    }
}
