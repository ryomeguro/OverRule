using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceViewer : MonoBehaviour
{
    private Piece piece;
    // Start is called before the first frame update
    void Start()
    {
        piece = GetComponent<Piece>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Transform plate)
    {
        transform.position = plate.position;
        transform.rotation = plate.rotation;

       GameManager.Instance.PlayerChange();
      
    }

    public void Death()
    {
        if (piece.isKing)
        {
            GameManager.Instance.GameEnd(piece.playerID);
        }
        Destroy(gameObject);
    }
}
