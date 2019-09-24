using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int CurrentPlayerID { get; private set; }

    private int[] rotatePower = {1, 1};
    private int[] getPieces = {0, 0};
    
    void Awake()
    {
        Instance = this;
    }

    public void PlayerChange()
    {
        /*for (int i = 0; i < 2; i++)
        {
            if (getPieces[i] >= 2)
            {
                //GameEnd((i + 1) % 2);
                GameEndProcedure((i + 1) % 2);
                UIManager.Instance.WinMoviePlay((i + 1) % 2);
            }
        }*/
        if (WinCheck())
        {
            return;
        }

        Audio.Instance.PlaySE(3);
        
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

        if(CurrentPlayerID == 1)
        {
            Transform cameraRig = GameObject.Find("CameraRig").transform;

            cameraRig.rotation = Quaternion.Euler(new Vector3(0, 180f, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            //PlayerChange();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //BoardController.Instance.Rotate(RotateDirection.NONE);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.Application.Quit();
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

    void GameEndProcedure(int winPlayerID)
    {
        Debug.Log("Procedure");
        GameInfo.winNum[winPlayerID]++;
        GameInfo.gameNum++;
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

        Audio.Instance.audiosource.PlayOneShot(Audio.Instance.audioclip[0]);
    }

    public void AddDamage(int damage, int playerID)
    {
        getPieces[(playerID + 1) % 2] += damage;
        Debug.Log("AddDamage:" + damage + ":" + getPieces[0] + ":" + getPieces[1]);
    }

    public bool WinCheck()
    {
        Debug.Log("Check:" + getPieces[0] + ":" + getPieces[1]);
        for (int i = 0; i < 2; i++)
        {
            if (getPieces[i] >= 2)
            {
                //GameEnd((i + 1) % 2);
                GameEndProcedure(i);
                UIManager.Instance.WinMoviePlay(i);

                StartCoroutine(GameEndSequence2());

                return true;
            }
        }

        return false;
    }
    
    IEnumerator GameEndSequence2()
    {
        if (GameInfo.winNum[0] == 2 || GameInfo.winNum[1] == 2)
        {
            yield return new WaitForSeconds(4f);
            SceneManager.LoadScene("ResultScene");
            
        }
        else
        {
            yield return new WaitForSeconds(4f);
            SceneManager.LoadScene("GameScene");
        }
    }
    
    
}
