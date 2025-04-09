using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // Public Class
    public InitManager initManager;
    public NpcManager npcManager;
    public ObjectTalkManager objectTalkManager;

    // IDs
    int[] questId;
    int currentQuestId;

    int npcA_ID;
    int npcB_ID;
    int doppelA_ID;
    

    // Quest Variables
    public int questActionIndex;
    public GameObject[] questObject;

    // Save Quest Data
    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();

        questId = new int[initManager.questNumber];
        for (int idx = 0; idx < initManager.questNumber; idx++) { questId[idx] = initManager.questId[idx]; }
         
        currentQuestId = questId[0];

        npcA_ID = initManager.npcA_ID;
        npcB_ID = initManager.npcB_ID;
        doppelA_ID = initManager.doppelA_ID;

        GenerateQuestData();
    }

    void Update()
    {
        
    }

    void GenerateQuestData()
    {
        QuestData quest0 = new QuestData(("첫 마을 방문"), new int[] { npcA_ID, npcB_ID });
        QuestData quest1 = new QuestData(("도플갱어 찾기"), new int[] { doppelA_ID, npcB_ID });
        QuestData quest2 = new QuestData(("마을 둘러보기"), new int[] { npcA_ID + 100 });
        // Add 함수로 <QuestID, QuestData> 데이터 저장
        // Quest 0: A, B = 1000, 2000
        questList.Add(questId[0], quest0);
        // Quest 1: Dop, B = 5000, 2000
        questList.Add(questId[1], quest1); 
        // Quest 2: A = 1100
        questList.Add(questId[2], quest2);
        
    }

    // Get Quest Talk Index using NPC_ID
    public int GetQuestTalkIndex(int id)
    {
        Debug.Log("NPC_ID + Quest_ID + questActionIndex is : " + 
                   id.ToString() + " " + currentQuestId.ToString() + " " + questActionIndex.ToString());
        return currentQuestId + questActionIndex;
    }

    // Quest Sequence Function
    public string CheckQuestSequence(int id)
    {

        if (id == questList[currentQuestId].npcId[questActionIndex])
        {
            questActionIndex++;
        }

        // Control Quest Object
        ControlObject();

        // 퀘스트 대화순서가 끝에 도달했을 때 퀘스트 번호 증가
        if (questActionIndex == questList[currentQuestId].npcId.Length)
        {
            NextQuest();
        }
        return questList[currentQuestId].questDataName;
    }

    // Game Start with Logging Quest
    public string CheckQuestSequence() { return questList[currentQuestId].questDataName; }

    void NextQuest()
    {
        int index;
        int nextIndex = 0;

        for(index = 0; index < questId.Length; index++)
        {
            if (questId[index] == currentQuestId)
            {
                nextIndex = index;
            }
        }

        currentQuestId = questId[++nextIndex];
        questActionIndex = 0;
    }

    // Quest Object Management Function
    void ControlObject()
    {
        switch (currentQuestId)
        {
            case 10:
                // Quest ID, QuestActionIndex에 따라 오브젝트 조절
                if(questActionIndex == 2)
                {
                    questObject[0].SetActive(true);
                }   
                break;
            case 20:
                if (questActionIndex == 1)
                {
                    questObject[0].SetActive(false);
                }
                break;
            case 30:
                Debug.Log("Debug::currentQuestId == 30::Undefined yet");
                break;
            case 40:
                Debug.Log("Debug::currentQuestId == 40::Undefined yet");
                break;
        }
    }
}