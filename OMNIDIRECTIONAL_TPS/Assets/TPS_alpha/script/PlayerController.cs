using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;

    public float MoveSpeed = 10.0f;
    public float Gravity = 9.8f;

    Vector3 MoveVector = Vector3.zero;

    public Transform CamTransform;

    public Animator animator;

    public Transform bodyTransform;
    public float turnSpeed;

    public float escapeSpeed = 10;
    public float jumpSpeed = 10;
    public bool rolling;

    float _follingY;
    float _old_follingY;

    // Use this for initialization
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        _follingY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float Move_x = Input.GetAxis("Horizontal");
        float Move_z = Input.GetAxis("Vertical");

        if (Move_x != 0 || Move_z != 0)
        {
            if (!rolling)
            {
                bodyTransform.localRotation = Quaternion.Slerp(
                    bodyTransform.localRotation,
                    Quaternion.LookRotation(new Vector3(Move_x, 0, Move_z)),
                    turnSpeed * Time.deltaTime
                );
            }

            float turnAngle = Mathf.Lerp(transform.eulerAngles.y,
                    CamTransform.eulerAngles.y,
                    turnSpeed * Time.deltaTime
                );

            transform.eulerAngles = new Vector3(0, turnAngle, 0);

        }

        if (characterController.isGrounded)
        {
            if (rolling)
            {
                MoveVector = transform.InverseTransformDirection(bodyTransform.forward) * escapeSpeed;
            }
            else
            {
                MoveVector = new Vector3(Move_x, 0, Move_z) * MoveSpeed;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                MoveVector.y = jumpSpeed;
            }
        }
        
        MoveVector.y -= Gravity * Time.deltaTime;

        animator.SetBool("IsGround", characterController.isGrounded);
        animator.SetFloat("folling", MoveVector.y);

        animator.SetFloat("speed", new Vector2(Move_x,Move_z).magnitude);

        characterController.Move(transform.TransformDirection(MoveVector * Time.deltaTime));
    }
}
