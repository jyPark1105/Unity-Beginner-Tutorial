using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    public float jumpPower;
    public int basicItemCount;
    public int superItemCount;
    bool isJumping;
    bool isGrounded;

    // GameManager Ŭ���� ��ü ����(public)
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
        // ����
        if (Input.GetButtonDown("Jump") && !isJumping && isGrounded)
        {
            // DM-CGS-32 ���
            audio[3].Play();
            isJumping = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }
    void FixedUpdate()
    {
        // �¿� ������
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
            // �浹 ǥ���� ���� ���͸� �̿��Ͽ� Floor üũ
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
        // Player�� Item �浹 ��
        if (other.gameObject.tag == "Item")
        {
            basicItemCount++;
            manager.GetBasicItem(basicItemCount);
            // // DM-CGS-28 ���
            audio[1].Play();
            // �ش� Item ��Ȱ��ȭ
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "Super Item")
        {
            // 2�� Jump �ɷ�(1ȸ��)
            isJumping = false;
            isGrounded = true;
            superItemCount++;
            manager.GetSuperItem(superItemCount);
            // DM-CGS-07 ���
            audio[0].Play();
            // �ش� Item ��Ȱ��ȭ
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "End Point")
        {
            if ((basicItemCount == manager.totalBasicItemCount) && (superItemCount == manager.totalSuperItemCount))
            {
                // Game Win!
                audio[4].Play(); // DM-CGS-45 ���
                if (manager.currentStage == 3)
                    SceneManager.LoadScene(0);
                else
                    SceneManager.LoadScene((manager.currentStage - 1) + 1);
            }
            else
            {
                // Restart..
                audio[2].Play(); // DM-CGS-29 ���
                SceneManager.LoadScene(manager.currentStage - 1);
            }
        }
    }
}