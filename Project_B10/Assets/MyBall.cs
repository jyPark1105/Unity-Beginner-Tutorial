using UnityEngine;

public class MyBall : MonoBehaviour
{
    Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump"))
            rigid.AddForce(Vector3.up * 15, ForceMode.Impulse);

        // x축, z축 입력 정의
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        // 입력값을 활용한 벡터 정의
        Vector3 moveVec = new Vector3(h, 0, v);

        // 사용자 입력값에 따른 Impulse 가하기(x축, z축)
        rigid.AddForce(moveVec, ForceMode.Impulse);

        rigid.AddTorque(new Vector3(2, 1, 2)); // 임의의 방향
    }
}