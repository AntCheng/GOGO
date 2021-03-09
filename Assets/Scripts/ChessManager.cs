using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessManager : MonoBehaviour
{
    public GameObject[] chesses;
    private Queue<ChessLogic> chessRecord = new Queue<ChessLogic>();
    public TurnManager turnManager;
    public ChessLogicManager chessLogicManager = new ChessLogicManager();
    //public Player player;

    public bool endGameClean =false;
    public bool chessCanPlay = true;

    private float topleftX = -5.6f;
    private float topleftY = 5.6f;
    private float interval = 0.6222f;

    public static ChessManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //player = NetworkClient.connection.identity.GetComponent<PlayerManager>();
        float z = chesses[0].transform.position.z;
        Quaternion rot = chesses[0].transform.rotation;
        //testChess.transform.SetPositionAndRotation(new Vector3(0, 0, z), rot);
        //Instantiate(testChess);
        for (int i =0; i< 19; i++)
        {
            for(int j = 0; j < 19; j++)
            {
                Vector3 newPos = new Vector3(topleftX + interval * i, topleftY - interval * j, z);

                chesses[i*19+j].transform.SetPositionAndRotation(newPos, rot);
                
                chesses[i*19+j]=Instantiate(chesses[i*19+j]);
                chesses[i * 19 + j].GetComponent<Chess>().x = i;
                chesses[i * 19 + j].GetComponent<Chess>().y = j;

            }
        }
    }

    public void Initial()
    {
        for (int i = 0; i < 19; i++)
        {
            for (int j = 0; j < 19; j++)
            {
                chesses[i * 19 + j].GetComponent<Chess>().setUnable();
            }
        }
        chessCanPlay = true;
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newChessPlay(ChessLogic cl)
    {
        chessRecord.Enqueue(cl);
        //chessLogicManager.NewChess(cl);
    }


    
}
