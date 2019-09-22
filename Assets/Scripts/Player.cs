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
                BoardController.Instance.selectedPiece = hit.collider.gameObject.GetComponent<Piece>();
                
                // Debug.Log(selectedObject.transform.position);
            }
        }


        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 30.0f))
            {
                BoardController.Instance.MovePiece(hit.collider.gameObject.transform.position,hit.collider.gameObject.transform.eulerAngles);
                //Debug.Log(hit.collider.gameObject.transform.position);
            }
        }
    }
}
