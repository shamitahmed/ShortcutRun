using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public bool gameStart;
    public bool dead;

    [Header("Stacking")]
    public GameObject logPickupObj;
    public GameObject logStackObj;
    public GameObject logPlaceObj;

    public GameObject stackFX;
    [Header("FX")]
    public GameObject splashFX;
    public GameObject confettiFX;
    [Header("End Bonus")]
    public GameObject endPlatform;
    public GameObject finishLine;



    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public IEnumerator EndBonusPods()
    {
        for (int i = 0; i < 10; i++)
        {
            //spawn
            yield return new WaitForSeconds(0.2f);
            GameObject go = Instantiate(endPlatform, new Vector3(finishLine.transform.position.x, endPlatform.transform.position.y, finishLine.transform.position.z + 10f * (i + 1)), Quaternion.identity);

        }
    }
}
