using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public int x, y, z;

    public void CoordinateSet(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}
