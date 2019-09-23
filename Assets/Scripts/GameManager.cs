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
        Debug.Log("Change!");
        CurrentPlayerID = (CurrentPlayerID + 1) % 2;
        UIManager.Instance.PlayerChange(CurrentPlayerID);

        rotatePower[CurrentPlayerID] = 1;
        UIManager.Instance.ChangeArrowColor(rotatePower[CurrentPlayerID]);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        CurrentPlayerID = (GameInfo.gameNum + 1) % 2;
        UIManager.Instance.PlayerChange(CurrentPlayerID);
        UIManager.Instance.ChangeArrowColor(rotatePower[CurrentPlayerID]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerChange();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            BoardController.Instance.Rotate(RotateDirection.NONE);
        }
    }

    void GameStart()
    {
        UIManager.Instance.ChangeArrowColor(rotatePower[CurrentPlayerID]);
    }

    public void GameEnd(int rosePlayerID)
    {
        StartCoroutine(GameEndSequence(rosePlayerID));
    }

    IEnumerator GameEndSequence(int rosePlayerID)
    {
        GameInfo.gameNum++;
        int winPlayerID = (rosePlayerID + 1) % 2;
        GameInfo.winNum[winPlayerID]++;

        if (GameInfo.winNum[0] == 2 || GameInfo.winNum[1] == 2)
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("ResultScene");
            
        }
        else
        {
            yield return new WaitForSeconds(2f);
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
        UIManager.Instance.ChangeArrowColor(rotatePower[CurrentPlayerID]);
    }
}
