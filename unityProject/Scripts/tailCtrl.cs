using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tailCtrl : MonoBehaviour
{
    public Vector3 position;
    public Vector3 movement;
    public playerControl playermov;
    public snekOrg SNEK;
    float rotation=-90f;
    float rotVar =0;

    public Vector3 getTailPos(){
        return new Vector3(position.x+mapGen.sizeX/2,position.y+mapGen.sizeY/2,0);
    }

    public void setTailPos(Vector3 X){
        position = new Vector3(X.x-mapGen.sizeX/2,X.y-mapGen.sizeY/2,0);
        transform.position=position;
    }

    public Vector2 getTailRot(){
        return new Vector2(rotation,rotVar);
    }

    public void setTailRot(Vector2 X){
        rotation=X.x;
        rotVar=X.y;
    }

    Vector3 getTailNumMove(Vector3 X){
        return new Vector3((Mathf.Round(X.x-mapGen.sizeX/2-position.x)),(Mathf.Round(X.y-mapGen.sizeY/2-position.y)),0);
    }

    public void setTailMov(Vector3 X){
        movement = getTailNumMove(X)*playermov.getMoveSpeed()*Time.deltaTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((movement.x>0 && this.transform.position.x<=SNEK.getLastBod().x) || (movement.x<0 && this.transform.position.x>=SNEK.getLastBod().x) || (movement.y>0 && this.transform.position.y<=SNEK.getLastBod().y) || (movement.y<0 && this.transform.position.y>=SNEK.getLastBod().y))
            transform.position +=movement;
        if(rotVar==0)
            transform.eulerAngles=new Vector3(0,0,rotation);
        else
            transform.Rotate(new Vector3(0,0,rotVar * Time.deltaTime * playermov.getMoveSpeed()));
    }
}
