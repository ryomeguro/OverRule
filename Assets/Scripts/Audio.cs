using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio Instance;
    public AudioClip[] audioclip;
    public AudioSource audiosource;
    
    void Start()
    {
        Audio.Instance.audiosource=this.gameObject.GetComponent<AudioSource>();
        //audiosource.PlayOneShot(audioclip[0]);
        //Audio.Instance.audiosource.PlayOneShot(Audio.Instance.audioclip[0]);
    }

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
