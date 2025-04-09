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
        talkData.Add(npcA_ID, new string[] { "�ȳ�?:0", "���� NPC_A��.:1",
                                             "�� ��ó�� NPC_B�� �־�. ������ ��!:2"});
        // Chat 1: ID_2000
        talkData.Add(npcB_ID, new string[] { "����.:1", "NPC_A�� ������ ���� ���̾�?:2",
                                             "NPC_A���� �� ��⸦ ���� �������� ���ھ�.:1" });

        // Quest 0: ID_1010, ID_2011
        talkData.Add(questId[0] + npcA_ID, new string[] { "�̼��� �ٰ�:0",
                                                       "�̼��� ���� ������ ������ ������ �� �־�.:3",
                                                       "�ϴ� NPC_B���� ���� �������� ��!:1"});
        talkData.Add((questId[0] + 1) + npcB_ID, new string[] { "����. ���.:0",
                                                             "NPC_A���� ����Ʈ�� ���޹޾ұ���?:1",
                                                             "������ �ѷ����� �ʸ� ���� ����� ã��.:1"});

        // Quest 1: ID_5020, ID_2021
        talkData.Add(questId[1] + doppelA_ID, new string[] { "...",
                                                               "�� ������ �ر��� ���̴�..." });
        talkData.Add((questId[1] + 1) + npcB_ID, new string[] { "�츮 ������ �����༭ ����.:2",
                                                            "���� ���� Ž���� �غ���.:0",
                                                            "NPC_A���� �������� ��.:0"});

        // Quest 2: ID_1030
        talkData.Add(questId[2] + npcA_ID, new string[] { "�����? ���� ������ �غ��� �ʹٰ�?:2",
                                                          "��... ����! �ϴ� ������ �ѷ������� ��.:1" });
    }
    
    // ������ ��ȭ ������ ��ȯ�ϴ� �Լ� �ϳ� ���� 
    public string GetTalkData(int id, int talkDataIndex)
    {
        // Exception Handling
        if (!talkData.ContainsKey(id))
        {
            // ó������ ����Ʈ ��簡 ���� Object�� ���(Quest Action Index, Quest ID �� ��� ����)
            if (!talkData.ContainsKey(id - id % 10))
                return GetTalkData(id - id % 100, talkDataIndex);
            // ���� ����Ʈ ��簡 ���� Object�� ���(Quest Action Index �� ����)
            else
                return GetTalkData(id - id % 10, talkDataIndex); // ����Ʈ �ʱ� ���� ��������
        }       

        // id�� ��ȭ Get -> talkDataIndex�� ��ȭ�� �� ������ Get
        if (talkDataIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkDataIndex];
    }
}