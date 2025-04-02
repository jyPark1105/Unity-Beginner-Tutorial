using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Import Class
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    // 최대 속도
    public float maxSpeed;
    // 감속 값
    public float decelerationValue;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 횡 속도 감속
        if (Input.GetButtonUp("Horizontal"))
        {
            // 횡 단위 벡터(방향)
            float direction = rigid.linearVelocity.normalized.x;
            // 방향에 맞는 감속 조정
            rigid.linearVelocity = new Vector2(direction * decelerationValue, rigid.linearVelocity.y);
        }

        // Flip Sprite
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        // 이동 및 정지 애니메이션
        if (Mathf.Abs(rigid.linearVelocity.x) < 0.3f)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);
    }       

    void FixedUpdate()
    {
        // 수평값 입력 받기
        float h = Input.GetAxisRaw("Horizontal");

        // Object 이동
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // 속도 제어(오른쪽 이동)
        if (rigid.linearVelocity.x > maxSpeed)
            rigid.linearVelocity = new Vector2(maxSpeed, rigid.linearVelocity.y);
        // 속도 제어(왼쪽 이동)
        else if (rigid.linearVelocity.x < maxSpeed * (-1))
            rigid.linearVelocity = new Vector2(maxSpeed * (-1), rigid.linearVelocity.y);
    }
}