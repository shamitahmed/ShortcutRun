using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float walkSpeed = 3f;
    public float runSpeed = 8f;
    [Range(0f, 1f)]
    public float movemenetSmoothing = .5f;
    [Range(0f, 1f)]
    public float rotationSmoothing = .25f;
    private Vector3 mouseCurrentPos;
    private Vector3 mouseStartPos;
    private Vector3 moveDirection;
    private Vector3 targetDirection;
    private Vector3 deviation;
    private float currentDragDistance;
    public float maxDragDistance = 10f;
    public bool move;

    public Rigidbody rb;
    public Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

    }

    void Update()
    {
        //if (!GameManager.instance.startGame) return;

        HandlePlayerInput();
    }

    void FixedUpdate()
    {
        //if game start
        move = true;
        anim.SetBool("run", true);

        if (move)
        {
            deviation = targetDirection * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + deviation);

            if (targetDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                if (transform.rotation != targetRotation)
                {
                    rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothing));
                }
            }
            
        }
    }

    private void HandlePlayerInput()
    {
        mouseCurrentPos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPos = mouseCurrentPos;
            //if (!UIManager.instance.howtoPlayTapped)
            //{
            //    GameManager.instance.startGame = true;
            //    UIManager.instance.tutorial.SetActive(false);
            //    UIManager.instance.howtoPlayTapped = true;
            //}
        }
        else if (Input.GetMouseButton(0))
        {
            currentDragDistance = (mouseCurrentPos - mouseStartPos).magnitude;

            if (currentDragDistance > maxDragDistance)
            {
                //mouseStartPos = mouseCurrentPos - moveDirection * maxDragDistance;
                speed = runSpeed;
                anim.SetBool("run", true);
                //footstepAudioThresold = 0.3f;
            }
            else
            {
                anim.SetBool("run", true);
            }
            //move = true;
            moveDirection = (mouseCurrentPos - mouseStartPos).normalized;
            targetDirection = new Vector3(moveDirection.x, 0, moveDirection.y);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            move = false;
            anim.SetBool("run", false);
        }
    }
}
