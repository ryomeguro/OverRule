using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int CurrentPlayerID { get; private set; }

    private int[] rotatePower = {1, 1};
    
    void Awake()
    {
        Instance = this;
    }

    public void PlayerChange()
    {
        CurrentPlayerID = (CurrentPlayerID + 1) % 2;
        UIManager.Instance.PlayerChange(CurrentPlayerID);

        rotatePower[CurrentPlayerID] = 1;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GameStart()
    {
        
    }

    public void GameEnd(int rosePlayerID)
    {
        GameInfo.gameNum++;
        int winPlayerID = (rosePlayerID + 1) % 2;
        GameInfo.winNum[winPlayerID]++;

        if (GameInfo.winNum[0] == 2 || GameInfo.winNum[1] == 2)
        {
            SceneManager.LoadScene("ResultScene");
            
        }
        else
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    public void UseRotate(RotateDirection rd)
    {
        if (rotatePower[CurrentPlayerID] < 1)
        {
            Debug.Log("このターンではもう回転できません。");
            return;
        }

        rotatePower[CurrentPlayerID]--;
        BoardController.Instance.Rotate(rd);
    }
}
