using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    public Transform target;
    public Vector3 offset;
    public float smoothFactor;
    private bool isTargetFound = false;

    public static CameraController SharedManager()
    {
        return Instance;
    }
    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }
    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }
        if (target && isTargetFound == false)
        {
            isTargetFound = true;
        }

        if (!GameManager.instance.dead)
        {
            Vector3 desiredPosition = target.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothFactor * Time.deltaTime);
            transform.position = smoothedPosition;

            if(GameManager.instance.player.GetComponent<PlayerCollisions>().curStackCount >= 10)
            {
                offset.y = Mathf.Lerp(offset.y, 12 , 0.25f);
            }
            if (GameManager.instance.player.GetComponent<PlayerCollisions>().curStackCount >= 20)
            {
                offset.y = Mathf.Lerp(offset.y, 15, 0.25f);
            }
            if (GameManager.instance.player.GetComponent<PlayerCollisions>().curStackCount < 10)
            {
                offset.y = Mathf.Lerp(offset.y, 10, 0.25f);
            }
            //transform.rotation = target.rotation;
            //transform.LookAt(target);
        }

    }
}
