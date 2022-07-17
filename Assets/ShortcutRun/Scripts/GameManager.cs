using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public bool gameStart;
    public bool dead;
    public bool finishCrossed;
    public string totalCoinKey = "totalCoinkey";
    public int totalCoin;

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
    public Material[] podMats;
    public Transform[] finishLinePositions;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        totalCoin = PlayerPrefs.GetInt(totalCoinKey, 0);
        UIManager.instance.txtTotalCoin.text = totalCoin.ToString();
    }
    public IEnumerator EndBonusPods()
    {
        for (int i = 0; i < 20; i++)
        {
            //spawn
            yield return new WaitForSeconds(0.2f);
            GameObject go = Instantiate(endPlatform, new Vector3(finishLine.transform.position.x + Random.Range(-10,10), endPlatform.transform.position.y, finishLine.transform.position.z + 10f * (i + 1)), Quaternion.identity);
            go.GetComponent<MeshRenderer>().material = podMats[Random.Range(0, podMats.Length)];
            go.transform.GetChild(0).GetComponent<TextMeshPro>().text = "X" + (i + 1).ToString();
            go.GetComponent<EndPod>().endPodID = i + 1;
            //punch scale
            go.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.2f);

        }
    }
}
