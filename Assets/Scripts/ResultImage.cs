using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultImage : MonoBehaviour
{
    private Image image;
    public Sprite[] Sprite;
    // Start is called before the first frame update
    void Start()
    {
        image = this.GetComponent<Image>();
        if (this.gameObject.layer == 8) {
            image.sprite = Sprite[GameInfo.winNum[0]];
        }
        else if(this.gameObject.layer==9)
        {
            image.sprite = Sprite[GameInfo.winNum[1]];
        }
        else {
            if (GameInfo.winNum[0] < GameInfo.winNum[1])
            {
                image.sprite = Sprite[0];
            }
            else
            {
                image.sprite = Sprite[1];
            }

        }





    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
