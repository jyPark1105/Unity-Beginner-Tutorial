using UnityEngine;
using UnityEngine.UI;
// TextMeshPro
using TMPro;

public class GameManager : MonoBehaviour
{
    // Public Class
    public PlayerMove playerMove;

    // Text Data
    public GameObject UITalkPanel;
    public TMP_Text UITalkText;
    
    // State Variable
    public bool isAction;

    void Awake()
    {
        isAction = false;
        UITalkPanel.SetActive(false);
    }

    void Update()
    {
        
    }

    public void Action(GameObject scanObject)
    {
        if(isAction)
            isAction = false;
        else
        {
            isAction = true;
            // Update Text
            UITalkText.text = "This is: " + playerMove.scanObject.name;
        }
        UITalkPanel.SetActive(isAction);
    }
}
