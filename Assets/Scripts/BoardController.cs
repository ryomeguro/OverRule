﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public static BoardController Instance;

    [SerializeField] BoardModel boardModel;
    [SerializeField] BoardViewer boardViewer;

    //こまを選択したらここに入れる
    public Piece selectedPiece;
    //rotationも合わせる
    public void MovePiece(Transform plate)
    {
        //selectedPiece.transform.position = movePosition;
        //selectedPiece.transform.eulerAngles = Rotation;
        selectedPiece.GetComponent<PieceViewer>().Move(plate);

        boardViewer.MovableDisplay(selectedPiece);

    }

    private void Awake()
    {
        Instance = this;
    }

    public void SetSelectedPiece(Piece piece)
    {
        selectedPiece = piece;
        boardViewer.MovableDisplay(selectedPiece);
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