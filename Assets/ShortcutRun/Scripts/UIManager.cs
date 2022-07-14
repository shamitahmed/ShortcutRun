using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject panelStart;
    public GameObject panelGame;
    public GameObject panelGameOver;
    public GameObject panelCoin;
    public TextMeshProUGUI txtLogCount;

    public Button btnStart;
    public Button btnRetry;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        btnStart.onClick.AddListener(() => BtnStart());
        btnRetry.onClick.AddListener(() => BtnRetryCallback());
    }
    private void FixedUpdate()
    {
        txtLogCount.transform.LookAt(Camera.main.transform);
    }
    public void BtnStart()
    {
        panelStart.SetActive(false);
        GameManager.instance.gameStart = true;
        GameManager.instance.player.GetComponent<PlayerMovement>().targetDirection = Vector3.forward;
    }
    public void BtnRetryCallback()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
