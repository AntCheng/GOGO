using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessLogicManager
{

    private List<ChessLogic> chessLogics;
    private Stack<Dictionary<ChessLogic,List<ChessLogic>>> chessStack;
    private ChessManager chessManager;

    public ChessLogicManager()
    {
        Initial();
        //chessManager = ChessManager.Instance;
        
    }

    //class initialzetion
    public void Initial()
    {
        chessLogics = new List<ChessLogic>();
        for(int i = 0; i < 19; i++)
        {
            for (int j = 0; j < 19; j++)
            {
                chessLogics.Add(new ChessLogic(i, j, Chess.State.Unable));
            }
        }
        chessStack = new Stack<Dictionary<ChessLogic, List<ChessLogic>>>();
    }

    //put the new chess in the chess list
    public void NewChess(ChessLogic cl)
    {
        int index = ChessLogic.XYtoIndex(cl.getX(),cl.getY());
        chessLogics.RemoveAt(index);
        chessLogics.Insert(index, cl);
       
        //UpdateLogic();
    }

    public void RemoveChess(int x, int y)
    {
        int index = ChessLogic.XYtoIndex(x, y);
        chessLogics.RemoveAt(index);
        chessLogics.Insert(index, new ChessLogic(x, y, Chess.State.Unable));
    }

    public void RegretChess()
    {
        //chessStack.
    }
    //return the chess on top of the given chess
    private ChessLogic TopChess(int x, int y)
    {
        if (y == 0)
        {
            return null;
        }
        int index = ChessLogic.XYtoIndex(x, y-1);
        return chessLogics[index];
    }

    //return the chess on left of the given chess, return null if not exist
    private ChessLogic LeftChess(int x, int y)
    {
        if (x == 0)
        {
            return null;
        }
        int index = ChessLogic.XYtoIndex(x - 1, y);
        return chessLogics[index];
    }
    //return the chess on right of the given chess
    private ChessLogic RightChess(int x, int y)
    {
        if (x == 18)
        {
            return null;
        }
        int index = ChessLogic.XYtoIndex(x+1, y);
        return chessLogics[index];
    }
    //return the chess on bottom of the given chess
    private ChessLogic DownChess(int x, int y)
    {
        if (y == 18)
        {
            return null;
        }
        int index = ChessLogic.XYtoIndex(x, y+1);
        return chessLogics[index];
    }
    //check if the chess in the position is alive
    private bool IsAlive(int x, int y)
    {
        return true;
    }

    //remove the block of chesses which are dead
    void RemoveDeathBlock(List<ChessLogic> clb)
    {
        foreach(ChessLogic cl in clb)
        {
            RemoveChess(cl.getX(), cl.getY());
            setChessUnable(cl.getX(), cl.getY());
        }
    }

    //Call when a player play a step
    void UpdateLogic()
    {

    }

    //check if the play is valid
    public bool IsValidPlay(int x, int y, Player.BlackWhite cp)
    {
        int index = ChessLogic.XYtoIndex(x, y);
        if (chessLogics[index].getState() != Chess.State.Unable)
        {
            return false;
        }
        if (!CheckValidIfPlayed(x,y,cp))
        {
            return false;
        }
        return true;
    }

    //check if this is jie
    bool IsJie()
    {
        return false;
    }

    //get qi of this chess
    // -1 means there is no chess
    // 0 means it is dead
    //  >0 means it is still alive
    public int getQi(int x, int y)
    {
        int index = ChessLogic.XYtoIndex(x, y);
        Chess.State s = chessLogics[index].getState();
        List<ChessLogic> visited = new List<ChessLogic>();
        if(s == Chess.State.Unable)
        {
            return -1;
        }
        int qi = getQiRecur(TopChess(x,y), s, visited)+
                 getQiRecur(LeftChess(x, y), s, visited)+
                 getQiRecur(RightChess(x, y), s, visited)+
                 getQiRecur(DownChess(x, y), s, visited);

        return qi;
    }

    //helper recursive function to get qi of chess
    public int getQiRecur(ChessLogic cl, Chess.State s, List<ChessLogic> visited)
    {
        if (cl==null)
        {
            return 0;
        }
        // if visited before, return 0
        if (visited.Contains(cl))
        {
            return 0;
        }
        //if no chess there, return 1
        if (cl.getState() == Chess.State.Unable)
        {
            visited.Add(cl);
            return 1;
        }
        //if the chess are different, return 0
        if(cl.getState() != s)
        {
            visited.Add(cl);
            return 0;
        }
        //run recursively to find the qi
        visited.Add(cl);
        int qi = getQiRecur(TopChess(cl.getX(), cl.getY()), s, visited) +
                 getQiRecur(LeftChess(cl.getX(), cl.getY()), s, visited) +
                 getQiRecur(RightChess(cl.getX(), cl.getY()), s, visited) +
                 getQiRecur(DownChess(cl.getX(), cl.getY()), s, visited);

        return qi;
    }

    //check nearby enemy chesss to see if they are death ( run getQi() on them), if so, kill them and return true
    //if not, run getQi() on the given position, if it is 0 qi, return false, otherwise return true
    //NOTE that in this implementation, if this is valid, then the chess would have already been play after this function call
    public bool CheckValidIfPlayed(int x, int y , Player.BlackWhite cp)
    {
        Chess.State playState;
        bool result =false;
        Dictionary<ChessLogic, List<ChessLogic>> dict = new Dictionary<ChessLogic, List<ChessLogic>>();
        ChessLogic newChess;
        //NOTE that if this is invalid , I need to remove that chessLogic form the chessLogic List
        if (cp == Player.BlackWhite.BLACK)
        {
            playState = Chess.State.Black;
            newChess = new ChessLogic(x, y, Chess.State.Black);
            NewChess(newChess);
            dict[newChess] = new List<ChessLogic>();
            //chessLogics.Insert(ChessLogic.XYtoIndex(x, y), new ChessLogic(x, y, Chess.State.Black));
        }
        else
        {
            playState = Chess.State.White;
            newChess = new ChessLogic(x, y, Chess.State.White);
            NewChess(newChess);
            dict[newChess] = new List<ChessLogic>();
            //chessLogics.Insert(ChessLogic.XYtoIndex(x, y), new ChessLogic(x, y, Chess.State.White));
        }


        //check for top 
        ChessLogic topChess = TopChess(x, y);
        if (topChess != null)
        {
            if (topChess.getState() != playState)
            {
                if (getQi(topChess.getX(), topChess.getY()) == 0)
                {
                    List<ChessLogic> clList = FindBlock(topChess.getX(), topChess.getY());
                    foreach(ChessLogic cl in clList)
                    {
                        //TODO fot future implementation of regret
                    }
                    RemoveDeathBlock(FindBlock(topChess.getX(), topChess.getY()));//
                    result = true;
                    //return true;
                }
            }
        }

        //check for left
        ChessLogic leftChess = LeftChess(x, y);
        if (leftChess != null)
        {
            if (leftChess.getState() != playState)
            {
                if (getQi(leftChess.getX(), leftChess.getY()) == 0)
                {
                    RemoveDeathBlock(FindBlock(leftChess.getX(), leftChess.getY()));//
                    result = true;
                    //return true;
                }
            }
        }

        //check for right
        ChessLogic rightChess = RightChess(x, y);
        if (rightChess != null)
        {
            if (rightChess.getState() != playState)
            {
                if (getQi(rightChess.getX(), rightChess.getY()) == 0)
                {
                    RemoveDeathBlock(FindBlock(rightChess.getX(), rightChess.getY()));//
                    result = true;
                    //return true;
                }
            }
        }

        //check for down
        ChessLogic downChess = DownChess(x, y);
        if (downChess != null)
        {
            if (downChess.getState() != playState)
            {
                if (getQi(downChess.getX(), downChess.getY()) == 0)
                {
                    RemoveDeathBlock(FindBlock(downChess.getX(), downChess.getY()));//
                    result = true;
                    //return true;
                }
            }
        }

        //check for itself,techincally could use !=0 because it is impossible to return negative number
        if (getQi(x, y) > 0)
        {
            result = true;
            //return true;
        }

        //This is an invalid play so we remove that chess and return false;
        if (!result)
        {
            RemoveChess(x, y);
        }


        return result;
        
    }
    
    //find blocks of chess that are together
    public List<ChessLogic> FindBlock(int x, int y)
    {
        List<ChessLogic> clList = new List<ChessLogic>();
        List<ChessLogic> visited = new List<ChessLogic>();
        int index = ChessLogic.XYtoIndex(x, y);
        ChessLogic cl = chessLogics[index];
        clList.Add(cl);
        visited.Add(cl);

        FindBlock_helper(TopChess(x, y), cl.getState(), visited, clList);
        FindBlock_helper(LeftChess(x, y), cl.getState(), visited, clList);
        FindBlock_helper(RightChess(x, y), cl.getState(), visited, clList);
        FindBlock_helper(DownChess(x, y), cl.getState(), visited, clList);
        Debug.Log(clList.Count);
        return clList;
    }

    public void FindBlock_helper(ChessLogic cl, Chess.State s, List<ChessLogic> visited, List<ChessLogic> clList)
    {
        if (cl == null)
        {
            return;
        }
        // if visited before, return 0
        if (visited.Contains(cl))
        {
            return;
        }
        if (cl.getState() != s)
        {
            visited.Add(cl);
            return;
        }
        visited.Add(cl);
        clList.Add(cl);
        FindBlock_helper(TopChess(cl.getX(), cl.getY()), s, visited, clList);
        FindBlock_helper(LeftChess(cl.getX(), cl.getY()), s, visited, clList);
        FindBlock_helper(RightChess(cl.getX(), cl.getY()), s, visited, clList);
        FindBlock_helper(DownChess(cl.getX(), cl.getY()), s, visited, clList);

    }

    //make the chess become unable again
    public void setChessUnable(int x,int y)
    {
        ChessManager.Instance.chesses[ChessLogic.XYtoIndex(x, y)].GetComponent<Chess>().setUnable();
    }


    //call when both players choose which chesses are death and removed
    //this method calculate how much black has gain, and use it to determine which player wind the game
    public int WhoWin()
    {
        int blackGain = 0;
        for(int i =0; i < chessLogics.Count; i++)
        {
            if (chessLogics[i].getState() == Chess.State.Black)
            {
                blackGain++;
            }else if(chessLogics[i].getState() == Chess.State.Unable)
            {
                //check if this chess is surround by black chess
                if (SurroundByBlack(chessLogics[i]))
                {
                    blackGain++;
                }
            }
        }
        return blackGain;
    }

    //check if a chess is surround by black chess
    public bool SurroundByBlack(ChessLogic cl)
    {
       
        
        //check for the top
        ChessLogic topChess = TopChess(cl.getX(), cl.getY());
        
        while (topChess!= null && topChess.getState() == Chess.State.Unable)
        {
            topChess = TopChess(topChess.getX(),topChess.getY());
        }
        
        if(topChess !=null && topChess.getState() == Chess.State.White)
        {
            return false;
        }
        
        //check for the left
        ChessLogic leftChess = LeftChess(cl.getX(), cl.getY());
        while (leftChess != null && leftChess.getState() == Chess.State.Unable)
        {
            leftChess = LeftChess(leftChess.getX(), leftChess.getY());
        }
        if (leftChess != null && leftChess.getState() == Chess.State.White)
        {
            return false;
        }

        //check for the right
        ChessLogic rightChess = RightChess(cl.getX(), cl.getY());
        while (rightChess != null && rightChess.getState() == Chess.State.Unable)
        {
            rightChess = RightChess(rightChess.getX(), rightChess.getY());
        }
        if (rightChess != null && rightChess.getState() == Chess.State.White)
        {
            return false;
        }

        //check for the down
        ChessLogic downChess = DownChess(cl.getX(), cl.getY());
        while (downChess != null && downChess.getState() == Chess.State.Unable)
        {
            downChess = DownChess(downChess.getX(), downChess.getY());
        }
        if (downChess != null && downChess.getState() == Chess.State.White)
        {
            return false;
        }
        

        return true;
    }
}
