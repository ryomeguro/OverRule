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
                if (hit.collider.gameObject.tag == "piece")
                {
                    Piece piece=hit.collider.gameObject.GetComponent<Piece>();
                    
                        if (GameManager.Instance.CurrentPlayerID == piece.playerID)
                        {
                            //BoardController.Instance.selectedPiece = hit.collider.gameObject.GetComponent<Piece>();
                            BoardController.Instance.SetSelectedPiece(hit.collider.gameObject.GetComponent<Piece>());

                        //チョイスSE
                        Audio.Instance.audiosource.PlayOneShot(Audio.Instance.audioclip[1]);
                        }
                    
                }

                if (hit.collider.gameObject.tag == "plate")
                {
                    BoardController.Instance.MovePiece(hit.collider.gameObject.transform);

                    //置くSE
                    Audio.Instance.audiosource.PlayOneShot(Audio.Instance.audioclip[1]);
                }

                if (hit.collider.gameObject.tag == "Arrow")
                {
                    RotateDirection rd = hit.collider.gameObject.GetComponent<RotateArrow>().direction;

                    //回転SE
                    Audio.Instance.audiosource.PlayOneShot(Audio.Instance.audioclip[0]);
                    //BoardController.Instance.Rotate(rd);
                    GameManager.Instance.UseRotate(rd);
                }
            }

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                UIManager.Instance.ArrowAppear(true);
            }
            else
            {
                UIManager.Instance.ArrowAppear(false);
            }
        }

        
    }
}
