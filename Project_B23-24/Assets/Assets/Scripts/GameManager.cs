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

        uiManager.UITalkPanel.SetActive(isAction);
    }

    // Talk Function
    void Talk(int id, bool isNpc)
    {
        // Set Chat Data
        int questTalkIndex = questManager.GetQuestTalkIndex(id);

        // Quest Index + NPC ID = Quest Chat Data ID
        string talkData = objectTalkManager.GetTalkData(id + questTalkIndex, talkIndex);

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
            uiManager.UITalkText.text = talkData.Split(':')[0];
            uiManager.UINpcPortraitImage.sprite = npcManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));

            // When NPC: Alpha 1.0f
            uiManager.UINpcPortraitImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else
        {
            uiManager.UITalkText.text = talkData;

            // When not NPC: Alpha 0.0f
            uiManager.UINpcPortraitImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }

        isAction = true;
        talkIndex++;
    }
}