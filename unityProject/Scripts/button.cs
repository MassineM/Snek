using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class button : MonoBehaviour
{
    public int scene=-1;
    SpriteRenderer spriteRen;
    public Sprite Ren0;
    public Sprite Ren1;
    bool isSet;

    public bool isBtnSet(){
        return isSet;
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRen=this.GetComponent<SpriteRenderer>();
	    spriteRen.sprite = Ren0;
        isSet=false;
    }

    void OnMouseUp(){
        isSet=true;
    }

    void OnMouseEnter(){
	    spriteRen.sprite = Ren1;
    }

    void OnMouseExit() {
	    spriteRen.sprite = Ren0;
    }
    // Update is called once per frame
    void Update()
    {
        if (isSet){
            if (scene==-1){}
            else if(scene==0)
                SceneManager.LoadScene(0);
            else if(scene==-2) 
                Application.LoadLevel(Application.loadedLevel);
            else
                Application.LoadLevel(scene);
        }
    }
}
