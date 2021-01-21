
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Option : MonoBehaviour
{
    public Button surButton;
    public Button endButton;
    private GlobalManager globalManager;
    // Start is called before the first frame update
    void Start()
    {
        globalManager = GameObject.Find("GlobalManager").GetComponent<GlobalManager>();
        GiveListener();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void surEvent()
    {
        globalManager.EndGameWithSur();
        TakeOutListner();
    }

    void endEvent()
    {
        globalManager.ShowEndGameBoard();
        TakeOutListner();
    }

    void TakeOutListner()
    {
        surButton.onClick.RemoveListener(surEvent);
        endButton.onClick.RemoveListener(endEvent);
    }
    public void GiveListener()
    {
        surButton.onClick.AddListener(surEvent);
        endButton.onClick.AddListener(endEvent);
    }
}
