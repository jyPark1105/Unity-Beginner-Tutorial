using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerTransform;
    Vector3 offset;
    int viewState;
    // GameManager 클래스 객체 생성(public)
    public GameManager manager;
    bool isPressed;
    int pressedCount;

    void Awake()
    {
        // 3인칭 시점
        viewState = 3;
        // Player Ball의 Transform 얻기
        playerTransform = GameObject.FindGameObjectWithTag("Player Ball").transform;
        // Camera와 Player Ball 사이 거리 계산
        offset = transform.position - playerTransform.position;
        // 특수 3인칭 Mode 전용 변수
        isPressed = false;
        pressedCount = 0;
    }

    void LateUpdate()
    {
        transform.position = playerTransform.position + offset;
        manager.ConvertViewState(viewState);

        // 특수 3인칭 Mode (더 멀리서 Player를 바라보기)
        if (Input.GetKeyDown(KeyCode.E) && !isPressed)
        {
            if (pressedCount < 2)
            {
                pressedCount++;
                offset.y += 0.5f;  // Camera 높이 증가
                offset.z -= 1.0f;  // Camera 더 멀리 이동
                transform.position = playerTransform.position + offset;
            }
            else 
                isPressed = true;
        }
    }
}
