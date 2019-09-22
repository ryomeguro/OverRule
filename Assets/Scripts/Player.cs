using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
     
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 30.0f))
            {
                //hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                if (hit.collider.gameObject.tag == "piece")
                {
                    //BoardController.Instance.selectedPiece = hit.collider.gameObject.GetComponent<Piece>();
                    BoardController.Instance.SetSelectedPiece(hit.collider.gameObject.GetComponent<Piece>());
                }
                if (hit.collider.gameObject.tag == "plate")
                {
                    BoardController.Instance.MovePiece(hit.collider.gameObject.transform);
                }
                // Debug.Log(selectedObject.transform.position);
            }
        }


        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 30.0f))
            {
               Debug.Log(hit.collider.gameObject);
            }
        }
    }
}
