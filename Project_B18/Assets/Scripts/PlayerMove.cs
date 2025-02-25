using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Import Class
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    public float jumpPower;
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
        // 플레이어 점프
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }
            
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

        if(rigid.linearVelocity.y < 0.0f)
        {
            // Ray Cast
            Debug.DrawRay(rigid.position, Vector3.down, new Color(1, 0, 0));

            // Ray Hit with LayerMask
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1.0f, LayerMask.GetMask("Platform"));

            // Platform Check
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    Debug.Log(rayHit.distance);
                    anim.SetBool("isJumping", false);
                }
            }
        }
    }
}