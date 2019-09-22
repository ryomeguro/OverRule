using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

    public int ID = -1;
    public int playerID = -1;

    public Vector2Int[] movableSpots;

    public bool isKing = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CurrentPlayerID != playerID)
        {
            //piece.playerID.
            //gameobject.layer=ignore;
            this.gameObject.layer = 2;
        }

        if (GameManager.Instance.CurrentPlayerID == playerID)
        {
            if (playerID == 0) 
            {
                this.gameObject.layer = 8;
            }
            else
            {
                this.gameObject.layer = 9;
            }

        }

        //piece1=8,2=9

    }



}

//