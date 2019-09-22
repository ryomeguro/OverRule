using System;
using System.Collections;
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
        selectedPiece.GetComponent<PieceViewer>().Move(plate);
        MovePieceData(selectedPiece, plate.GetComponent<Plate>());
        
        boardViewer.MovableDisplay(selectedPiece);
        SetSelectedPiece(null);

    }

    public void Rotate(RotateDirection direction)
    {
        StartCoroutine(RotateSequence(direction));
    }

    IEnumerator RotateSequence(RotateDirection direction)
    {
        Vector3 eulerDir;
        switch (direction)
        {
            case RotateDirection.FORWARD :
                eulerDir = new Vector3(90,0,0);
                break;
            case RotateDirection.BACK:
                eulerDir = new Vector3(-90, 0, 0);
                break;
            case RotateDirection.LEFT:
                eulerDir = new Vector3(0, 0, 90);
                break;
            case RotateDirection.RIGHT:
                eulerDir = new Vector3(0, 0, -90);
                break;
            default:
                eulerDir = Vector3.zero;
                break;
        };

        yield return boardViewer.BoardRotate(eulerDir);

        yield return RotateBoard();
    }

    IEnumerator RotateBoard()
    {
        Vector3Int dropDir = boardModel.DropDirCalc();
        Vector3Int dDrop = dropDir * 2;

        Piece[,,] op = new Piece[boardModel.x + 2, boardModel.y + 2, boardModel.z + 2];
        Array.Copy(boardModel.pieces, op, boardModel.pieces.Length);
        

        for (int i = 0; i < boardModel.x + 1; i++)
        {
            for (int j = 0; j < boardModel.y + 1; j++)
            {
                for (int k = 0; k < boardModel.z + 1; k++)
                {
                    if(op[i,j,k] == null)
                        continue;

                    try
                    {
                        if(boardModel.plates[i + dropDir.x , j + dropDir.y, k + dropDir.z] == null)
                            continue;
                    }
                    catch (Exception e)
                    {
                        continue;
                    }

                    if (boardModel.pieces[i + dropDir.x, j + dropDir.y, k + dropDir.z] == null)
                    {
                        if (boardModel.plates[i + dDrop.x, j + dDrop.y, k + dDrop.z] == null)
                        {
                            //2
                        }
                        else if (boardModel.pieces[i + dDrop.x, j + dDrop.y, k + dDrop.z] == null)
                        {
                            //1
                        }
                        else
                        {
                            if (op[i + dDrop.x, j + dDrop.y, k + dDrop.z] == null)
                            {
                                //3 先に落ちていた場合
                            }
                            else
                            {
                                //4
                            }
                        }
                    }
                    else
                    {
                        if (boardModel.plates[i + dDrop.x, j + dDrop.y, k + dDrop.z] != null &&
                            boardModel.pieces[i + dDrop.x, j + dDrop.y, k + dDrop.z] != null)
                        {
                            //3 先に落ちていない場合
                        }
                    }
                    
                }
            }
        }

        yield return null;
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

    void MovePieceData(Piece piece, Plate plate)
    {
        Piece currentToPlacePiece = boardModel.pieces[plate.x, plate.y, plate.z];
        Vector3Int pieceCoord = boardModel.IDtoCoordinate(piece.ID);
        
        //コマを取る
        if (currentToPlacePiece != null)
        {
            currentToPlacePiece.GetComponent<PieceViewer>().Death();
        }

        boardModel.pieces[plate.x, plate.y, plate.z] = piece;
        boardModel.pieces[pieceCoord.x, pieceCoord.y, pieceCoord.z] = null;
    }

    void DropPieceData(Piece piece, int x, int y, int z)
    {
        Vector3Int pieceCoord = boardModel.IDtoCoordinate(piece.ID);
        boardModel.pieces[x, y, z] = piece;
        boardModel.pieces[pieceCoord.x, pieceCoord.y, pieceCoord.z] = null;
    }

}