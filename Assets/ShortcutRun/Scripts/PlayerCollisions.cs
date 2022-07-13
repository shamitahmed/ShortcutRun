using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum PlayerType
{
    human,
    bot
}
public class PlayerCollisions : MonoBehaviour
{
    public Transform stackPos;
    public int curStackCount;

    public bool jumping;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        if (jumping)
        {
            transform.position += transform.forward * Time.deltaTime * 8f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("logpickup"))
        {
            Destroy(other.gameObject);
            curStackCount++;
            transform.gameObject.GetComponent<PlayerMovement>().anim.SetBool("carry", true);    

            GameObject go = Instantiate(GameManager.instance.logStackObj, new Vector3(stackPos.transform.position.x, stackPos.transform.position.y + 0.15f * curStackCount, stackPos.transform.position.z), Quaternion.identity);
            go.transform.parent = stackPos;
            go.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 0.2f);
            go.transform.DOPunchScale(new Vector3(0.3f,0.3f,0.3f),0.2f);

            GameObject fx = Instantiate(GameManager.instance.stackFX, new Vector3(stackPos.transform.position.x, stackPos.transform.position.y + 0.15f * curStackCount, stackPos.transform.position.z), Quaternion.identity);
            Destroy(fx, 1f);
        }
        if (other.gameObject.CompareTag("water"))
        {
            GameManager.instance.dead = true;
            GameObject fx = Instantiate(GameManager.instance.splashFX, new Vector3(transform.position.x, transform.position.y, transform.position.z), GameManager.instance.splashFX.transform.rotation);
            Destroy(fx, 1f);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            jumping = false;
            transform.GetComponent<PlayerMovement>().anim.SetBool("jump", false);
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            transform.DOMoveY(transform.position.y + 7f, 0.75f).SetLoops(2,LoopType.Yoyo);
            jumping = true;
            transform.GetComponent<PlayerMovement>().anim.SetBool("jump", true);
        }

    }
}
