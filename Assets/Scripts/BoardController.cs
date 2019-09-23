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
        SetSelectedPiece(null);
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
        Debug.Log("BoardRotate:DataEdit");
        Vector3Int dropDir = boardModel.DropDirCalc();
        Vector3Int dDrop = dropDir * 2;

        Piece[,,] op = new Piece[boardModel.x + 2, boardModel.y + 2, boardModel.z + 2];
        Array.Copy(boardModel.pieces, op, boardModel.pieces.Length);
        

        for (int i = 0; i < boardModel.x + 2; i++)
        {
            for (int j = 0; j < boardModel.y + 2; j++)
            {
                for (int k = 0; k < boardModel.z + 2; k++)
                {
                    //そもそもピースがない場合
                    if(boardModel.pieces[i,j,k] == null)
                        continue;

                    //これ以上落ちない場合
                    try
                    {
                        if(boardModel.plates[i + dropDir.x , j + dropDir.y, k + dropDir.z] == null)
                            continue;
                    }
                    catch (Exception e)
                    {
                        continue;
                    }

                    Piece p = boardModel.pieces[i, j, k];

                    Debug.Log("Edit!!:" + i + ":" + j + ":" + k);
                    //その他
                    if (boardModel.pieces[i + dropDir.x, j + dropDir.y, k + dropDir.z] == null)
                    {
                        //一つ下に何もない場合
                        if (boardModel.plates[i + dDrop.x, j + dDrop.y, k + dDrop.z] == null &&
                            op[i,j,k] != null)
                        {
                            Debug.Log(p.name + ":2段目にある場合");
                            //2 2段目にある場合
                            DropPiece(p, i + dropDir.x, j + dropDir.y, k + dropDir.z);
                        }
                        else if (boardModel.pieces[i + dDrop.x, j + dDrop.y, k + dDrop.z] == null &&
                                 boardModel.plates[i + dDrop.x, j + dDrop.y, k + dDrop.z] != null)
                        {
                            Debug.Log(p.name + ":3段目にある場合");
                            //1　3段目にある場合
                            //DropPiece(p, i + dDrop.x, j + dDrop.y, k + dDrop.z);
                            DropPiece(p, i + dropDir.x, j + dropDir.y, k + dropDir.z);
                        }
                        else
                        {
                            if (op[i + dDrop.x, j + dDrop.y, k + dDrop.z] == null &&
                                /*追加*/op[i,j,k] != null)
                            {
                                Debug.Log(p.name + ":２つ重なっていて先に落ちていた場合");
                                //3 ２つ重なっていて先に落ちていた場合
                                DropPiece(p, i + dropDir.x, j + dropDir.y, k + dropDir.z);
                            }
                            else if(boardModel.pieces[i + dDrop.x, j + dDrop.y, k + dDrop.z] != null)
                            {
                                Debug.Log(p.name + ":１つ空いていて落ちる場合");
                                //4　１つ空いていて落ちる場合
                                Piece dropPiece = boardModel.pieces[i + dDrop.x, j + dDrop.y, k + dDrop.z];
                                //dropPiece.GetComponent<PieceViewer>().Death();
                                dropPiece.GetComponent<PieceViewer>().DropDeath();
                                
                                //DropPiece(p, i + dDrop.x, j + dDrop.y, k + dDrop.z);
                                DropPiece(p, i + dropDir.x, j + dropDir.y, k + dropDir.z);
                            }
                        }
                    }
                    else
                    {
                        //一つ下になにかがある場合
                        if (boardModel.plates[i + dDrop.x, j + dDrop.y, k + dDrop.z] != null &&
                            boardModel.pieces[i + dDrop.x, j + dDrop.y, k + dDrop.z] == null )
                        {
                            Debug.Log(p.name + ":２つ重なっていて先に落ちていない場合");
                            //3 ２つ重なっていて先に落ちていない場合
                            Piece movePiece = boardModel.pieces[i + dropDir.x, j + dropDir.y, k + dropDir.z];
                            DropPiece(movePiece, i + dDrop.x, j + dDrop.y, k + dDrop.z);
                            DropPiece(p, i + dropDir.x, j + dropDir.y, k + dropDir.z);
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
        SetPieceRim(selectedPiece, 0);
        selectedPiece = piece;
        SetPieceRim(selectedPiece, 1);
        boardViewer.MovableDisplay(selectedPiece);
    }

    void SetPieceRim(Piece p, float value)
    {
        if(p == null)
            return;

        Transform obj = p.transform.GetChild(0);
        obj.GetComponent<Renderer>().sharedMaterial.SetFloat("_UseRim", value);
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

    void DropPiece(Piece piece, int x, int y, int z)
    {
        Debug.Log("Drop!:" + piece.name);
        Vector3Int pieceCoord = boardModel.IDtoCoordinate(piece.ID);
        boardModel.pieces[x, y, z] = piece;
        boardModel.pieces[pieceCoord.x, pieceCoord.y, pieceCoord.z] = null;

        piece.GetComponent<PieceViewer>().Drop(boardModel.plates[x, y, z]);
    }

}