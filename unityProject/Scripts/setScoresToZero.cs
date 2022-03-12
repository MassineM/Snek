using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class setScoresToZero : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<=7;i++)
        PlayerPrefs.SetInt("Highscore"+(i), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
