using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [SerializeField] MoviePlay[] winMovies;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GoToTitleSequence());
        if (GameInfo.winNum[0] > GameInfo.winNum[1]) {
            winMovies[0].gameObject.SetActive(true);
        }
        else
        {
            winMovies[1].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    IEnumerator GoToTitleSequence()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("TitleScene");
    }
}
