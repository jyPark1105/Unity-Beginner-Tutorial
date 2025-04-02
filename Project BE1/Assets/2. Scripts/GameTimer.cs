using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public Text timerText; // UI Text Object
    private float startTime; // Game ���� �ð�

    void Start()
    {
        startTime = Time.time; // Game ���� �ð� ����
    }

    void Update()
    {
        float elapsedTime = Time.time - startTime; // Current Time - Start Time
        int minutes = Mathf.FloorToInt(elapsedTime / 60); // �� ���� ��ȯ
        int seconds = Mathf.FloorToInt(elapsedTime % 60); // �� ���� ��ȯ

        // UI Update (00:00 Format)
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
