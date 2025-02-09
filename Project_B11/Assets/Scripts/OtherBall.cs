using UnityEngine;

public class OtherBall : MonoBehaviour
{
    // 클래스 객체 선언
    MeshRenderer mesh;
    Material mat;

    void Start()
    {
        // 성분 불러오기
        mesh = GetComponent<MeshRenderer>();
        mat = mesh.material;
    }

    // 물리적 충돌이 일어났을 때 호출되는 함수
    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트의 태그 확인
        if (collision.gameObject.tag == "MyBall")            
            mat.color = new Color(0, 0, 0); // 검은 색
    }

    // 물리적 충돌이 끝났을 때 호출되는 함수
    void OnCollisionExit(Collision collision)
    {
        // 충돌한 오브젝트의 태그 확인
        if (collision.gameObject.tag == "MyBall")
            mat.color = new Color(1, 1, 1); // 흰 색
    }
}