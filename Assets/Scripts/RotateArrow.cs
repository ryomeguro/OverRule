using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotateDirection
{
    FORWARD,
    BACK,
    LEFT,
    RIGHT
};

public class RotateArrow : MonoBehaviour
{
    public RotateDirection direction;
}
