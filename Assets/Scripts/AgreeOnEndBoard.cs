using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgreeOnEndBoard : MonoBehaviour
{
    public Button yesButton;
    public Button noButton;
    private GlobalManager globalManager;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        globalManager = GameObject.Find("GlobalManager").GetComponent<GlobalManager>();
        yesButton.onClick.AddListener(yesEvent);
        noButton.onClick.AddListener(noEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void yesEvent()
    {
        globalManager.StartEndGameClean();
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();
        gameObject.transform.Translate(new Vector3(7.5f, 4.5f, 0));
        noButton.enabled = false;
        text.text = "Click on the button when you finish picking up all the dead chesses";
        //Destroy(gameObject);
        yesButton.onClick.AddListener(AfterCleaningYesEvent);
    }

    void AfterCleaningYesEvent()
    {
        globalManager.FinishEndGameClean();
        globalManager.EndGameWithCal();
        Destroy(gameObject);
    }

    void noEvent()
    {
        globalManager.chessManager.chessCanPlay = true;
        globalManager.Option.GiveListener();
        Destroy(gameObject);
    }
}
