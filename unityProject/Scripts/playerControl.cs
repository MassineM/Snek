using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public int moveSpeed=1;
    int currCellX=(int)mapGen.spawn.x;
    int currCellY=(int)mapGen.spawn.y;
    bool moveReq=false;
    public static bool alive;
    bool isRotating=false;
    Vector3 numericMove=new Vector3(1,0,0);
    Vector3 prevNumMove=new Vector3(1,0,0);
    float hRotation=0f;
    float realRot=-90f;
    Vector3 realMove=new Vector3();
    Vector3 cubeMove=new Vector3();
    Vector3 finalMove=new Vector3();
    public mapGen stdGrid;
    public currCell currCell;
    public food food;

    public int getMoveSpeed(){
        return moveSpeed;
    }
    
    public float getRotVar(){
        return hRotation;
    }

    public float getRealRot(){
        return realRot;
    }

    public Vector3 getNumMove(){
        return numericMove;
    }
    
    public Vector3 getCurrCell(){
        return new Vector3(currCellX,currCellY,0);
    }
    // Start is called before the first frame update
    

    void Start()
    {
        alive=true;
        numericMove = new Vector3(1, 0, 0);
        realMove= new Vector3( moveSpeed * Time.deltaTime,0,0);
        currCellX = stdGrid.getPosXn(); 
        currCellY = stdGrid.getPosYn();
        this.transform.position = new Vector3(currCellX-mapGen.sizeX/2,currCellY-mapGen.sizeY/2,0);
        prevNumMove=numericMove;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (moveReq & alive){
            if (Input.GetKeyDown("up") && numericMove.y != -1){
                prevNumMove=numericMove;
                numericMove = new Vector3(0, 1, 0);
                currCellX = stdGrid.getPosXn(); 
                currCellY = stdGrid.getPosYn();
                moveReq = false;
            }
            else if (Input.GetKeyDown("down") && numericMove.y != 1){
                prevNumMove=numericMove;
                numericMove = new Vector3(0, -1, 0);
                currCellX = stdGrid.getPosXn(); 
                currCellY = stdGrid.getPosYn();
                moveReq = false;
            }
            else if (Input.GetKeyDown("right") && numericMove.x != -1){
                prevNumMove=numericMove;
                numericMove = new Vector3(1, 0, 0);
                currCellX = stdGrid.getPosXn(); 
                currCellY = stdGrid.getPosYn();
                moveReq = false;
            }
            else if (Input.GetKeyDown("left") && numericMove.x != 1){
                prevNumMove=numericMove;
                numericMove = new Vector3(-1, 0, 0);
                currCellX = stdGrid.getPosXn(); 
                currCellY = stdGrid.getPosYn();
                moveReq = false;
            }
        }
        
        realMove= numericMove * moveSpeed * Time.deltaTime;

        if (stdGrid.getPosXn()!=currCellX || stdGrid.getPosYn()!=currCellY){
            currCellX = stdGrid.getPosXn(); 
            currCellY = stdGrid.getPosYn();
            this.transform.position = new Vector3(currCellX-mapGen.sizeX/2,currCellY-mapGen.sizeY/2,0) - numericMove;
            transform.eulerAngles=new Vector3(0,0,realRot);
            if(!moveReq){
                cubeMove=realMove;
                moveReq=true;
                // UnityEngine.Debug.Log("cellX="+stdGrid.getPosXf());
                // UnityEngine.Debug.Log("cellY="+stdGrid.getPosYf());
            }

            if(isRotating){
                isRotating=false;
                hRotation=0;
            }
            if(prevNumMove!=numericMove){
                if((prevNumMove.x==numericMove.y && numericMove.y!=0) || (prevNumMove.y==1 && numericMove.x==-1) || (prevNumMove.y==-1 && numericMove.x==1))
                    hRotation=90f;
                else
                    hRotation=-90f;
                realRot+=hRotation;
                isRotating=true;
                prevNumMove=numericMove;
            }

            food.putFood();
            if (currCell.getActObst() || currCell.getActSnek()){
                alive=false;
                realMove = new Vector3(0,0,0);
                cubeMove=realMove;
            }
            if (currCell.getActFood())
                food.eat();
        }
        
        if (alive)
            stdGrid.setPos(stdGrid.getPosXf()+realMove.x,stdGrid.getPosYf()+realMove.y);
            if (((cubeMove.x>0 && this.transform.position.x>stdGrid.getPosXn()-mapGen.sizeX/2) || (cubeMove.x<0 && this.transform.position.x<stdGrid.getPosXn()-mapGen.sizeX/2)) || ((cubeMove.y>0 && this.transform.position.y>stdGrid.getPosYn()-mapGen.sizeY/2) || (cubeMove.y<0 && this.transform.position.y<stdGrid.getPosYn()-mapGen.sizeY/2)))
                finalMove=new Vector3(0,0,0);
            else
                finalMove=cubeMove;
            this.transform.position += finalMove;
            transform.Rotate(new Vector3(0,0,hRotation * Time.deltaTime * moveSpeed));
    }
}
