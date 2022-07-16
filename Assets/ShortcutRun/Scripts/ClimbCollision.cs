using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ClimbCollision : MonoBehaviour
{
    float climbTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        Climb();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("ground") && !transform.root.GetComponent<PlayerCollisions>().grounded && !GameManager.instance.dead && !GameManager.instance.finishCrossed)
        {
            transform.root.GetComponent<PlayerCollisions>().climbing = true;
            transform.root.GetComponent<PlayerMovementTwo>().anim.SetBool("climb", true);
            
            //climb
            transform.root.DOMoveY(1f,0.1f).OnComplete(()=>
            {
                transform.root.GetComponent<PlayerCollisions>().climbing = false;
                transform.root.GetComponent<PlayerMovementTwo>().anim.SetBool("climb", false);
            });
            transform.GetComponent<Collider>().enabled = false;
            //transform.root.DOLocalMoveZ(transform.root.localPosition.z + 1f, 0.2f);
        }
    }
    void Climb()
    {
        if (transform.root.GetComponent<PlayerCollisions>().climbing)
        {
            climbTime += Time.deltaTime;
            if (climbTime < 0.2f)
            {
                transform.root.position += transform.forward * Time.deltaTime * 5f;
                if (climbTime >= 0.2f)
                {
                    climbTime = 0;
                    transform.GetComponent<Collider>().enabled = true;
                    transform.root.GetComponent<PlayerMovementTwo>().anim.SetBool("climb", false);
                    transform.root.GetComponent<PlayerCollisions>().climbing = false;
                }
            }
        }
    }
}
