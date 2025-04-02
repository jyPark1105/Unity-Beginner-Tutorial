using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerTransform;
    Vector3 offset;
    int viewState;
    // GameManager Ŭ���� ��ü ����(public)
    public GameManager manager;
    bool isPressed;
    int pressedCount;

    void Awake()
    {
        // 3��Ī ����
        viewState = 3;
        // Player Ball�� Transform ���
        playerTransform = GameObject.FindGameObjectWithTag("Player Ball").transform;
        // Camera�� Player Ball ���� �Ÿ� ���
        offset = transform.position - playerTransform.position;
        // Ư�� 3��Ī Mode ���� ����
        isPressed = false;
        pressedCount = 0;
    }

    void LateUpdate()
    {
        transform.position = playerTransform.position + offset;
        manager.ConvertViewState(viewState);

        // Ư�� 3��Ī Mode (�� �ָ��� Player�� �ٶ󺸱�)
        if (Input.GetKeyDown(KeyCode.E) && !isPressed)
        {
            if (pressedCount < 2)
            {
                pressedCount++;
                offset.y += 0.5f;  // Camera ���� ����
                offset.z -= 1.0f;  // Camera �� �ָ� �̵�
                transform.position = playerTransform.position + offset;
            }
            else 
                isPressed = true;
        }
    }
}
