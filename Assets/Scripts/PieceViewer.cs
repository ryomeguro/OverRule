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

    private Vector3 movevector;
    private Vector3 targetposition;
    private Vector3 direction;

    public void Move(Transform plate)
    {

        targetposition = plate.transform.position;
        direction = targetposition - transform.position;
        movevector = (targetposition - transform.position)/20;
        //transform.forward=direction - transform.forward;
        //transform.forward = movevector;

        StartCoroutine("Sample");
        
        iTween.RotateTo(this.gameObject,iTween.Hash(
            "x",plate.transform.eulerAngles.x,
            "y", plate.transform.eulerAngles.y,
            "z", plate.transform.eulerAngles.z,
            "time",2.0f)
            );
        
            
        //transform.Rotate(90, 0, 0);
        //Debug.Log(this.transform.forward);
        Debug.Log(direction);
        GameManager.Instance.PlayerChange();
      
    }

    private IEnumerator Sample()
    {

        while (transform.position != targetposition)
        {
            transform.position += movevector;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void Drop(Transform plate)
    {
        transform.position = plate.position;
        transform.rotation = plate.rotation;
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
