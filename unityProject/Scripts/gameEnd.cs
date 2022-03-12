using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameEnd : MonoBehaviour
{
    public GameObject endBloc;
    public Text txt;
    public Text txt2;
    string highScore;

    // Start is called before the first frame update
    void Start()
    {
        highScore="Highscore"+(SceneManager.GetActiveScene().buildIndex-1);
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerControl.alive){
            endBloc.SetActive(true);
            txt.text = "Score=\n" + food.score;
            txt2.text = "Highscore=\n" + PlayerPrefs.GetInt(highScore);
            if(PlayerPrefs.GetInt(highScore) < food.score)
                PlayerPrefs.SetInt(highScore, food.score);
        }
    }
}
