using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

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
            EnemyCliffTurn();
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
}