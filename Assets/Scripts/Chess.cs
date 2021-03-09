using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chess : NetworkBehaviour //network behaviour
{
    private ChessManager ChessManager;
    public int x;
    public int y;

    public enum State
    {
        Unable,
        Black,
        White,
    }
    private State currentState;
    // Start is called before the first frame update
    void Start()
    {
        InitialState();
        ChessManager = GameObject.Find("ChessManager").GetComponent<ChessManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (isClientOnly)
        {
            if (ChessManager.turnManager.whoseTurn.bw == Player.BlackWhite.WHITE)
            {
               MDmethod();
               //serveronly MDmethod()
               //player.callMD(pox.x,pos.y)
               //GameObject.getComponent<player>().
            }
        }
        else
        {
            if(ChessManager.turnManager.whoseTurn.bw == Player.BlackWhite.BLACK)
            {
                MDmethod();
               
            }
        }
        

    }

    
    public void MDmethod()
    {
        //GetComponent<UnityEngine.UI.Image>().color;
        if (ChessManager.endGameClean)
        {
            setUnable();
            ChessManager.chessLogicManager.RemoveChess(x, y);
            return;
        }
        if (!ChessManager.chessCanPlay)
        {
            return;
        }
        Player.BlackWhite curruentPlay = ChessManager.turnManager.whoseTurn.bw;

        if (ChessManager.chessLogicManager.IsValidPlay(x, y, curruentPlay)) //the chess would be played in this step
        {
            if (ChessManager.turnManager.whoseTurn.bw == Player.BlackWhite.BLACK)
            {
                currentState = State.Black;
                GetComponentInChildren<UnityEngine.UI.Image>().color = new UnityEngine.Color(0, 0, 0);
            }
            else
            {
                currentState = State.White;
                GetComponentInChildren<UnityEngine.UI.Image>().color = new UnityEngine.Color(255, 255, 255);
            }

            gameObject.GetComponentInChildren<Canvas>().enabled = true;
            //ChessManager.newChessPlay(new ChessLogic(x, y, currentState));
            ChessManager.turnManager.NextTurn();
        }
        
    }

   

    public void InitialState()
    {
        gameObject.GetComponentInChildren<Canvas>().enabled = false;
        currentState = State.Unable;
    }

    public void setUnable()
    {
        gameObject.GetComponentInChildren<Canvas>().enabled = false;
    }
}
