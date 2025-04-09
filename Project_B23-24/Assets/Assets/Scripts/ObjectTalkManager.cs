using System.Collections.Generic;
using UnityEngine;

public class ObjectTalkManager : MonoBehaviour
{
    // Public Class
    public InitManager initManager;
    public NpcManager npcManager;
    public QuestManager questManager;

    // Save Chat Data & Image as Using Dictionary
    public Dictionary<int, string[]> talkData;

    // IDs
    int[] questId;

    int npcA_ID;
    int npcB_ID;
    int doppelA_ID;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();

        questId = new int[initManager.questNumber];
        for (int idx = 0; idx < initManager.questNumber; idx++) { questId[idx] = initManager.questId[idx]; }

        npcA_ID = initManager.npcA_ID;
        npcB_ID = initManager.npcB_ID;
        doppelA_ID = initManager.doppelA_ID;

        GenerateTalkData();
    }
    
    public void GenerateTalkData()
    {
        // Add Chat Data
        // Chat 0: ID_1000
        talkData.Add(npcA_ID, new string[] { "안녕?:0", "나는 NPC_A야.:1",
                                             "이 근처에 NPC_B가 있어. 만나러 가!:2"});
        // Chat 1: ID_2000
        talkData.Add(npcB_ID, new string[] { "여어.:1", "NPC_A를 만나고 오는 길이야?:2",
                                             "NPC_A에게 내 얘기를 많이 해줬으면 좋겠어.:1" });

        // Quest 0: ID_1010, ID_2011
        talkData.Add(questId[0] + npcA_ID, new string[] { "미션을 줄게:0",
                                                       "미션을 깨지 않으면 마을이 무너질 수 있어.:3",
                                                       "일단 NPC_B에게 빨리 가보도록 해!:1"});
        talkData.Add((questId[0] + 1) + npcB_ID, new string[] { "여어. 어서와.:0",
                                                             "NPC_A에게 퀘스트를 전달받았구나?:1",
                                                             "마을을 둘러보며 너를 닮은 사람을 찾아.:1"});

        // Quest 1: ID_5020, ID_2021
        talkData.Add(questId[1] + doppelA_ID, new string[] { "...",
                                                               "이 마을은 붕괴될 것이다..." });
        talkData.Add((questId[1] + 1) + npcB_ID, new string[] { "우리 마을을 지켜줘서 고마워.:2",
                                                            "이제 마을 탐험을 해보자.:0",
                                                            "NPC_A에게 가보도록 해.:0"});

        // Quest 2: ID_1030
        talkData.Add(questId[2] + npcA_ID, new string[] { "뭐라고? 마을 구경을 해보고 싶다고?:2",
                                                          "음... 좋아! 일단 마을을 둘러보도록 해.:1" });
    }
    
    // 지정된 대화 문장을 반환하는 함수 하나 생성 
    public string GetTalkData(int id, int talkDataIndex)
    {
        // Exception Handling
        if (!talkData.ContainsKey(id))
        {
            // 처음부터 퀘스트 대사가 없는 Object의 경우(Quest Action Index, Quest ID 값 모두 제거)
            if (!talkData.ContainsKey(id - id % 10))
                return GetTalkData(id - id % 100, talkDataIndex);
            // 현재 퀘스트 대사가 없는 Object의 경우(Quest Action Index 값 제거)
            else
                return GetTalkData(id - id % 10, talkDataIndex); // 퀘스트 초기 상태 가져오기
        }       

        // id로 대화 Get -> talkDataIndex로 대화의 한 문장을 Get
        if (talkDataIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkDataIndex];
    }
}