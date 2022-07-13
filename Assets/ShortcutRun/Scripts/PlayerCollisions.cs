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

    // Start is called before the first frame update
    void Start()
    {
        
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
        }   
    }
}
