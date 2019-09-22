using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardModel : MonoBehaviour
{
    public int x = 3, y = 3, z = 2;
    public float unitScale = 1f;
    public InitPiecePlace[] InitPiecePlaces;
    public GameObject PlatePrefab;
    
    public Piece[,,] pieces;
    public Transform[,,] plates;
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        pieces = new Piece[x + 2, y + 2, z + 2];
        plates = new Transform[x + 2, y + 2, z + 2];

        PlatePut();

        PiecePut();
    }

    public void PlatePut()
    {
        //上下の板を配置
        float yPos = unitScale * y / 2;
        float xStart = centerStart(x);
        float zStart = centerStart(z);
        //Debug.Log(zStart);
        for (int i = 1; i < x + 1; i++)
        {
            for (int j = 1; j < z + 1; j++)
            {
                plates[i, y + 1, j] = Instantiate(PlatePrefab).transform;
                Transform p = plates[i, y + 1, j];
                p.position = new Vector3(xStart + unitScale * (i - 1), yPos, zStart + unitScale * (j - 1));
                p.parent = transform;
                p.GetComponent<Plate>().CoordinateSet(i, y + 1, j);
            }
        }

        yPos *= -1;
        for (int i = 1; i < x + 1; i++)
        {
            for (int j = 1; j < z + 1; j++)
            {
                plates[i, 0, j] = Instantiate(PlatePrefab).transform;
                Transform p = plates[i, 0, j];
                p.position = new Vector3(xStart + unitScale * (i - 1), yPos, zStart + unitScale * (j - 1));
                p.Rotate(new Vector3(180,0,0));
                p.parent = transform;
                p.GetComponent<Plate>().CoordinateSet(i, 0, j);
            }
        }
        
        //前後の板を配置
        float zPos = unitScale * z / 2;
        float yStart = centerStart(y);
        for (int i = 1; i < x + 1; i++)
        {
            for (int j = 1; j < y + 1; j++)
            {
                plates[i, j, z + 1] = Instantiate(PlatePrefab).transform;
                Transform p = plates[i, j, z + 1];
                p.position = new Vector3(xStart + unitScale * (i - 1), yStart + unitScale * (j - 1), zPos);
                p.Rotate(new Vector3(90,0,0));
                p.parent = transform;
                p.GetComponent<Plate>().CoordinateSet(i, j, z + 1);
            }
        }

        zPos *= -1;
        for (int i = 1; i < x + 1; i++)
        {
            for (int j = 1; j < y + 1; j++)
            {
                plates[i, j, 0] = Instantiate(PlatePrefab).transform;
                //Debug.Log(plates[1,2,0]);
                Transform p = plates[i, j, 0];
                p.position = new Vector3(xStart + unitScale * (i - 1), yStart + unitScale * (j - 1), zPos);
                p.Rotate(new Vector3(-90,0,0));
                p.parent = transform;
                p.GetComponent<Plate>().CoordinateSet(i, j, 0);
            }
        }
        
        //左右の板を配置
        float xPos = unitScale * x / 2;
        for (int i = 1; i < z + 1; i++)
        {
            for (int j = 1; j < y + 1; j++)
            {
                plates[x + 1, j, i] = Instantiate(PlatePrefab).transform;
                Transform p = plates[x + 1, j, i];
                p.position = new Vector3(xPos, yStart + unitScale * (j - 1), zStart + unitScale * (i - 1));
                p.Rotate(new Vector3(0,0,-90));
                p.parent = transform;
                p.GetComponent<Plate>().CoordinateSet(x + 1, j, i);
            }
        }

        xPos *= -1;
        for (int i = 1; i < z + 1; i++)
        {
            for (int j = 1; j < y + 1; j++)
            {
                plates[0, j, i] = Instantiate(PlatePrefab).transform;
                Transform p = plates[0, j, i];
                p.position = new Vector3(xPos, yStart + unitScale * (j - 1), zStart + unitScale * (i - 1));
                p.Rotate(new Vector3(0,0,90));
                p.parent = transform;
                p.GetComponent<Plate>().CoordinateSet(0, j, i);
            }
        }
        
    }

    float centerStart(int num)
    {
        return -unitScale * num / 2 + unitScale * 0.5f;
    }

    void PiecePut()
    {
        int currentId = 1;
        foreach (InitPiecePlace ip in InitPiecePlaces)
        {
            Debug.Log(ip.x + ":" + ip.y + ":" + ip.z);
            pieces[ip.x, ip.y, ip.z] = Instantiate(ip.PiecePrefab).GetComponent<Piece>();
            Piece p = pieces[ip.x, ip.y, ip.z];
            Transform tf = plates[ip.x, ip.y, ip.z];
            Debug.Log(pieces[ip.x, ip.y, ip.z] + ":" + plates[ip.x, ip.y, ip.z]);
            p.transform.position = tf.position;
            p.transform.rotation = tf.rotation;

            p.ID = currentId++;
        }
    }

    public Vector3Int IDtoCoordinate(int ID)
    {
        for (int i = 0; i < x + 2; i++)
        {
            for (int j = 0; j < y + 2; j++)
            {
                for (int k = 0; k < z + 2; k++)
                {
                    if (pieces[i, j, k] != null && pieces[i, j, k].ID == ID)
                    {
                        return new Vector3Int(i,j,k);
                    }
                }
            }
        }
        return Vector3Int.one * -1;
    }

    public void PlatesReset()
    {
        for (int i = 0; i < x + 2; i++)
        {
            for (int j = 0; j < y + 2; j++)
            {
                for (int k = 0; k < z + 2; k++)
                {
                    if (plates[i, j, k] != null)
                    {
                        plates[i,j,k].gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
