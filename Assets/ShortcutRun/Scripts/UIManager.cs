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
    public Canvas playerCanvas;
    public TextMeshProUGUI txtLogCount;
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtStartCount;
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

        txtStartCount.gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        //playerCanvas.transform.LookAt(Camera.main.transform);
        txtLogCount.transform.LookAt(Camera.main.transform);
        txtName.transform.LookAt(Camera.main.transform);
    }
    public void BtnStart()
    {
        panelStart.SetActive(false);
        StartCoroutine(StartRoutine());
    }
    public void BtnRetryCallback()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public IEnumerator StartRoutine()
    {
        txtStartCount.gameObject.SetActive(true);
        txtStartCount.text = "3";
        txtStartCount.transform.DOPunchScale(new Vector3(0.4f,0.4f,0.4f),0.2f);
        yield return new WaitForSeconds(0.7f);
        txtStartCount.text = "2";
        txtStartCount.transform.DOPunchScale(new Vector3(0.4f, 0.4f, 0.4f), 0.2f);
        yield return new WaitForSeconds(0.7f);
        txtStartCount.text = "1";
        txtStartCount.transform.DOPunchScale(new Vector3(0.4f, 0.4f, 0.4f), 0.2f);
        yield return new WaitForSeconds(0.7f);
        txtStartCount.text = "GO!";
        txtStartCount.transform.DOPunchScale(new Vector3(0.6f, 0.6f, 0.6f), 0.2f);

        GameManager.instance.gameStart = true;
        GameManager.instance.player.GetComponent<PlayerMovement>().targetDirection = Vector3.forward;

        //BotManager.instance.bots[0].transform.position = PathCreation.pathCreator.path.GetPointAtDistance(13f);

        yield return new WaitForSeconds(1f);
        txtStartCount.gameObject.SetActive(false);

       
    }
}
