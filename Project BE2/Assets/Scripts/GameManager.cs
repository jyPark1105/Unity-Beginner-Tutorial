using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Import Public Class
    public PlayerMove player;
    public UIManager uiManager;
    public RecordManager recordManager;

    /* Game Score Management */
    // 1. Total Number of Objects in Game
    public Transform[] monsterParent;
    public Transform[] bronzeCoinParent;
    public Transform[] silverCoinParent;
    public Transform[] goldCoinParent;

    // 5. Portal Information
    public Transform[] portalParent;
    public List<Transform> portalChildList;

    // 6. Player Transform Saving
    public Transform[] savePointParent;
    public List<Transform> saveChildList;
    
    public int savePointIndex;
    public Vector3 currentSavePosition;

    // 7. Player Health
    public int playerHealth;

    // 8. Stages
    public GameObject[] Stages;
    public int totalStages;
    public List<Vector3> stageStartPositions;

    public void Awake()
    {
        GetObjectTotalCount(0);
        StageStartPosition();

        currentSavePosition = stageStartPositions[0];
        playerHealth = 3;
    }

    public void GetObjectTotalCount(int stageIndex)
    {
        // 1. Load the Total Number of Monster Objects in Game
        if (monsterParent != null)
            recordManager.totalExistingMonsterCount = monsterParent[stageIndex].childCount;
        else
            Debug.Log("Stage " + (stageIndex + 1) + ": monsterParent == null");

        // 2. Load the Total Number of Bronze Coins in Game
        if (bronzeCoinParent != null)
            recordManager.totalExistingBronzeCount = bronzeCoinParent[stageIndex].childCount;
        else
            Debug.Log("Stage " + (stageIndex + 1) + ": bronzeCoinParent == null");

        // 3. Load the Total Number of Silver Coins in Game
        if (silverCoinParent != null)
            recordManager.totalExistingSilverCount = silverCoinParent[stageIndex].childCount;
        else
            Debug.Log("Stage " + (stageIndex + 1) + ": silverCoinParent == null");

        // 4. Load the Total Number of Gold Coins in Game
        if (goldCoinParent != null)
            recordManager.totalExistingGoldCount = goldCoinParent[stageIndex].childCount;
        else
            Debug.Log("Stage " + (stageIndex + 1) + ": goldCoinParent == null");

        // 5. Load the Total Number of Portal Objects in Game
        if (portalParent[stageIndex] != null)
        {
            // List of Child Nodes
            portalChildList = new List<Transform>();

            // Add All Nodes in Child List
            foreach (Transform portalChildNode in portalParent[stageIndex])
            { portalChildList.Add(portalChildNode); }

            Debug.Log("현 스테이지 전체 포탈 개수: " + portalChildList.Count);
        }
        else
            Debug.Log("Stage " + (stageIndex + 1) + ": portalParent == null");

        // 6. Load Transform of Total Save Points
        if (savePointParent[stageIndex] != null)
        {
            // List of Child Nodes
            saveChildList = new List<Transform>();

            // Add All Nodes in Child List
            foreach (Transform saveChildNode in savePointParent[stageIndex]) 
            { saveChildList.Add(saveChildNode); }
        }
        else
            Debug.Log("Stage " + (stageIndex + 1) + ": savePointParent == null");
    }

    public void StageStartPosition()
    {
        // # of Total Stages
        totalStages = Stages.Length;
        stageStartPositions.Add(new Vector3(-1.5f, -6.5f, -1));
        //stageStartPositions.Add(new Vector3(-19.5f, -1.5f, -1));
        stageStartPositions.Add(new Vector3(-30.5f, 38.5f, -1));
        stageStartPositions.Add(new Vector3(-25.5f, 25.5f, -1));
    }

    public void ReduceHealth()
    {
        uiManager.UIHealth[playerHealth - 1].color = new Color(1.0f, 1.0f, 1.0f, 0.2f);

        // Health Reduction
        if (playerHealth == 1)
        {
            // Game Fail
            playerHealth--;
            StartCoroutine(GameFail());
        }
        else 
            playerHealth--;
    }
    
    // Player Moves to the Next Stage
    public void NextStage()
    {
        // Stage Change
        if(recordManager.currentStageIndex < totalStages - 1)
        {
            // Player Going to Next Stage Sound
            player.PlaySoundEffect("13_NextStage");

            // Active: Next Stage
            Stages[recordManager.currentStageIndex].SetActive(false);
            recordManager.currentStageIndex++;
            Stages[recordManager.currentStageIndex].SetActive(true);
            // Assign Start Position of Next Stage
            currentSavePosition = stageStartPositions[recordManager.currentStageIndex];
            
            player.PlayerReposition();

            // Stage Count Initialization
            recordManager.currentKilledMonsterCount[recordManager.currentStageIndex] = 0;
            recordManager.collectedBronzeCount[recordManager.currentStageIndex] = 0;
            recordManager.collectedSilverCount[recordManager.currentStageIndex] = 0;
            recordManager.collectedGoldCount[recordManager.currentStageIndex] = 0;
            recordManager.currentStageScore[recordManager.currentStageIndex] = 0;
            playerHealth = 3;

            saveChildList.Clear();

            GetObjectTotalCount(recordManager.currentStageIndex);
        }
        else
        {
            // Player Game Clear!
            player.PlaySoundEffect("14_GameClear");
            // Result UI
            uiManager.GetUIButton(true);
        }    
    }

    public void Restart()
    {
        // Restart Game
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        uiManager.InitUI();
    }

    // Time Delay
    IEnumerator GameFail()
    {
        // Player Die
        player.OnDie();

        // 3 Second Delay
        yield return new WaitForSeconds(3.0f);

        // Collider Deactive
        player.spriteRenderer.color = new Color(1, 1, 1, 0.1f);
        player.playerCollider.enabled = false;

        // UI Game End
        uiManager.GetUIButton(false);
    }

    public void Exit() { return; }

    // When Player Falls to Cliff
    public void PlayerReposition(Collider2D collision)
    {
        collision.attachedRigidbody.linearVelocity = Vector2.zero;
        collision.transform.position = new Vector3(currentSavePosition.x, currentSavePosition.y, -1.0f);
        player.PlaySoundEffect("7_CliffDamaged");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "PlayerDamaged")
        {
            // Player falls off a cliff
            ReduceHealth();

            // Rigidbody & Transform Reapply
            if(playerHealth >= 1)
                PlayerReposition(collision);   
        }
    }
}