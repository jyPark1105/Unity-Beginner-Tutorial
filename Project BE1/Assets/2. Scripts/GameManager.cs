using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 전체 Item 개수
    public int totalBasicItemCount;
    public int totalSuperItemCount;
    // 현재 Stage
    public int currentStage;
    // 전체 Stage 개수
    int totalStageCount;

    // UI 표시: 획득한 Item 개수
    public Text currentBasicItemCountText;
    public Text currentSuperItemCountText;
    // UI 표시: 전체 Item 개수
    public Text totalBasicItemCountText;
    public Text totalSuperItemCountText;
    // UI 표시: 현재 Stage
    public Text currentStageText;
    // UI 표시: 전체 Stage 개수
    public Text totalStageText;
    // UI 표시: 현재 시점 상태
    public Text currentViewStateText;

    void Awake()
    {
        // 전체 Basic Item 개수 표시
        totalBasicItemCountText.text = " / " + totalBasicItemCount.ToString();
        // 전체 Super Item 개수 표시
        totalSuperItemCountText.text = " / " + totalSuperItemCount.ToString();
        // 현재 Stage 표시
        currentStageText.text = currentStage.ToString();
        // 전체 Stage 표시
        totalStageCount = SceneManager.sceneCountInBuildSettings;
        totalStageText.text = " / " + totalStageCount.ToString();
    }

    public void GetBasicItem(int count)
    {
        currentBasicItemCountText.text = count.ToString();
    }

    public void GetSuperItem(int count)
    {
        currentSuperItemCountText.text = count.ToString();
    }

    public void ConvertViewState(int viewState)
    {
        // 현재 시점 표시
        currentViewStateText.text = viewState.ToString() + "인칭";
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player Ball")
        {
            // Restart..
            SceneManager.LoadScene(currentStage - 1);
        }
    }
}