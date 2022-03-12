using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currCell : MonoBehaviour
{
    public GameObject stdGrid;
    public cell actual;
    public mapGen temp;
    public playerControl controller;

    public void setCell(int i,int j){
        if(playerControl.alive && 0<=i && i<=mapGen.sizeX && 0<=j && j<=mapGen.sizeY){
            actual = temp.getCellAt(i,j);
        }
    }

    public bool getActObst(){
        return actual.getObst();
    }

    public bool getActSnek(){
        return actual.getSnek();
    }

    public bool getActFood(){
        return actual.getFood();
    }

    public cell getActual(){
        return actual;
    }
    // Start is called before the first frame update
    void Start()
    {
        temp = stdGrid.GetComponent<mapGen>();
        setCell((int)mapGen.spawn.x,(int)mapGen.spawn.y);
    }

    // Update is called once per frame
    void Update()
    {
        setCell(temp.getPosXn(),temp.getPosYn());
        transform.position = new Vector3(temp.getPosXn()-mapGen.sizeX/2,temp.getPosYn()-mapGen.sizeY/2,0);
        transform.eulerAngles = new Vector3(0,0,controller.getRealRot());
    }
}
