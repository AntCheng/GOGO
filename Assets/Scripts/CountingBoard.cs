using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountingBoard : MonoBehaviour
{
    public Text message;
    public Button yesButton;
    public Button noButton;
    // Start is called before the first frame update
    void Start()
    {
        yesButton.onClick.AddListener(yesEvent);
        noButton.onClick.AddListener(yesEvent);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void yesEvent()
    {
        GameObject.Find("GlobalManager").GetComponent<GlobalManager>().AllInitial();
        Destroy(gameObject);
    }
    
    //static method to make and show the countingboard
    public static void MakeCountingBoardCal(int blcakgain)
    {
        string whowin;
        if(blcakgain> 189)
        {
            whowin = "Black";
        }
        else
        {
            whowin = "White";
        }
        GameObject newCoungtingBoard = GameObject.Instantiate(GameObject.Find("GlobalManager").GetComponent<GlobalManager>().countingBoard
            , new Vector3(0,0,0), Quaternion.identity);
        CountingBoard cb = newCoungtingBoard.GetComponent<CountingBoard>();
        // Change the amount text to reflect the amount of damage dealt
        cb.message.text = "-" + whowin + " Win" + "\n"+ "Black : "+ blcakgain.ToString() + "\n" + "White : " + (19*19-blcakgain).ToString();
        // start a coroutine to fade away and delete this effect after a certain time
        
    }

    //static method to make and show the countingboard
    public static void MakeCountingBoardSur(string whowin)
    {
        
        GameObject newCoungtingBoard = GameObject.Instantiate(GameObject.Find("GlobalManager").GetComponent<GlobalManager>().countingBoard
            , new Vector3(0, 0, 0), Quaternion.identity);
        CountingBoard cb = newCoungtingBoard.GetComponent<CountingBoard>();
        // Change the amount text to reflect the amount of damage dealt
        cb.message.text = "-" + whowin + " Win" + "\n";
        // start a coroutine to fade away and delete this effect after a certain time

    }
}
