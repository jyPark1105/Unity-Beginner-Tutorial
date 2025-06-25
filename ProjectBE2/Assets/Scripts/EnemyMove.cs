using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // Private Class
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    CircleCollider2D circleCollider;
    // Import Public Class
    public Animator anim;

    // Next Move
    public int enemyNextMove;
    // Next Think
    public float enemyNextThinkTime;

    void Awake()
    {
        // Rigidbody 2D
        rigid = GetComponent<Rigidbody2D>();
        // Enemy Animation
        anim = GetComponent<Animator>();
        // Sprite Renderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Circle Collider
        circleCollider = GetComponent<CircleCollider2D>();

        // Invoke Delay
        Invoke("EnemyThink", enemyNextThinkTime);
    }

    void FixedUpdate()
    {
        // Random Move
        rigid.linearVelocity = new Vector2(enemyNextMove, rigid.linearVelocity.y);

        // Cliff Ahead Platform Check
        Vector2 enemyFrontVector = new Vector2(rigid.position.x + enemyNextMove * 0.3f, rigid.position.y);
        // Ray Cast
        Debug.DrawRay(enemyFrontVector, Vector3.down, new Color(1, 0, 0, 0.5f));
        // Ray Hit with LayerMask
        RaycastHit2D rayHit = Physics2D.Raycast(enemyFrontVector, Vector3.down, 1.0f, LayerMask.GetMask("Platform"));

        // Cliff Ahead Platform Check
        if (rayHit.collider == null)
        {
            EnemyCliffTurn();
        }
    }

    void EnemyThink()
    {
        // Random Moving Direction
        enemyNextMove = Random.Range(-1, 2);

        // Random Think Time
        enemyNextThinkTime = Random.Range(2.5f, 5.0f);

        // Animator
        anim.SetInteger("walkSpeed", enemyNextMove);
        // Sprite Renderer Flip X
        if (enemyNextMove != 0)
            spriteRenderer.flipX = enemyNextMove == 1;

        // Recursive with Invoke Delay
        Invoke("EnemyThink", enemyNextThinkTime);
    }

    void EnemyCliffTurn()
    {
        // Change Direction
        enemyNextMove *= -1;
        // Sprite Renderer Flip X
        if (enemyNextMove != 0)
            spriteRenderer.flipX = enemyNextMove == 1;
        // Cancel Invoke
        CancelInvoke();
        // Restart Invoke
        Invoke("EnemyThink", enemyNextThinkTime);
    }

    public void OnDamaged()
    {
        // 1. Sprite Renderer -> Color
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // 2. Sprite Renderer -> Flip
        enemyNextMove = 0;
        spriteRenderer.flipY = true;

        // 3. Collider Disable
        circleCollider.enabled = false;

        // 4. Die Effect
        rigid.AddForce(Vector2.up * 3.0f, ForceMode2D.Impulse); 
        
        // 5. Deactivate
        Invoke("Deactive", 3);
    }

    void Deactive()
    {
        gameObject.SetActive(false);
    }
}