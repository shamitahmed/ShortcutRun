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
    
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        txtLogCount.transform.LookAt(Camera.main.transform);
    }

}
