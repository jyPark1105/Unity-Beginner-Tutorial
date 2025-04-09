using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    // Public Class
    public InitManager initManager;
    public ObjectTalkManager objectTalkManager;

    Dictionary<int, Sprite> npcPortraitImageData;
    public Sprite[] npcPortraitImageArray;

    // IDs
    int npcA_ID;
    int npcB_ID;

    void Awake()
    {
        npcPortraitImageData = new Dictionary<int, Sprite>();

        npcA_ID = initManager.npcA_ID;
        npcB_ID = initManager.npcB_ID;

        
        GenerateNpcPortrait();
    }

    void Update()
    {

    }

    // Add Sprite Image
    void GenerateNpcPortrait()
    {
        for (int index = 0; index < npcPortraitImageArray.Length; index++) 
        {
            if (index < 4)  // NPC_A Portrait: 0 ~ 3 
                npcPortraitImageData.Add(npcA_ID + index, npcPortraitImageArray[index]);
            else if (index >= 4)       // NPC_B Portrait: 4 ~ 7
                npcPortraitImageData.Add(npcB_ID + index - 4, npcPortraitImageArray[index]);
        }
    }

    // Get Portrait Image
    public Sprite GetPortrait(int id, int portraitIndex)
    {
        if (id == npcA_ID || id == npcB_ID)
            return npcPortraitImageData[id + portraitIndex];
        else
            return null;
    }
}