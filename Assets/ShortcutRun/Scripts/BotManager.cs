using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    public static BotManager instance;
    public List<PlayerCollisions> bots;
    public List<float> leaderboardDist;
    public int playerPos;
    public int playerFinalPos;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < bots.Count; i++)
        {
            leaderboardDist.Add(bots[i].distFromEnd);
        }
    }
    private void Update()
    {
        if (GameManager.instance.gameStart && !GameManager.instance.finishCrossed)
        {
            leaderboardDist.Sort();
            //player pos
            playerPos = leaderboardDist.IndexOf(bots[0].distFromEnd) + 1;
            string sufix = playerPos == 1 ? "st" : playerPos == 2 ? "nd" : playerPos == 3 ? "rd" : "th";
            UIManager.instance.txtPlayerPosition.text = playerPos + sufix;
        }
    }

}
