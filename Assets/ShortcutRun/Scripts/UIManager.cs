using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Lofelt.NiceVibrations;

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
    public GameObject tutorial;

    public TextMeshProUGUI txtTotalCoin;
    public TextMeshProUGUI txtCoinGained;
    public GameObject panelGameWin;
    public TextMeshProUGUI txtFinalPos;
    public Button btnNext;

    public GameObject onScreenKeyboard;
    public string playerNameKey = "playernamekey";
    public string playerName;
    public Button btnPlayername;

    public TextMeshProUGUI txtPlayerPosition;
    public Transform gameCamPos;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        btnStart.onClick.AddListener(() => BtnStart());
        btnRetry.onClick.AddListener(() => BtnRetryCallback());
        btnNext.onClick.AddListener(() => BtnNextCallback());

        txtStartCount.gameObject.SetActive(false);
        onScreenKeyboard.gameObject.SetActive(false);

        playerName = PlayerPrefs.GetString(playerNameKey, "Player");
        txtName.text = playerName;
        btnPlayername.onClick.AddListener(() => onScreenKeyboard.SetActive(true));
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
        panelGame.SetActive(true);
        HapticPatterns.PlayConstant(0.3f, 0f, 0.2f);
        Camera.main.transform.DOMove(gameCamPos.position,1.5f).OnComplete(()=>
        {
            GameManager.instance.player.GetComponent<PlayerMovementTwo>().canRotate = true;
        });
        StartCoroutine(StartRoutine());
    }
    public void BtnRetryCallback()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void BtnNextCallback()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public IEnumerator StartRoutine()
    {
        tutorial.SetActive(true);
        txtStartCount.gameObject.SetActive(true);
        txtStartCount.text = "3";
        txtStartCount.transform.DOPunchScale(new Vector3(0.4f,0.4f,0.4f),0.2f);
        SoundManager.Instance.PlaySFX(SoundManager.Instance.beepSFX);
        yield return new WaitForSeconds(0.7f);
        txtStartCount.text = "2";
        txtStartCount.transform.DOPunchScale(new Vector3(0.4f, 0.4f, 0.4f), 0.2f);
        SoundManager.Instance.PlaySFX(SoundManager.Instance.beepSFX);
        yield return new WaitForSeconds(0.7f);
        txtStartCount.text = "1";
        txtStartCount.transform.DOPunchScale(new Vector3(0.4f, 0.4f, 0.4f), 0.2f);
        SoundManager.Instance.PlaySFX(SoundManager.Instance.beepSFX);
        yield return new WaitForSeconds(0.7f);
        txtStartCount.text = "GO!";
        txtStartCount.transform.DOPunchScale(new Vector3(0.6f, 0.6f, 0.6f), 0.2f);
        SoundManager.Instance.PlaySFX(SoundManager.Instance.tapStartSFX);
        HapticPatterns.PlayConstant(0.3f, 0f, 0.2f);

        GameManager.instance.gameStart = true;
        //GameManager.instance.player.GetComponent<PlayerMovementTwo>().targetDirection = Vector3.forward;

        yield return new WaitForSeconds(1f);
        txtStartCount.gameObject.SetActive(false);
        tutorial.SetActive(false);
        yield return new WaitForSeconds(1f);
        txtName.gameObject.SetActive(false);
    }

    public void EnterPlayerName()
    {
        playerName = onScreenKeyboard.GetComponent<KeyboardScript>().TextField.text;
        PlayerPrefs.SetString(playerNameKey, playerName);
        txtName.text = playerName;
        onScreenKeyboard.SetActive(false);
    }
}
