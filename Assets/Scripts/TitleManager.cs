using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject[] HowToPanels;

    private int currentPages;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextPages()
    {
        HowToPanels[currentPages++].SetActive(false);
        HowToPanels[currentPages].SetActive(true);
    }

    public void PrevPages()
    {
        HowToPanels[currentPages--].SetActive(false);
        HowToPanels[currentPages].SetActive(true);
    }

    public void HowToToggle(bool isActive)
    {
        if (isActive)
        {
            currentPages = 0;
            HowToPanels[0].SetActive(true);
        }
        else
        {
            HowToPanels[currentPages].SetActive(false);
        }
    }

    public void GoToGameScene()
    {
        GameInfo.Init();
        SceneManager.LoadScene("GameScene");
    }
}
