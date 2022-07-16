using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTwo : MonoBehaviour
{
    private Touch touch;
    private Vector2 touchPosition;
    private Quaternion rotationY;
    public float rotateSpeedModifier = 0.1f;

    public float speed = 6f;
    public float runSpeed = 8f;
    [Range(0f, 1f)]
    public float movemenetSmoothing = .5f;
    public bool move;
    public Rigidbody rb;
    public Animator anim;
    public bool canRotate;

    PlayerType type;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        type = GetComponent<PlayerCollisions>().playerType;
    }
    private void FixedUpdate()
    {
        //if (!GameManager.instance.gameStart) return;

        if (type == PlayerType.human && !GetComponent<PlayerCollisions>().endPodReached && canRotate)
            HandlePlayerInput();

        if (!GameManager.instance.dead && GameManager.instance.gameStart && type == PlayerType.human && !GetComponent<PlayerCollisions>().endPodReached)
        {
            move = true;
            anim.SetBool("run", true);
            if (move)
            {
                //rb.MovePosition(rb.position);
                transform.position += transform.forward *  Time.fixedDeltaTime * speed;
            }
        }
    }
    void HandlePlayerInput()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                rotationY = Quaternion.Euler(0f, touch.deltaPosition.x * rotateSpeedModifier, 0f);
                transform.rotation = rotationY * transform.rotation;
            }
        }
    }
}
