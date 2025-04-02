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

        // x��, z�� �Է� ����
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        // �Է°��� Ȱ���� ���� ����
        Vector3 moveVec = new Vector3(h, 0, v);

        // ����� �Է°��� ���� Impulse ���ϱ�(x��, z��)
        rigid.AddForce(moveVec, ForceMode.Impulse);

        rigid.AddTorque(new Vector3(2, 1, 2)); // ������ ����
    }
}