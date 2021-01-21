using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public Player player1;
    public Player player2;
    public Player whoseTurn;
    public int test = 0;
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, 1);
        if (rand == 0)
        {
            player1.bw = Player.BlackWhite.BLACK;
            player2.bw = Player.BlackWhite.WHITE;
            whoseTurn = player1;
        }
        else
        {
            player1.bw = Player.BlackWhite.WHITE;
            player2.bw = Player.BlackWhite.BLACK;
            whoseTurn = player2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //change to the other player's turn
    void ChangeTurn()
    {
        if (whoseTurn == player1)
        {
            whoseTurn = player2;
        }else{
            whoseTurn = player1;
        }
        
    }

    //go to the next turn
    public void NextTurn()
    {
        ChangeTurn();
        test++;
    }
}
