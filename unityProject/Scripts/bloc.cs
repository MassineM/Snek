using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloc : MonoBehaviour
{
    public mainMenu mainMenu;
    SpriteRenderer spriteRen;
    public Sprite Ren0;
    public Sprite Ren1;
    bool isSet;

    public void setBloc(bool b){
        isSet=b;
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRen=this.GetComponent<SpriteRenderer>();
	    spriteRen.sprite = Ren0;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSet) spriteRen.sprite=Ren1;
    }
}
