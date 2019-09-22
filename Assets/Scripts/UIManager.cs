using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    public GameObject[] Arrows;

    [SerializeField] private SelectableImage gameNum, turn, p1Win, p2Win;

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
    }
}
