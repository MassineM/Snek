using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class mapGen : MonoBehaviour
{

    public static int sizeX ;
    public static int sizeY ;
    public static Vector2 spawn;
    public float floatPosX;
    public float floatPosY;
    public static bool mapGenerated=false;
    public ArrayList map = new ArrayList();


    public cell getCellAt(int i, int j){
        return (cell) getMap()[i*sizeY+j];
    }

    public void setPos(float x,float y){
        if(0<=x && x<=sizeX && 0<=y && y<=sizeY){
        this.floatPosX=x;
        this.floatPosY=y;
        }
    }

    public float getPosXf(){
        return this.floatPosX;
    }
    
    public float getPosYf(){
        return this.floatPosY;
    }

    public int getPosXn(){
        return (int)this.floatPosX;
    }
    
    public int getPosYn(){
        return (int)this.floatPosY;
    }

    public ArrayList getMap(){
        return map;
    }

    



    // Start is called before the first frame update
    void Start()
    {
        mapGenerated=true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
