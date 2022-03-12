using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyCtrl : MonoBehaviour
{
    Vector3 position;
    float rotation=-90f;
    float rotVar =0;
    bool isAngle=false;
    public SpriteRenderer spriteRen;
    public Sprite straightSprite;
    public Sprite angleSprite;
    public mapGen stdGrid;


    public void setAngle(bool b){
        isAngle=b;
    }

    public void setPartPos(Vector3 X){
        position.x = X.x-mapGen.sizeX/2;
        position.y = X.y-mapGen.sizeY/2;
        position.z=0f;
    }

    public void setPartRot(Vector2 X){
        rotation=X.x;
        rotVar=X.y;
    }

    public Vector3 getPartPos(){
        return new Vector3(position.x+mapGen.sizeX/2,position.y+mapGen.sizeY/2,0);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //UnityEngine.Debug.Log("here");
        transform.position=position;
        if (rotVar!=0f){
            spriteRen.sprite = angleSprite;
            if(rotVar==90f) transform.eulerAngles=new Vector3(0,0,rotation+rotVar);
            if(rotVar==-90f) transform.eulerAngles=new Vector3(0,0,rotation);
        }
        else{
            transform.eulerAngles=new Vector3(0,0,rotation);
            spriteRen.sprite = straightSprite;
        }
    }
}
