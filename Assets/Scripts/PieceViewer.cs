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
        float duration = 2.0f;
        StartCoroutine(MoveSequence(duration, plate));
        
        //transform.Rotate(90, 0, 0);
        //Debug.Log(this.transform.forward);
        //Debug.Log(direction);
        GameManager.Instance.PlayerChange();
      
    }

    private IEnumerator MoveSequence(float duration, Transform plate)
    {
        iTween.RotateTo(this.gameObject,iTween.Hash(
            "x",plate.transform.eulerAngles.x,
            "y", plate.transform.eulerAngles.y,
            "z", plate.transform.eulerAngles.z,
            "time",duration)
        );
        
        
        Vector3 targetPosition = plate.position;
        Vector3 vel = (targetPosition - transform.position) / duration;

        Quaternion targetAngle = plate.rotation;
        //Quaternion 
        
        float currentTime = 0;
        while (currentTime < duration)
        {
            transform.position += vel * Time.deltaTime;
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    public void Drop(Transform plate)
    {
        //transform.position = plate.position;
        //transform.rotation = plate.rotation;
        
        float duration = 1.0f;
        StartCoroutine(MoveSequence(duration, plate));
    }

    public void Death()
    {
        if (piece.isKing)
        {
            GameManager.Instance.GameEnd(piece.playerID);
        }
        Destroy(gameObject);
    }

    public void DropDeath()
    {
        StartCoroutine(DropDeathSequence());
    }

    IEnumerator DropDeathSequence()
    {
        Vector3 speed = Vector3.down * 2f;
        float duration = 5f;
        float currentTime = 0f;

        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(1f);
        
        while (currentTime < duration)
        {
            transform.position += speed * Time.deltaTime;
            currentTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
