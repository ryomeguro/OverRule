using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRotate : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            OnRotate(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
        }
    }

    [SerializeField]
    float RotationSpeed;
    
    void OnRotate(Vector2 delta)
    {
        float deltaAngle = delta.magnitude * RotationSpeed;

        if (Mathf.Approximately(deltaAngle, 0.0f))
        {
            return;
        }

        Transform cameraTransform = Camera.main.transform;
        Vector3 deltaWorld = cameraTransform.right * delta.x + cameraTransform.up * delta.y;

        Vector3 axisWorld = Vector3.Cross(deltaWorld, cameraTransform.forward).normalized;

        transform.Rotate(axisWorld, deltaAngle, Space.World);
    }
}
