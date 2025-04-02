using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // ��ü Item ����
    public int totalBasicItemCount;
    public int totalSuperItemCount;
    // ���� Stage
    public int currentStage;
    // ��ü Stage ����
    int totalStageCount;

    // UI ǥ��: ȹ���� Item ����
    public Text currentBasicItemCountText;
    public Text currentSuperItemCountText;
    // UI ǥ��: ��ü Item ����
    public Text totalBasicItemCountText;
    public Text totalSuperItemCountText;
    // UI ǥ��: ���� Stage
    public Text currentStageText;
    // UI ǥ��: ��ü Stage ����
    public Text totalStageText;
    // UI ǥ��: ���� ���� ����
    public Text currentViewStateText;

    void Awake()
    {
        // ��ü Basic Item ���� ǥ��
        totalBasicItemCountText.text = " / " + totalBasicItemCount.ToString();
        // ��ü Super Item ���� ǥ��
        totalSuperItemCountText.text = " / " + totalSuperItemCount.ToString();
        // ���� Stage ǥ��
        currentStageText.text = currentStage.ToString();
        // ��ü Stage ǥ��
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
        // ���� ���� ǥ��
        currentViewStateText.text = viewState.ToString() + "��Ī";
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