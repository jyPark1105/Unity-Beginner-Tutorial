using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    // Import Public Class
    public GameManager gameManager;
    public UIManager uiManager;
    public RecordManager recordManager;
    public SpriteRenderer spriteRenderer;
    public Animator anim;
    public CircleCollider2D playerCollider;
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    // Import Class
    Rigidbody2D rigid;

    // 점프력
    public float jumpPower;
    // X축 현재 속도
    public float speedX;
    // 최대 속도
    public float maxSpeed;
    // 감속 값
    public float decelerationValue;
    // 능력 시간
    float startTime;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        startTime = Time.time;

        float elapsedTime = Time.time - startTime;
        int seconds = 59 - Mathf.FloorToInt(elapsedTime % 60);
    }

    public void PlaySoundEffect(string audioClipName)
    {
        foreach (AudioClip audioClip in audioClips)
        {
            if (audioClip != null)
            {
                if (audioClip.name == audioClipName)
                    audioSource.PlayOneShot(audioClip);
            }
        }
    }

    void Update()
    {
        // 플레이어 점프
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
        {
            // Player Jump Sound
            PlaySoundEffect("0_Jump");
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);

            if (recordManager.isGreenPotion)
                PlaySoundEffect("8_DoubleUpJump");
            else
            {

            }
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

        // Run & Idle Animation
        if (Mathf.Abs(rigid.linearVelocity.x) < 0.3f)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);
    }

    void FixedUpdate()
    {
        // Horizontal Input
        float h = Input.GetAxisRaw("Horizontal");
        // Object Move
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        speedX = rigid.linearVelocity.x;

        // Only Input Value
        if (Mathf.Abs(speedX) == 1)
        {
            if (Mathf.Abs(h) > 1)
            {
                rigid.linearVelocity = new Vector2(0, rigid.linearVelocity.y);
            }
            else if (Mathf.Abs(h) == 1 && Mathf.Abs(speedX) == 1)
            {
                Vector2 newPosition = transform.position;
                newPosition.y -= 0.05f; // Update to Reduced Y
                transform.position = newPosition;
            }
        }

        // Speed Control(Right Direction)
        if (rigid.linearVelocity.x > maxSpeed)
            rigid.linearVelocity = new Vector2(maxSpeed, rigid.linearVelocity.y);
        // Speed Control(Left Direction)
        else if (rigid.linearVelocity.x < maxSpeed * (-1))
            rigid.linearVelocity = new Vector2(maxSpeed * (-1), rigid.linearVelocity.y);

        // -y direction
        if (rigid.linearVelocity.y < 0.0f)
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
                    //Debug.Log(rayHit.distance);
                    anim.SetBool("isJumping", false);
                    // Land to Platform Sound
                    PlaySoundEffect("4_Landing");
                }
            }
        }
    }

    // When Player is Damaged
    void OnDamaged(Vector2 targetPosition, string enemyTag)
    {
        // Health Down
        gameManager.ReduceHealth();

        // Layer: PlayerDamaged
        gameObject.layer = 9;

        // Color Effect
        spriteRenderer.color = new Color(1, 1, 1, 0.3f);

        if (enemyTag == "Monster")
            PlaySoundEffect("5_MonsterDamaged");    // Player Damaged by Monster Sound
        else if (enemyTag == "Spike")
            PlaySoundEffect("6_SpikeDamaged");      // Player Damaged by Spike Sound

        // Reaction Force(Left or Right)
        int forceDirection = transform.position.x - targetPosition.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2((float)forceDirection, 1) * jumpPower * 0.7f, ForceMode2D.Impulse);

        // Animation
        anim.SetTrigger("OnDamaged");

        // Immortal Effect Exit
        Invoke("OffDamaged", 3);
    }

    // After 3 seconds
    void OffDamaged()
    {
        gameObject.layer = 8;
        spriteRenderer.color = new Color(1, 1, 1, 1.0f);
    }

    void OnAttack(Transform enemy)
    {
        // Player Kill Monster Sound
        PlaySoundEffect("3_KillMonster");

        // Kill Monster
        recordManager.currentKilledMonsterCount[recordManager.currentStageIndex]++;
        recordManager.totalKilledMonsterCount++;
        recordManager.currentStageScore[recordManager.currentStageIndex] += 10;
        recordManager.totalScore += recordManager.currentStageScore[recordManager.currentStageIndex];

        // Reaction Force
        rigid.AddForce(Vector2.up, ForceMode2D.Impulse);

        // Attack Logic
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.anim.SetTrigger("OnDamaged");
        enemyMove.OnDamaged();
    }

    public void OnDie()
    {
        // Player Died Sound
        PlaySoundEffect("15_Died");

        // Color Effect
        spriteRenderer.color = new Color(1, 1, 1, 0.3f);

        // Flip Y
        spriteRenderer.flipY = true;

        // Die Reaction
        rigid.AddForce(Vector2.up * jumpPower * 0.5f, ForceMode2D.Impulse);
    }
    public void PlayerDoubleUpJump() { anim.SetBool("isJumping", false); }
    public void PlayerEnhancedJumpPower()
    {
        StartCoroutine(GamePlayerEnhancedJumpPower());
    }

    IEnumerator GamePlayerEnhancedJumpPower()
    {
        jumpPower *= 1.5f;
        
        // 5 Second Delay
        yield return new WaitForSeconds(5.0f);

        jumpPower /= 1.5f;
    }

    public void PlayerImmortal()
    {
        StartCoroutine(GamePlayerImmortal());
    }

    IEnumerator GamePlayerImmortal()
    {
        gameObject.layer = 9;

        // Color Effect
        spriteRenderer.color = new Color(1, 1, 1, 0.3f);

        // 5 Second Delay
        yield return new WaitForSeconds(5.0f);

        OffDamaged();
    }
    public void PlayerReposition()
    {
        rigid.linearVelocity = Vector2.zero;
        transform.position = new Vector3(gameManager.currentSavePosition.x, gameManager.currentSavePosition.y, -1.0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Player: Collision -> Enemy
        if (collision.gameObject.layer == 7) // Monster Index
        {
            // Attack Enemy
            if (rigid.linearVelocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }
            // Attacked By Enemy
            else
                OnDamaged(collision.transform.position, collision.gameObject.tag);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Coin Acquisition
        if (collision.gameObject.tag == "Item")
        {
            // Player Get Coin Sound
            PlaySoundEffect("1_GetCoin");
            recordManager.GetCoin(collision);
        }

        if(collision.gameObject.tag == "Potion")
        {
            // Player Get Potion Sound
            PlaySoundEffect("2_GetPotion");
            /* Auto Call(Event Function)
               in 'RecordManager.cs' */
        }
        
        // Portal Flag
        if (collision.gameObject.tag == "Portal")
        {
            // Player Get in Portal Sound
            PlaySoundEffect("10_Portal");
            recordManager.GetInPortal(collision, gameManager.portalChildList.Count);
        }

        // Save Point
        if (collision.gameObject.tag == "SavePoint")
        {
            // Player Saved Point Sound
            PlaySoundEffect("11_SavePoint");
            recordManager.GetSavePoint(collision, gameManager.saveChildList.Count);
        }

        // Move to Next Stage
        if (collision.gameObject.tag == "Finish")
        {
            gameManager.NextStage();
        }   
    }
}