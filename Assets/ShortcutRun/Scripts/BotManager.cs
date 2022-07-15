using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    public static BotManager instance;
    public List<PlayerCollisions> bots;
    public List<float> leaderboardDist;

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
        leaderboardDist.Sort();

        //int position = leaderboard.IndexOf(bots[0].distFromEnd) + 1;
        //string sufix = position == 1 ? "st" : position == 2 ? "nd" : position == 3 ? "rd" : "th";

        //positionText.text = position + sufix;

    }

}
