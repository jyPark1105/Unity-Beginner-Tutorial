using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public Text timerText; // UI Text Object
    private float startTime; // Game 시작 시간

    void Start()
    {
        startTime = Time.time; // Game 시작 시간 저장
    }

    void Update()
    {
        float elapsedTime = Time.time - startTime; // Current Time - Start Time
        int minutes = Mathf.FloorToInt(elapsedTime / 60); // 분 단위 변환
        int seconds = Mathf.FloorToInt(elapsedTime % 60); // 초 단위 변환

        // UI Update (00:00 Format)
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
