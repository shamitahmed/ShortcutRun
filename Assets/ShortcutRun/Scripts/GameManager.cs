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


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

}
