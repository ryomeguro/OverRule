﻿using System.Collections;
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
        StartCoroutine(MoveSequence(duration, plate, true));
        
        //transform.Rotate(90, 0, 0);
        //Debug.Log(this.transform.forward);
        //Debug.Log(direction);
        //GameManager.Instance.PlayerChange();
      
    }

    private IEnumerator MoveSequence(float duration, Transform plate, bool canPlayerChange)
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

        if (canPlayerChange)
            GameManager.Instance.PlayerChange();
    }

    public void Drop(Transform plate)
    {
        //transform.position = plate.position;
        //transform.rotation = plate.rotation;
        
        float duration = 1.0f;
        StartCoroutine(MoveSequence(duration, plate, false));
    }

    public void Death()
    {
        int damagePoint = 1;
        if (piece.isKing)
        {
            //GameManager.Instance.GameEnd(piece.playerID);
            damagePoint = 5;
        }

        GameManager.Instance.AddDamage(damagePoint, piece.playerID);
        Destroy(gameObject);
    }

    public void DropDeath()
    {
        int damagePoint = 1;
        if (piece.isKing)
        {
            //GameManager.Instance.GameEnd(piece.playerID);
            damagePoint = 5;
        }

        GameManager.Instance.AddDamage(damagePoint, piece.playerID);
        
        StartCoroutine(DropDeathSequence());

        StartCoroutine(WinCheckCoroutine());
    }

    IEnumerator DropDeathSequence()
    {
        Vector3 speed = Vector3.down * 2f;
        float duration = 5f;
        float currentTime = 0f;

        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(1f);

        Audio.Instance.PlaySE(2);
        
        while (currentTime < duration)
        {
            transform.position += speed * Time.deltaTime;
            currentTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    IEnumerator WinCheckCoroutine()
    {
        yield return new WaitForSeconds(3f);

        GameManager.Instance.WinCheck();
    }
}
