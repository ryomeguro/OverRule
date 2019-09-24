using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRotate : MonoBehaviour
{

    private int cameraMode;

    private void Start()
    {
        cameraMode = 0;
        //openingカメラ初期位置player1
        GameObject.Find("CameraRig").transform.position = new Vector3(0, 0, 0);
        GameObject.Find("CameraRig").transform.Rotate(0, 0, 0);

        //openingカメラ初期位置player2
        GameObject.Find("CameraRig").transform.position = new Vector3(0, 0, 0);
        GameObject.Find("CameraRig").transform.Rotate(0, 0, 0);

    }
    
    //コルーチンカメラ制御player1
    IEnumerator OpeningCamera1()
    {
        //openingのカメラ制御
        iTween.MoveTo(GameObject.Find("CameraRig"), iTween.Hash(

            ));
        //3秒停止
        yield return new WaitForSeconds(3);
        cameraMode = 2;
    }

    //コルーチンカメラ制御player2
    IEnumerator OpeningCamera2()
    {
        //openingのカメラ制御
        iTween.MoveTo(GameObject.Find("CameraRig"), iTween.Hash(

            ));
        //3秒停止
        yield return new WaitForSeconds(3);
        cameraMode = 2;
    }



    void Update()
    {
        //opening用の3秒たったらカメラ制御可能に
        if (cameraMode == 0)
        {
            StartCoroutine("OpeningCamera1");
        }else if (cameraMode == 2)
        {
            StartCoroutine("OpeningCamera2");
        }
        else
        {
            //gamescene

        }

        
        if (Input.GetMouseButton(1))
        {
            OnRotate(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
        }
        //マウスホイールクリックしたらデフォポジへ
        if (Input.GetMouseButtonDown(2))
        {
            iTween.RotateTo(GameObject.Find("CameraRig"), iTween.Hash(
                "rotation",new Vector3(0f,0f,0f),
                "time",1
                ));

        }
        //矢印で上下左右90度回転
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            iTween.RotateAdd(GameObject.Find("CameraRig"), iTween.Hash(
                "y",90f,
                "time",1f
                ));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            iTween.RotateAdd(GameObject.Find("CameraRig"), iTween.Hash(
            "y", -90f,
            "time", 1f
            ));
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            iTween.RotateAdd(GameObject.Find("CameraRig"), iTween.Hash(
            "x", 90f,
            "time", 1f
            ));
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            iTween.RotateAdd(GameObject.Find("CameraRig"), iTween.Hash(
            "x", -90f,
            "time", 1f
            ));
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
