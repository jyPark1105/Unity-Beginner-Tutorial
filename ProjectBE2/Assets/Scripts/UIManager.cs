using UnityEngine;
using UnityEngine.UI;
// TextMeshPro
using TMPro;

public class UIManager : MonoBehaviour
{
    // Import Public Class
    public GameManager gameManager;
    public PlayerMove player;
    public RecordManager recordManager;

    // UI Default
    public Image[] UIHealth;
    public TMP_Text[] UITotalExistingCount;
    public TMP_Text[] UICurrentCollectedCount;
    public TMP_Text UICurrentKilledCount;
    public TMP_Text[] UIStageInfo;

    // UI Menu Tab
    public GameObject UIMenu;
    public TMP_Text[] UITabStageOneCount;
    public TMP_Text[] UITabStageTwoCount;
    public TMP_Text[] UITabStageThreeCount;
    public TMP_Text[] UITabTotalCount;
    public TMP_Text[] UITabStageScore;
    public TMP_Text[] UITabCurrentPosition;

    // UI End of Game
    public Image[] UIEndImage;
    public GameObject UIRestartButton;
    public GameObject UIExitButton;
    public TMP_Text GameEndingMention;

    // Static Variable
    // Item
    static int bronzeIndex = 0;
    static int silverIndex = 1;
    static int goldIndex = 2;
    static int monsterIndex = 3;
    // Stage
    static int stage1 = 0;
    static int stage2 = 1;
    static int stage3 = 2;
    static int total = 3;
    // Position
    static int xPos = 0;
    static int yPos = 1;
    // Time
    float startTime;

    void Awake()
    {
        InitUI();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // UI Default
        UITotalExistingCount[bronzeIndex].text = " / " + recordManager.totalExistingBronzeCount.ToString();
        UITotalExistingCount[silverIndex].text = " / " + recordManager.totalExistingSilverCount.ToString();
        UITotalExistingCount[goldIndex].text = " / " + recordManager.totalExistingGoldCount.ToString();
        UITotalExistingCount[monsterIndex].text = " / " + recordManager.totalExistingMonsterCount.ToString();

        UICurrentCollectedCount[bronzeIndex].text = recordManager.collectedBronzeCount[recordManager.currentStageIndex].ToString();
        UICurrentCollectedCount[silverIndex].text = recordManager.collectedSilverCount[recordManager.currentStageIndex].ToString();
        UICurrentCollectedCount[goldIndex].text = recordManager.collectedGoldCount[recordManager.currentStageIndex].ToString();
        UICurrentKilledCount.text = recordManager.currentKilledMonsterCount[recordManager.currentStageIndex].ToString();

        // UI Update (00:00 Format)
        float elapsedTime = Time.time - startTime;
        int minutes = 59 - Mathf.FloorToInt(elapsedTime / 60);
        int seconds = 59 - Mathf.FloorToInt(elapsedTime % 60);
        UIStageInfo[0].text = (recordManager.currentStageIndex + 1).ToString();
        UIStageInfo[1].text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if (minutes < 15 && seconds <= 59)
            player.PlaySoundEffect("12_LimitedTime");
        UIStageInfo[2].text = recordManager.currentStageScore[recordManager.currentStageIndex].ToString();

        // UI Menu Tab
        UITabStageOneCount[bronzeIndex].text = recordManager.collectedBronzeCount[stage1].ToString();
        UITabStageOneCount[silverIndex].text = recordManager.collectedSilverCount[stage1].ToString();
        UITabStageOneCount[goldIndex].text = recordManager.collectedGoldCount[stage1].ToString();
        UITabStageOneCount[monsterIndex].text = recordManager.currentKilledMonsterCount[stage1].ToString();

        UITabStageTwoCount[bronzeIndex].text = recordManager.collectedBronzeCount[stage2].ToString();
        UITabStageTwoCount[silverIndex].text = recordManager.collectedSilverCount[stage2].ToString();
        UITabStageTwoCount[goldIndex].text = recordManager.collectedGoldCount[stage2].ToString();
        UITabStageTwoCount[monsterIndex].text = recordManager.currentKilledMonsterCount[stage2].ToString();

        UITabStageThreeCount[bronzeIndex].text = recordManager.collectedBronzeCount[stage3].ToString();
        UITabStageThreeCount[silverIndex].text = recordManager.collectedSilverCount[stage3].ToString();
        UITabStageThreeCount[goldIndex].text = recordManager.collectedGoldCount[stage3].ToString();
        UITabStageThreeCount[monsterIndex].text = recordManager.currentKilledMonsterCount[stage3].ToString();

        UITabTotalCount[bronzeIndex].text = recordManager.totalCollectedBronzeCount.ToString();
        UITabTotalCount[silverIndex].text = recordManager.totalCollectedSilverCount.ToString();
        UITabTotalCount[goldIndex].text = recordManager.totalCollectedGoldCount.ToString();
        UITabTotalCount[monsterIndex].text = recordManager.totalKilledMonsterCount.ToString();

        UITabStageScore[stage1].text = recordManager.currentStageScore[stage1].ToString();
        UITabStageScore[stage2].text = recordManager.currentStageScore[stage2].ToString();
        UITabStageScore[stage3].text = recordManager.currentStageScore[stage3].ToString();
        UITabStageScore[total].text = recordManager.totalScore.ToString();

        UITabCurrentPosition[xPos].text = player.transform.position.x.ToString("F1");
        UITabCurrentPosition[yPos].text = player.transform.position.y.ToString("F1");

        if (Input.GetKey(KeyCode.Tab))
            UIMenu.SetActive(true);
        else
            UIMenu.SetActive(false);
    }

    public void InitUI()
    {
        // UI Menu Tab
        UIMenu.SetActive(false);

        // UI Game End
        UIEndImage[0].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        UIEndImage[1].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        GameEndingMention.text = "";

        UIRestartButton.SetActive(false);
        UIExitButton.SetActive(false);
    }

    public void GetUIButton(bool isGameClear)
    {
        // UI Game End
        UIEndImage[0].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        UIEndImage[1].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        UIRestartButton.SetActive(true);
        UIExitButton.SetActive(true);

        TMP_Text RestartText = UIRestartButton.GetComponentInChildren<TMP_Text>();
        TMP_Text ExitText = UIExitButton.GetComponentInChildren<TMP_Text>();
        RestartText.text = "Restart";
        ExitText.text = "Exit";

        if (isGameClear && gameManager.playerHealth >= 1)
        {
            GameEndingMention.text = "Congratulations!\n\n" + "Game Clear!";
        }
        else if (!isGameClear || gameManager.playerHealth == 0)
        {
            GameEndingMention.text = "So closed..\n\n" + "Retry?";
        }

        // Game End
        Time.timeScale = 0;
    }
}
