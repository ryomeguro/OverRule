using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    public GameObject[] Arrows;

    [SerializeField] private SelectableImage gameNum, turn, p1Win, p2Win;

    [SerializeField] private Image[] turnImages;
    [SerializeField] private SelectableImage turnPlayer;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameNum.SetImage(GameInfo.gameNum - 1);
        p1Win.SetImage(GameInfo.winNum[0]);
        p2Win.SetImage(GameInfo.winNum[1]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            foreach (var arrow in Arrows)
            {
                arrow.SetActive(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            foreach (var arrow in Arrows)
            {
                arrow.SetActive(false);
            }
        }
    }

    public void ArrowAppear(bool isActive)
    {
        foreach (var arrow in Arrows)
        {
            arrow.SetActive(isActive);
        }
    }

    public void PlayerChange(int playerID)
    {
        turn.SetImage(playerID);
        StartCoroutine(PlayerChangeSequence(playerID));
    }

    IEnumerator PlayerChangeSequence(int playerID)
    {
        turnPlayer.SetImage(playerID);
        
        float duration = 0.5f;
        float maxA = 0.8f;

        float currentTime = duration;
        
        while (currentTime > 0)
        {
            foreach (var img in turnImages)
            {
                img.color = new Color(1, 1, 1, Mathf.Max(0, maxA * (1 - currentTime / duration)));
            }

            yield return null;
            currentTime -= Time.deltaTime;
        }
        
        yield return new WaitForSeconds(1f);
        
        duration = 1f;
        currentTime = duration;
        while (currentTime > 0)
        {
            foreach (var img in turnImages)
            {
                img.color = new Color(1, 1, 1, Mathf.Max(0, maxA * currentTime / duration));
            }

            yield return null;
            currentTime -= Time.deltaTime;
        }
        
        foreach (var img in turnImages)
        {
            img.color = new Color(1, 1, 1, 0);
        }
    }

    public void ChangeArrowColor(int power)
    {
        Material arrowMat = Arrows[0].GetComponent<Renderer>().sharedMaterial;
        if (power == 1)
        {
            //arrowMat.SetColor("_MainColor", new Color(1,0,0,1));
            arrowMat.color = new Color(1, 0, 0, 1);
        }
        else
        {
            arrowMat.SetColor("_MainColor", new Color(1,1,1,0));
            arrowMat.color = new Color(1, 1, 1, 0);
        }
    }
}
