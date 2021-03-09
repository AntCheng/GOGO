using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   //try
    public enum BlackWhite
    {
        BLACK,
        WHITE,
    }
    public BlackWhite bw;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this is for client DB method
    public void rpcDB(int posX, int posY)
    {
        GameObject.Find("ChessManager").GetComponent<ChessManager>().chesses[ChessLogic.XYtoIndex(posX,posY)].GetComponent<Chess>().MDmethod();
    }
    
}
