using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public Text result;
    // Start is called before the first frame update
    void Start()
    {
        /*
        try
        {
            result.text += "player1: ";
            result.text += GameInfo.winNum[0].ToString();
            result.text += "-";
            result.text += GameInfo.winNum[1].ToString();
            result.text += ": player2";
        }
        catch
        {
        }
        */



    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
