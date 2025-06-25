using UnityEngine;
using System.Collections;

public class RecordManager : MonoBehaviour
{
    // Import Public Class
    public GameManager gameManager;
    public PlayerMove player;
    public UIManager uiManager;
    public Animator anim;

    public GameObject potionObject;

    /* Game Score Management */
    // 1. Total Number of Objects in Game
    public int totalExistingMonsterCount;
    public int totalExistingBronzeCount;
    public int totalExistingSilverCount;
    public int totalExistingGoldCount;
    public int totalExistingPortalCount;

    // 2. Monster Enemy Information 
    public int[] currentKilledMonsterCount;
    public int totalKilledMonsterCount;

    // 3. Item Information
    public int[] collectedBronzeCount;
    public int[] collectedSilverCount;
    public int[] collectedGoldCount;
    public int totalCollectedBronzeCount;
    public int totalCollectedSilverCount;
    public int totalCollectedGoldCount;

    // 4. Stage Information
    public int[] currentStageScore;
    public int currentStageIndex;
    public int totalScore;

    // 5. Potion Information
    public string currentPotionColor;
    public bool isRedPotion;
    public bool isGreenPotion;
    public bool isBluePotion;
    public bool isYellowPotion;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void GetCoin(Collider2D collision)
    {
        bool isBronze = collision.gameObject.name.Contains("Bronze");
        bool isSilver = collision.gameObject.name.Contains("Silver");
        bool isGold = collision.gameObject.name.Contains("Gold");

        // Bronze
        if (isBronze)
        {
            collectedBronzeCount[currentStageIndex]++;
            totalCollectedBronzeCount++;
            currentStageScore[currentStageIndex] += 5;
            totalScore = currentStageScore[currentStageIndex];
        }
        else if (isSilver)
        {
            collectedSilverCount[currentStageIndex]++;
            totalCollectedSilverCount++;
            currentStageScore[currentStageIndex] += 10;
            totalScore = currentStageScore[currentStageIndex];
        }
        else if (isGold)
        {
           collectedGoldCount[currentStageIndex]++;
            totalCollectedGoldCount++;
            currentStageScore[currentStageIndex] += 20;
            totalScore = currentStageScore[currentStageIndex];
        }

        // Item Deactivate
        collision.gameObject.SetActive(false);
    }

    public void GetPotion(string potionColor) 
    { 
        currentPotionColor = potionColor;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
            PotionEffect(gameObject, currentPotionColor);
    }

    public void PotionEffect(GameObject potion, string potionColor)
    {
        isRedPotion = potionColor == "redPotion";
        isGreenPotion = potionColor == "greenPotion";
        isBluePotion = potionColor == "bluePotion";
        isYellowPotion = potionColor == "yellowPotion";

        if (isRedPotion)
        {
            if (gameManager.playerHealth >= 1 && gameManager.playerHealth < 3)
            {
                gameManager.playerHealth++;
                uiManager.UIHealth[gameManager.playerHealth - 1].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
        else if (isGreenPotion)
        {
            player.PlayerDoubleUpJump();
        }
        else if (isBluePotion)
        {
            player.PlayerEnhancedJumpPower();
        }
        else if (isYellowPotion)
        {
            player.PlayerImmortal();
        }

        anim.enabled = false;
        // Potion Deactivate
        potion.SetActive(false);
    }

    public void GetSavePoint(Collider2D collisionTarget, int savePointCount)
    {
        for (int savePoint = 0; savePoint < savePointCount; savePoint++)
        {
            // Save Current Player Transform
            if (collisionTarget.gameObject.name == gameManager.saveChildList[savePoint].name)
                gameManager.currentSavePosition = new Vector3(collisionTarget.transform.position.x, collisionTarget.transform.position.y, -1.0f);
        }
    }

    // Entering Portal
    public void GetInPortal(Collider2D collisionTarget, int portalCount)
    {
        // 현재 충돌한 MiddleLine의 위치를 비교하여 이동할 위치 결정
        Transform exitTarget = null;

        // totalExisitingPortalCount: Always Even
        for (int portal = 0; portal < portalCount; portal++)
        {
            if (collisionTarget.gameObject.name == gameManager.portalChildList[portal].name)
            {

                exitTarget = gameManager.portalChildList[portal % 2 == 0 ? portal + 1 : portal - 1];

                if (exitTarget != null)
                    StartCoroutine(PortalDeactiveAndPlayerMove(collisionTarget, exitTarget));
                    StartCoroutine(PortalReactive(collisionTarget, exitTarget));

            }
        }
    }

    // Portal Deactivate
    public IEnumerator PortalDeactiveAndPlayerMove(Collider2D EnterPortal, Transform ExitPortal)
    {
        // Portal Deactivate
        EnterPortal.gameObject.SetActive(false);
        ExitPortal.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.05f);

        player.transform.position = new Vector3(ExitPortal.transform.position.x, ExitPortal.transform.position.y, -1.0f);
    }

    public IEnumerator PortalReactive(Collider2D EnterPortal, Transform ExitPortal)
    {
        // Delay 3 seconds
        yield return new WaitForSeconds(3.0f);

        // Portal Reactivate
        EnterPortal.gameObject.SetActive(true);
        ExitPortal.gameObject.SetActive(true);
    }
}
