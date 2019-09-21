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
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < z; j++)
            {
                plates[i, y + 1, j] = Instantiate(PlatePrefab).transform;
                Transform p = plates[i, y + 1, j];
                p.position = new Vector3(xStart + unitScale * i, yPos, zStart + unitScale * j);
                p.parent = transform;
            }
        }

        yPos *= -1;
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < z; j++)
            {
                plates[i, 0, j] = Instantiate(PlatePrefab).transform;
                Transform p = plates[i, 0, j];
                p.position = new Vector3(xStart + unitScale * i, yPos, zStart + unitScale * j);
                p.Rotate(new Vector3(180,0,0));
                p.parent = transform;
            }
        }
        
        //前後の板を配置
        float zPos = unitScale * z / 2;
        float yStart = centerStart(y);
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                plates[i, j, z + 1] = Instantiate(PlatePrefab).transform;
                Transform p = plates[i, j, z + 1];
                p.position = new Vector3(xStart + unitScale * i, yStart + unitScale * j, zPos);
                p.Rotate(new Vector3(90,0,0));
                p.parent = transform;
            }
        }

        zPos *= -1;
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                plates[i, 0, j] = Instantiate(PlatePrefab).transform;
                Transform p = plates[i, 0, j];
                p.position = new Vector3(xStart + unitScale * i, yStart + unitScale * j, zPos);
                p.Rotate(new Vector3(-90,0,0));
                p.parent = transform;
            }
        }
        
        //左右の板を配置
        float xPos = unitScale * x / 2;
        for (int i = 0; i < z; i++)
        {
            for (int j = 0; j < y; j++)
            {
                plates[x + 1, j, i] = Instantiate(PlatePrefab).transform;
                Transform p = plates[x + 1, j, i];
                p.position = new Vector3(xPos, yStart + unitScale * j, zStart + unitScale * i);
                p.Rotate(new Vector3(0,0,-90));
                p.parent = transform;
            }
        }

        xPos *= -1;
        for (int i = 0; i < z; i++)
        {
            for (int j = 0; j < y; j++)
            {
                plates[0, j, i] = Instantiate(PlatePrefab).transform;
                Transform p = plates[0, j, i];
                p.position = new Vector3(xPos, yStart + unitScale * j, zStart + unitScale * i);
                p.Rotate(new Vector3(0,0,90));
                p.parent = transform;
            }
        }
        
    }

    float centerStart(int num)
    {
        return -unitScale * num / 2 + unitScale * 0.5f;
    }

    void PiecePut()
    {
        
    }
}
