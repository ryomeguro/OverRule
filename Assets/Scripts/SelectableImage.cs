using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableImage : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;

    private Image image;
    
    void Awake()
    {
        image = GetComponent<Image>();
    }
    
    public void SetImage(int index)
    {
        if (index < sprites.Length)
        {
            image.sprite = sprites[index];
        }
    }
}
