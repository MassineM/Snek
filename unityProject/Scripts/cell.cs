using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cell : MonoBehaviour
{
    public bool isObstacle;
    public bool isCellFood;
    public bool isCellSnek;
    public int mapX;
    public int mapY;
    public SpriteRenderer spriteRen;
    public Sprite obstSprite;
    public Vector2 coords = new Vector2();
    // Start is called before the first frame update



    public void mapping(int i,int j){
        this.mapX=i;
        this.mapY=j;
    }

    public bool getObst(){
        return this.isObstacle;
    }

    public void isObst(bool b){
        this.isObstacle=b;
    }

    public bool getFood(){
        return this.isCellFood;
    }

    public void isFood(bool b){
        this.isCellFood=b;
    }

    public bool getSnek(){
        return this.isCellSnek;
    }

    public void isSnek(bool b){
        this.isCellSnek=b;
    }

    public void setCoords(float i,float j){
        this.coords=new Vector2(i,j);
    }

    public float getCellXf(){
        return coords.x;
    }

    public float getCellYf(){
        return coords.y;
    }

    public int getCellXn(){
        return mapX;
    }
    
    public int getCellYn(){
        return mapY;
    }

    // public int getCellVal(ArrayList A,int i){
    //     if (A[i]!=null) {
    //     GameObject go = Instantiate(A[i]) as GameObject;
    //     cell x= go.GetComponent<cell>();
    //     //cell x= A[i].GetComponent<cell>();
    //     return x.getVal();
    //     }
    //     return 0;
    // }


    void Start()
    {
        transform.position=coords;
        spriteRen = this.GetComponent<SpriteRenderer>();
        isCellFood=false;
        isCellSnek=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (getObst()){
            spriteRen.sprite = obstSprite;
            spriteRen.sortingOrder = 3;
        }
        //else
            //spriteRen.color = new Color (0, 1, 0, 1);
    }
}
