using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snekOrg : MonoBehaviour
{
    public List<GameObject> snekParts = new List<GameObject>();
    public GameObject Head;
    public List<Vector3> partsPositions;
    public List<Vector2> partsRotations;
    public playerControl controller;
    public mapGen stdGrid;
    public GameObject Body0;
    public GameObject Body1;
    public GameObject Tail;
    public food food;
    public int nbBodies=2;
    int latestMealsCount=0;
    bool canSpawn=true;

    public Vector2 getLastBod(){
        return new Vector2(partsPositions[nbBodies].x-mapGen.spawn.x,partsPositions[nbBodies].y-mapGen.spawn.y);
    }

    // Start is called before the first frame update
    void Start()
    {
        snekParts.Add(Head);
        Vector3 pos0=new Vector3(mapGen.spawn.x,mapGen.spawn.y,0);
        partsPositions.Add(pos0);
        snekParts.Add(Body0);
        Vector3 pos1=new Vector3(mapGen.spawn.x-1f,mapGen.spawn.y,0);
        partsPositions.Add(pos1);
        snekParts.Add(Body1);
        Vector3 pos2=new Vector3(mapGen.spawn.x-1f,mapGen.spawn.y,0);
        partsPositions.Add(pos2);
        Body0.GetComponent<bodyCtrl>().setPartPos(partsPositions[1]);
        Body1.GetComponent<bodyCtrl>().setPartPos(partsPositions[2]);
        Vector3 pos3=new Vector3(mapGen.spawn.x-2f,mapGen.spawn.y,0);
        Tail.GetComponent<tailCtrl>().setTailPos(pos3);
        Tail.GetComponent<tailCtrl>().setTailMov(partsPositions[2]);
        partsRotations.Add(new Vector2(-90f,0f));
        partsRotations.Add(new Vector2(-90f,0f));
        partsRotations.Add(new Vector2(-90f,0f));
    }
    

    // Update is called once per frame
    void Update()
    {
        // snekParts[0]=controller.getCurrCell();
        // UnityEngine.Debug.Log(controller.getCurrCell().x);
        // snekParts[1]=bodyGen;
        // snekParts[2]=Tail;
        if(canSpawn){
            UnityEngine.Debug.Log("here");
            partsRotations[1]=new Vector2(controller.getRealRot(),controller.getRotVar());
            snekParts[1].GetComponent<bodyCtrl>().setPartRot(partsRotations[1]);
            if ((stdGrid.getPosXn()!=(controller.getCurrCell()).x || stdGrid.getPosYn()!=(controller.getCurrCell()).y)){
                UnityEngine.Debug.Log("here");
                if(food.getMealsCount()!=latestMealsCount){
                    GameObject tempBody=Instantiate(Body1) as GameObject;
                    nbBodies++;
                    bodyCtrl temp = tempBody.GetComponent<bodyCtrl>();
                    temp.setPartPos(partsPositions[nbBodies-1]);
                    temp.setPartRot(partsRotations[nbBodies-1]);
                    partsPositions.Add(temp.getPartPos());
                    partsRotations.Add(partsRotations[nbBodies-1]);
                    snekParts.Add(tempBody);
                    //Tail.GetComponent<tailCtrl>().setTailPos(partsPositions[nbBodies-1]);
                    Tail.transform.position=partsPositions[nbBodies];
                    Tail.GetComponent<tailCtrl>().setTailMov(new Vector3(0,0,0));
                    Tail.GetComponent<tailCtrl>().setTailRot(new Vector2(Tail.GetComponent<tailCtrl>().getTailRot().x,0f));
                    latestMealsCount++;
                }
                else{
                    Tail.GetComponent<tailCtrl>().setTailPos(partsPositions[nbBodies]);
                    Tail.GetComponent<tailCtrl>().setTailMov(partsPositions[nbBodies-1]);
                    Tail.GetComponent<tailCtrl>().setTailRot(partsRotations[nbBodies-1]);
                    UnityEngine.Debug.Log("here");
                    snekParts[nbBodies].GetComponent<bodyCtrl>().setPartPos(partsPositions[nbBodies-1]);
                    partsPositions[nbBodies]=partsPositions[nbBodies-1];
                    partsRotations[nbBodies]=partsRotations[nbBodies-1];
                    snekParts[nbBodies].GetComponent<bodyCtrl>().setPartRot(partsRotations[nbBodies-1]);
                }
                for(int i=nbBodies-2; i>=0;i--){
                    UnityEngine.Debug.Log("there");
                    snekParts[i+1].GetComponent<bodyCtrl>().setPartPos(partsPositions[i]);
                    partsPositions[i+1]=partsPositions[i];
                    if(i>0){
                        partsRotations[i+1]=partsRotations[i];
                        snekParts[i+1].GetComponent<bodyCtrl>().setPartRot(partsRotations[i]);
                    }
                }
                partsPositions[0]=new Vector3(stdGrid.getPosXn(),stdGrid.getPosYn(),0);
            }
        
            for(int i=0; i<mapGen.sizeX; i++)
                for(int j=0; j<mapGen.sizeY; j++)
                    ((cell) stdGrid.getCellAt(i,j)).isSnek(false);
            for (int i=1; i<=nbBodies;i++)
                ((cell) stdGrid.getCellAt((int)partsPositions[i].x,(int)partsPositions[i].y)).isSnek(true);
            ((cell) stdGrid.getCellAt((int)Tail.GetComponent<tailCtrl>().getTailPos().x,(int)Tail.GetComponent<tailCtrl>().getTailPos().y)).isSnek(true);
        }
        //if(level0Config.isLvlGen) canSpawn=true;
    }
}
