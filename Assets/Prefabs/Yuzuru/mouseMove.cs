using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseMove : MonoBehaviour
{
    private RaycastHit hit;
    private Ray ray;

    private Vector3 targetposition;
    


    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                targetposition = hit.point;
                

            }
        }
        if (transform.position != moveposition)
        {
            transform.position += movedistance;
        }
    }
}
