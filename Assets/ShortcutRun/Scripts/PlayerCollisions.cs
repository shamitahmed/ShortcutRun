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
    public PlayerType playerType;
    public Transform stackPos;
    public int curStackCount;

    public bool grounded;
    public bool jumping;
    public bool bouncing;
    public bool canPlaceLog;
    public float logSpawnDelay;
    public List<GameObject> logs;
    public bool botDeath;


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
        if (bouncing)
        {
            transform.position += transform.forward * Time.deltaTime * 10f;
            transform.position += Vector3.up * Time.deltaTime * 20f;
        }
        if (canPlaceLog && !GameManager.instance.dead && !bouncing)
        {
            logSpawnDelay += Time.deltaTime;
            if(logSpawnDelay >= 0.125f)
                PlaceLog();
        }
        if (grounded)
            transform.GetComponent<PlayerMovement>().speed = 6;

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
            logs.Add(go);

            GameObject fx = Instantiate(GameManager.instance.stackFX, new Vector3(stackPos.transform.position.x, stackPos.transform.position.y + 0.15f * curStackCount, stackPos.transform.position.z), Quaternion.identity);
            Destroy(fx, 1f);

            UIManager.instance.txtLogCount.text = curStackCount.ToString();
        }
        if (other.gameObject.CompareTag("water") && !GameManager.instance.dead)
        {
            if (playerType == PlayerType.human)
            {
                Camera.main.transform.parent = null;
                GameManager.instance.dead = true;
                UIManager.instance.panelGame.SetActive(false);
                UIManager.instance.panelGameOver.SetActive(true);
            }
            if (playerType == PlayerType.bot)
            {
                botDeath = true;
            }
            GameObject fx = Instantiate(GameManager.instance.splashFX, new Vector3(transform.position.x, transform.position.y, transform.position.z), GameManager.instance.splashFX.transform.rotation);
            Destroy(fx, 1f);

        }
        if (other.gameObject.CompareTag("bounce") && !GameManager.instance.dead)
        {
            //transform.DOMoveY(transform.position.y + 30f, 2f).SetLoops(2, LoopType.Yoyo).OnComplete(()=>
            //{
            //    bouncing = false;
            //});
            grounded = false;
            bouncing = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.CompareTag("water") && !GameManager.instance.dead)
        //{
        //    canPlaceLog = false;
        //}
            
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            grounded = true;
            jumping = false;
            transform.GetComponent<PlayerMovement>().anim.SetBool("jump", false);
            canPlaceLog = false;
            transform.GetComponent<PlayerMovement>().speed = 6;
            bouncing = false;
        }
        if (other.gameObject.CompareTag("logPlaced"))
        {
            transform.GetComponent<PlayerMovement>().speed = 11;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("ground") && curStackCount <= 0 && !GameManager.instance.dead)
        {
            transform.DOMoveY(transform.position.y + 7f, 0.75f).SetLoops(2,LoopType.Yoyo);
            jumping = true;
            transform.GetComponent<PlayerMovement>().anim.SetBool("jump", true);
            grounded = false;
        }
        if (other.gameObject.CompareTag("ground") && curStackCount > 0)
        {
            canPlaceLog = true;
            grounded = false;
        }
    }
    public void PlaceLog()
    {
        if (curStackCount > 0)
        {
            GameObject go = Instantiate(GameManager.instance.logPlaceObj, new Vector3(transform.position.x, 0f, transform.position.z + 0.3f), transform.rotation);
            go.transform.DOMoveY(go.transform.position.y - 0.2f, 0.05f);
            //go.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 0.2f);
            go.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.2f);
            Destroy(logs[curStackCount - 1]);
            logs.RemoveAt(curStackCount - 1);
            curStackCount--;
            GameObject fx = Instantiate(GameManager.instance.stackFX, go.transform.position, Quaternion.identity);
            Destroy(fx, 1f);
            //transform.GetComponent<PlayerMovement>().anim.SetTrigger("place");

            logSpawnDelay = 0;
            if(curStackCount <= 0)
            {
                transform.DOMoveY(transform.position.y + 7f, 0.75f).SetLoops(2, LoopType.Yoyo);
                jumping = true;
                grounded = false;
                transform.GetComponent<PlayerMovement>().anim.SetBool("carry", false);
                transform.GetComponent<PlayerMovement>().anim.SetBool("jump", true);
                transform.GetComponent<PlayerMovement>().speed = 6;
            }
        }
    }
}
