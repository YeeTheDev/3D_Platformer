using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public float moveSpeed;
    public float jumpForce;
    public float gravityScale = 5f;

    private Vector3 moveDirection;

    public CharacterController charController;

    private Camera theCam;

    public GameObject playerModel;
    public float rotateSpeed;

    public Animator anim;

    public bool isKnocking;
    public float knockBackLength = 0.5f;
    private float knockBackCounter;
    public Vector2 knockBackPower;

    public GameObject[] playerPieces;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
    }

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isKnocking)
        {
            float yStore = moveDirection.y;
            //moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDirection.Normalize();
            moveDirection *= moveSpeed;
            moveDirection.y = yStore;

            if (charController.isGrounded)
            {
                moveDirection.y = 0f;

                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                }
            }

            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            //transform.position = transform.position + moveDirection * Time.deltaTime * moveSpeed;

            charController.Move(moveDirection * Time.deltaTime);

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, theCam.transform.rotation.eulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));
                //playerModel.transform.rotation = newRotation;

                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }
        }

        if (isKnocking)
        {
            knockBackCounter -= Time.deltaTime;

            float yStore = moveDirection.y;
            moveDirection = playerModel.transform.forward * -knockBackPower.x;
            moveDirection.y = yStore;

            if (charController.isGrounded) { moveDirection.y = 0f; }

            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            charController.Move(moveDirection * Time.deltaTime);

            if (knockBackCounter <= 0)
            {
                isKnocking = false;
            }
        }

        anim.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        anim.SetBool("Grounded", charController.isGrounded);
    }

    public void KnockBack()
    {
        isKnocking = true;
        knockBackCounter = knockBackLength;
        moveDirection.y = knockBackPower.y;
        charController.Move(moveDirection * Time.deltaTime);
    }
}
