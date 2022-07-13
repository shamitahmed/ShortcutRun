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
    public bool canPlaceLog;
    public float logSpawnDelay;
    public List<GameObject> logs;



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
        if (canPlaceLog)
        {
            logSpawnDelay += Time.deltaTime;
            if(logSpawnDelay >= 0.2f)
                PlaceLog();
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
            logs.Add(go);

            GameObject fx = Instantiate(GameManager.instance.stackFX, new Vector3(stackPos.transform.position.x, stackPos.transform.position.y + 0.15f * curStackCount, stackPos.transform.position.z), Quaternion.identity);
            Destroy(fx, 1f);

            UIManager.instance.txtLogCount.text = curStackCount.ToString();
        }
        if (other.gameObject.CompareTag("water") && !GameManager.instance.dead)
        {
            GameManager.instance.dead = true;
            GameObject fx = Instantiate(GameManager.instance.splashFX, new Vector3(transform.position.x, transform.position.y, transform.position.z), GameManager.instance.splashFX.transform.rotation);
            Destroy(fx, 1f);

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
            jumping = false;
            transform.GetComponent<PlayerMovement>().anim.SetBool("jump", false);
            canPlaceLog = false;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("ground") && curStackCount <= 0)
        {
            transform.DOMoveY(transform.position.y + 7f, 0.75f).SetLoops(2,LoopType.Yoyo);
            jumping = true;
            transform.GetComponent<PlayerMovement>().anim.SetBool("jump", true);
 
        }
        if (other.gameObject.CompareTag("ground") && curStackCount > 0)
        {
            canPlaceLog = true;
        }
    }
    public void PlaceLog()
    {
        if (curStackCount > 0)
        {
            GameObject go = Instantiate(GameManager.instance.logPlaceObj, new Vector3(transform.position.x, 0f, transform.position.z + 0.3f), Quaternion.identity);
            go.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 0.2f);
            go.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.2f);
            Destroy(logs[curStackCount - 1]);
            logs.RemoveAt(curStackCount - 1);
            curStackCount--;
           
            logSpawnDelay = 0;
            if(curStackCount <= 0)
            {
                transform.DOMoveY(transform.position.y + 7f, 0.75f).SetLoops(2, LoopType.Yoyo);
                jumping = true;
                transform.GetComponent<PlayerMovement>().anim.SetBool("carry", false);
                transform.GetComponent<PlayerMovement>().anim.SetBool("jump", true);
            }
        }
    }
}
