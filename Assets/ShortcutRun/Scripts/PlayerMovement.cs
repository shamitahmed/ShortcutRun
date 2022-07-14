using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float runSpeed = 8f;
    [Range(0f, 1f)]
    public float movemenetSmoothing = .5f;
    [Range(0f, 1f)]
    public float rotationSmoothing = .25f;
    private Vector3 mouseCurrentPos;
    private Vector3 mouseStartPos;
    private Vector3 moveDirection;
    public Vector3 targetDirection;
    private Vector3 deviation;
    private float currentDragDistance;
    public float maxDragDistance = 10f;
    public bool move;

    public Rigidbody rb;
    public Animator anim;
    PlayerType type;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        type = GetComponent<PlayerCollisions>().playerType;
    }

    void Update()
    {
        if (!GameManager.instance.gameStart) return;

        if(type==PlayerType.human)
            HandlePlayerInput();
    }

    void FixedUpdate()
    {
        //if game start
        if (!GameManager.instance.dead && GameManager.instance.gameStart && type == PlayerType.human)
        {
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
                //speed = runSpeed;
                anim.SetBool("run", true);
            }
            else
            {
                anim.SetBool("run", true);
            }
            //move = true;
            moveDirection = (mouseCurrentPos - mouseStartPos).normalized;
            targetDirection = new Vector3(moveDirection.x, 0, moveDirection.y);
        }
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    move = false;
        //    anim.SetBool("run", false);
        //}
    }
}
