using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenu : MonoBehaviour
{
    public bloc bloc;
    public GameObject contBox;
    public GameObject survBox;
    public GameObject advBox;
    public GameObject lvlsBox;
    public GameObject diffBox;
    public GameObject back;

    // Start is called before the first frame update
    void Start(){
        bloc.setBloc(false);
        contBox.SetActive(true);
        survBox.SetActive(false);
        advBox.SetActive(false);
        lvlsBox.SetActive(false);
        diffBox.SetActive(false);
    }

    

    // Update is called once per frame
    void Update()
    {
        if(contBox.GetComponent<button>().isBtnSet()){
            bloc.setBloc(true);
            contBox.SetActive(false);
            survBox.SetActive(true);
            advBox.SetActive(true);
            back.SetActive(true);
        }
        if(advBox.GetComponent<button>().isBtnSet()){
            survBox.SetActive(false);
            advBox.SetActive(false);
            lvlsBox.SetActive(true);
        }
        if(survBox.GetComponent<button>().isBtnSet()){
            survBox.SetActive(false);
            advBox.SetActive(false);
            diffBox.SetActive(true);
        }
        if(back.GetComponent<button>().isBtnSet()){
            bloc.setBloc(false);
            contBox.SetActive(true);
            survBox.SetActive(false);
            advBox.SetActive(false);
            lvlsBox.SetActive(false);
            diffBox.SetActive(false);
            back.SetActive(false);
        }
    }
}
