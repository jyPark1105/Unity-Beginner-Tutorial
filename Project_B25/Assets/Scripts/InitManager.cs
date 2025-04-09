using UnityEngine;

// Pre-processor Execution
[DefaultExecutionOrder(-100)]

public class InitManager : MonoBehaviour
{
    // Quest Number
    public int questNumber;

    // Quest IDs
    public int[] questId;

    // NPC IDs
    public int npcA_ID;
    public int npcB_ID;

    // Object IDs
    public int doppelA_ID;

    void Awake()
    {
        questNumber = 5;
        InitGameObjectIDs();
    }

    public void InitGameObjectIDs()
    {
        // Assign Quest IDs
        questId = new int[questNumber];
        for (int idx = 0; idx < questNumber; idx++) { questId[idx] = 10 * (idx + 1); }

        // Quest ID: 10 -> 20 -> 30 -> ...
        questId[0] = 10;
        questId[1] = 20;

        npcA_ID = 1000;
        npcB_ID = 2000;
        doppelA_ID = 5000;
    }
}