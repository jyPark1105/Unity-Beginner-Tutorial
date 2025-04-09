using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Public Class
    public GameManager gameManager;

    public float playerSpeed;

    Rigidbody2D playerRigid;
    Animator playerAnim;

    // Player Move
    float h;
    float v;
    bool isHorizontalMove;
    bool hDown;
    bool vDown;
    bool hUp;
    bool vUp;

    // Ray
    Vector3 directionVector;
    public GameObject scanObject;

    void Awake()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        // Player Move With State Variable
        h = gameManager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = gameManager.isAction ? 0 : Input.GetAxisRaw("Vertical");
        hDown = gameManager.isAction ? false : Input.GetButtonDown("Horizontal");
        vDown = gameManager.isAction ? false : Input.GetButtonDown("Vertical");
        hUp = gameManager.isAction ? false : Input.GetButtonUp("Horizontal");
        vUp = gameManager.isAction ? false : Input.GetButtonUp("Vertical");

        // Check Move Direction
        if (hDown)
            isHorizontalMove = true;
        else if (vDown)
            isHorizontalMove = false;
        else if (hUp || vUp)
            isHorizontalMove = h != 0;

        // Player Animation
        if (playerAnim.GetInteger("hAxisRaw") != h)
        {
            playerAnim.SetBool("isChange", true);
            playerAnim.SetInteger("hAxisRaw", (int)h);
        } 
        else if (playerAnim.GetInteger("vAxisRaw") != v)
        {
            playerAnim.SetBool("isChange", true);
            playerAnim.SetInteger("vAxisRaw", (int)v);
        }
        else
            playerAnim.SetBool("isChange", false);

        // Ray Direction
        if (vDown && v == 1) // Up
            directionVector = Vector3.up;
        else if (vDown && v == -1) // Down
            directionVector = Vector3.down;
        else if (hDown && h == -1) // Left
            directionVector = Vector3.left;
        else if (hDown && h == 1) // Right
            directionVector = Vector3.right;

        // Scan Object
        if(Input.GetButtonDown("Jump") && scanObject != null)
        {
            //Debug.Log("This is: " + scanObject.name);
            gameManager.Action(scanObject);
        }
    }

    void FixedUpdate()
    {
        Vector2 moveVector = isHorizontalMove ? new Vector2(h, 0) : new Vector2(0, v);
        playerRigid.linearVelocity = moveVector * playerSpeed;

        Debug.DrawRay(playerRigid.position, directionVector * 0.5f, new Color(0.0f, 1.0f, 0.0f));
        // Check if Object
        RaycastHit2D rayHit = Physics2D.Raycast(playerRigid.position, directionVector, 0.7f, LayerMask.GetMask("Object"));

        // Raycast된 Object를 변수로 저장
        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
            scanObject = null;
    }
}
