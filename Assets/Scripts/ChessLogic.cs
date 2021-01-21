using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessLogic 
{
    private int x;
    private int y;
    private Chess.State s;
    public ChessLogic(int x, int y, Chess.State s)
    {
        this.x = x;
        this.y = y;
        this.s = s;
    }

    public int getX() { return x; }
    public int getY() { return y; }
    public Chess.State getState() { return s; }
    public static int XYtoIndex(int x ,int y)
    {
        return x * 19 + y;
    }
   
}
