using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalManager : MonoBehaviour
{
    public GameObject countingBoard;
    public GameObject endGameBoard;
    public Option Option;
    public ChessManager chessManager;
    public TurnManager turnManager;
    public Button confirmbutton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //call when the surrender button is clicked
    public void EndGameWithSur()
    {
        string whowin;
        if (chessManager.turnManager.whoseTurn.bw == Player.BlackWhite.BLACK)
        {
            whowin = "White";
        }
        else
        {
            whowin = "Black";
        }
        CountingBoard.MakeCountingBoardSur(whowin);
        chessManager.chessCanPlay = false; //surrender button -> 
    }

    
    public void EndGameWithCal()
    {
        //add the process of removing deadth chess TODO
        int blackGain = chessManager.chessLogicManager.WhoWin();
        CountingBoard.MakeCountingBoardCal(blackGain);
    }

    //This would be call by end game button
    public  void ShowEndGameBoard()
    {
        GameObject.Instantiate(endGameBoard);
        chessManager.chessCanPlay = false; //end game button

    }
    public void StartEndGameClean()
    {
        chessManager.endGameClean = true;
    }

    public void FinishEndGameClean()
    {
        chessManager.endGameClean = false;
    }

    //This is call when the game restart 
    // (call when in counting board player agree to play again)
    public void AllInitial()
    {
        chessManager.Initial();
        chessManager.chessLogicManager.Initial();
        Option.GiveListener();
        turnManager.Initial();
    }
   
}
