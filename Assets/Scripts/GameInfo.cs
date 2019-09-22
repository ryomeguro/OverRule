using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo
{
    //何回戦か
    public static int gameNum = 1;

    //何回勝ったか
    public static int[] winNum = {0, 0};

    public static void Init()
    {
        gameNum = 1;
        winNum[0] = 0;
        winNum[1] = 0;
    }
}
