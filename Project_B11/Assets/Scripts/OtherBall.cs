using UnityEngine;

public class OtherBall : MonoBehaviour
{
    // Ŭ���� ��ü ����
    MeshRenderer mesh;
    Material mat;

    void Start()
    {
        // ���� �ҷ�����
        mesh = GetComponent<MeshRenderer>();
        mat = mesh.material;
    }

    // ������ �浹�� �Ͼ�� �� ȣ��Ǵ� �Լ�
    void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� �±� Ȯ��
        if (collision.gameObject.tag == "MyBall")            
            mat.color = new Color(0, 0, 0); // ���� ��
    }

    // ������ �浹�� ������ �� ȣ��Ǵ� �Լ�
    void OnCollisionExit(Collision collision)
    {
        // �浹�� ������Ʈ�� �±� Ȯ��
        if (collision.gameObject.tag == "MyBall")
            mat.color = new Color(1, 1, 1); // �� ��
    }
}