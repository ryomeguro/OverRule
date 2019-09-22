using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardViewer : MonoBehaviour
{
    [SerializeField] private Piece _p;
    private BoardModel _boardModel;
    
    // Start is called before the first frame update
    void Start()
    {
        _boardModel = GetComponent<BoardModel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            MovableDisplay(_p);
        }
    }

    public void MovableDisplay(Piece piece)
    {
        _boardModel.PlatesReset();
        
        if (piece == null)
        {
            return;
        }
        
        Vector2Int[] dirs = piece.movableSpots;
        Vector3Int pcoord = _boardModel.IDtoCoordinate(piece.ID);
        
        List<Vector3Int> dirs3D = new List<Vector3Int>();
        Vector3Int overflow = Vector3Int.zero;
        
        //オーバーフローしたときに足すベクトルを計算
        if (pcoord.x == 0)
            overflow = new Vector3Int(1, 0, 0);
        if (pcoord.x == _boardModel.x + 1)
            overflow = new Vector3Int(-1, 0, 0);
        if (pcoord.y == 0)
            overflow = new Vector3Int(0, 1, 0);
        if (pcoord.y == _boardModel.y + 1)
            overflow = new Vector3Int(0, -1, 0);
        if (pcoord.z == 0)
            overflow = new Vector3Int(0, 0, 1);
        if (pcoord.z == _boardModel.y + 1)
            overflow = new Vector3Int(0, 0, -1);

        //Vector2をVector3に変換
        if (pcoord.x == 0 || pcoord.x == _boardModel.x + 1)
        {
            foreach (var dir in dirs)
            {
                Vector3Int d3 = pcoord + new Vector3Int(0, dir.x, dir.y);
                if (d3.y == 0 || d3.y == _boardModel.y + 1 || d3.z == 0 || d3.z == _boardModel.z + 1)
                    d3 += overflow;
                dirs3D.Add(d3);
            }
        }
        
        if (pcoord.y == 0 || pcoord.y == _boardModel.y + 1)
        {
            foreach (var dir in dirs)
            {
                Vector3Int d3 = pcoord + new Vector3Int(dir.x, 0, dir.y);
                if (d3.x == 0 || d3.x == _boardModel.x + 1 || d3.z == 0 || d3.z == _boardModel.z + 1)
                    d3 += overflow;
                dirs3D.Add(d3);
            }
        }
        
        if (pcoord.z == 0 || pcoord.z == _boardModel.z + 1)
        {
            foreach (var dir in dirs)
            {
                Vector3Int d3 = pcoord + new Vector3Int(dir.x, dir.y, 0);
                if (d3.x == 0 || d3.x == _boardModel.x + 1 || d3.y == 0 || d3.y == _boardModel.y + 1)
                    d3 += overflow;
                dirs3D.Add(d3);
            }
        }

        foreach (var d3 in dirs3D)
        {
            Transform plate = _boardModel.plates[d3.x, d3.y, d3.z];
            Debug.Log(d3);
            if (plate != null)
            {
                Piece placeP = _boardModel.pieces[d3.x, d3.y, d3.z];
                if(placeP == null || placeP.ID != piece.ID)
                    plate.gameObject.SetActive(true);
            }
        }

    }
}
