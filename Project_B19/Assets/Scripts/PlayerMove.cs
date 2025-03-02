using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Import Class
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    public float jumpPower;
    // �ִ� �ӵ�
    public float maxSpeed;
    // ���� ��
    public float decelerationValue;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // �÷��̾� ����
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }
            
        // Ⱦ �ӵ� ����
        if (Input.GetButtonUp("Horizontal"))
        {
            // Ⱦ ���� ����(����)
            float direction = rigid.linearVelocity.normalized.x;
            // ���⿡ �´� ���� ����
            rigid.linearVelocity = new Vector2(direction * decelerationValue, rigid.linearVelocity.y);
        }

        // Flip Sprite
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        // �̵� �� ���� �ִϸ��̼�
        if (Mathf.Abs(rigid.linearVelocity.x) < 0.3f)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);
    }       

    void FixedUpdate()
    {
        // ���� �Է� �ޱ�
        float h = Input.GetAxisRaw("Horizontal");

        // Object �̵�
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // �ӵ� ����(������ �̵�)
        if (rigid.linearVelocity.x > maxSpeed)
            rigid.linearVelocity = new Vector2(maxSpeed, rigid.linearVelocity.y);
        // �ӵ� ����(���� �̵�)
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

    void OnDamaged(Vector2 targetPosition)
    {
        // Layer: PlayerDamaged
        gameObject.layer = 9;

        // Color Effect
        spriteRenderer.color = new Color(1, 1, 1, 0.3f);

        // Reaction Force(Left or Right)
        int forceDirection = transform.position.x - targetPosition.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2((float)forceDirection, 1) * 7.0f, ForceMode2D.Impulse);

        // Animation
        anim.SetTrigger("OnDamaged");

        // Immortal Effect Exit
        Invoke("OffDamaged", 3.0f);
    }

    void OffDamaged()
    {
        gameObject.layer = 8;
        spriteRenderer.color = new Color(1, 1, 1, 1.0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Collision -> Enemy(Player ����)
        if (collision.gameObject.tag == "Enemy")
        {
            OnDamaged(collision.transform.position);
        }
    }
}