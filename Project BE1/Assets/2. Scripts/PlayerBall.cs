using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    public float jumpPower;
    public int basicItemCount;
    public int superItemCount;
    bool isJumping;
    bool isGrounded;

    // GameManager 클래스 객체 생성(public)
    public GameManager manager;
    Rigidbody rigid;
    public AudioSource[] audio;

    void Awake()
    {
        isJumping = false;
        isGrounded = false;
        rigid = GetComponent<Rigidbody>();
        audio = GetComponents<AudioSource>();
    }
    void Update()
    {
        // 점프
        if (Input.GetButtonDown("Jump") && !isJumping && isGrounded)
        {
            // DM-CGS-32 재생
            audio[3].Play();
            isJumping = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }
    void FixedUpdate()
    {
        // 좌우 움직임
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 moveVec = new Vector3(h, 0, v);

        rigid.AddForce(moveVec, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
            isJumping = false;

        foreach (ContactPoint contact in collision.contacts)
        {
            // 충돌 표면의 법선 벡터를 이용하여 Floor 체크
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.3f)
            {
                isGrounded = true;
                return;
            }
        }
    }
    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // Player와 Item 충돌 시
        if (other.gameObject.tag == "Item")
        {
            basicItemCount++;
            manager.GetBasicItem(basicItemCount);
            // // DM-CGS-28 재생
            audio[1].Play();
            // 해당 Item 비활성화
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "Super Item")
        {
            // 2단 Jump 능력(1회용)
            isJumping = false;
            isGrounded = true;
            superItemCount++;
            manager.GetSuperItem(superItemCount);
            // DM-CGS-07 재생
            audio[0].Play();
            // 해당 Item 비활성화
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "End Point")
        {
            if ((basicItemCount == manager.totalBasicItemCount) && (superItemCount == manager.totalSuperItemCount))
            {
                // Game Win!
                audio[4].Play(); // DM-CGS-45 재생
                if (manager.currentStage == 3)
                    SceneManager.LoadScene(0);
                else
                    SceneManager.LoadScene((manager.currentStage - 1) + 1);
            }
            else
            {
                // Restart..
                audio[2].Play(); // DM-CGS-29 재생
                SceneManager.LoadScene(manager.currentStage - 1);
            }
        }
    }
}