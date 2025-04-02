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
        // ����
        if (Input.GetButtonDown("Jump"))
            rigid.AddForce(Vector3.up * 15, ForceMode.Impulse);

        // �¿� ������
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 moveVec = new Vector3(h, 0, v);

        rigid.AddForce(moveVec, ForceMode.Impulse);
    }

    // 
    void OnTriggerStay(Collider other)
    {
        if (other.name == "TriggerCube")
            rigid.AddForce(Vector3.up * 3, ForceMode.Impulse);
    }
}