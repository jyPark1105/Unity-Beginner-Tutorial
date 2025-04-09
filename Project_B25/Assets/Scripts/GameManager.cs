using UnityEngine;
using UnityEngine.UI;
// TextMeshPro
using TMPro;

public class GameManager : MonoBehaviour
{
    // Public Class
    public InitManager initManager;
    public PlayerMove playerMove;
    public ObjectTalkManager objectTalkManager;
    public UIManager uiManager;
    public NpcManager npcManager;
    public QuestManager questManager;
    public UITypeEffect uiTypeEffect;

    // 퀘스트 매니저를 변수로 생성 후, 퀘스트 번호를 가져옴.
    // State Variable
    public bool isAction;

    // Talk Variable
    public int talkIndex;

    void Start()
    {
        // Quest List Logging with Game Start
        questManager.CheckQuestSequence();
    }

    void Awake()
    {
        isAction = false;
    }

    void Update()
    {

    }

    public void Action(GameObject scanObject)
    {
        // Update Text
        // Talk with Scanned Object
        ObjectData objectData = scanObject.GetComponent<ObjectData>();
        Talk(objectData.id, objectData.isNpc);

        // Previous: uiManager.UITalkPanel.SetActive(isAction); → GameObject 클래스 객체일 때
        uiManager.UITalkPanel.SetBool("isShow", isAction);
    }

    // Talk Function
    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = 0;
        string talkData = "";

        // Set Chat Data
        if (uiTypeEffect.isTyping)
        {
            // UITypeEffect.Effecting()
            uiTypeEffect.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = questManager.GetQuestTalkIndex(id);

            // Quest Index + NPC ID = Quest Chat Data ID
            talkData = objectTalkManager.GetTalkData(id + questTalkIndex, talkIndex);
        }

        // Exit Action
        if (talkData == null)
        {
            talkIndex = 0;
            isAction = false;
            Debug.Log(questManager.CheckQuestSequence(id));
            return;
        }

        // Enter Action
        if (isNpc)
        {
            uiManager.UITalkText.SetMsg(talkData.Split(':')[0]);

            // Show NPC Portrait
            uiManager.UINpcPortraitImage.sprite = npcManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            // When NPC: Alpha 1.0f
            uiManager.UINpcPortraitImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            // Save Current NPC Sprite as Previous NPC Sprite
            uiManager.DoEffectNpcPortraitAnimation();
        }
        else
        {
            uiManager.UITalkText.SetMsg(talkData);

            // When not NPC: Alpha 0.0f
            uiManager.UINpcPortraitImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }

        isAction = true;
        talkIndex++;
    }
}