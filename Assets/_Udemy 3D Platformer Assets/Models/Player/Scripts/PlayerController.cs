using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    public float gravityScale = 5f;

    private Vector3 moveDirection;

    public CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float yStore = moveDirection.y;
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveDirection *= moveSpeed;
        moveDirection.y = yStore;

        if (Input.GetButtonDown("Jump"))
        {
            moveDirection.y = jumpHeight;
        }

        moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

        //transform.position = transform.position + moveDirection * Time.deltaTime * moveSpeed;

        characterController.Move(moveDirection * Time.deltaTime);
    }
}
