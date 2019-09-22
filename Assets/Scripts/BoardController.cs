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
        
        if (currentToPlacePiece != null)
        {
            //currentToPlacePiece.GetComponent<PieceViewer>().death();
        }

        boardModel.pieces[plate.x, plate.y, plate.z] = piece;
        boardModel.pieces[pieceCoord.x, pieceCoord.y, pieceCoord.z] = null;
    }

}