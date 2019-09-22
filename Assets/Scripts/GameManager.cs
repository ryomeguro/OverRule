using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int CurrentPlayerID { get; private set; }
    
    void Awake()
    {
        Instance = this;
    }

    public void PlayerChange()
    {
        CurrentPlayerID = (CurrentPlayerID + 1) % 2;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
