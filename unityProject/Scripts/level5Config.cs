using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level5Config : MonoBehaviour
{
    public mapGen gridGen;
    public GameObject cellGen;
    // Start is called before the first frame update
    void Start()
    {
        mapGen.sizeX=33;
        mapGen.sizeY=33;
        mapGen.spawn=new Vector2(3f,13f);

        for (int i=0; i < mapGen.sizeX; i++)
            for (int j=0; j < mapGen.sizeY; j++){
                GameObject tempCell=Instantiate(cellGen) as GameObject;
                gridGen.getMap().Add(tempCell.GetComponent<cell>());
            }
        
        for (int i=0; i < mapGen.sizeX; i++)
            for (int j=0; j < mapGen.sizeY; j++){
                if(i==0 || j==0 || i== mapGen.sizeX-1 || j== mapGen.sizeY-1 || (j>=8 && j<=24 && i==16) || (i<=24 && i>=8 && j==16) || ((i==11 || i==21) && (j<=8 || j>=24)) || ((j==11 || j==21) && (i<=8 || i>=24))){
                    gridGen.getCellAt(i,j).isObst(true);
                }
                else{
                    
                    gridGen.getCellAt(i,j).isObst(false);
                }
                gridGen.getCellAt(i,j).mapping(i,j);
                gridGen.getCellAt(i,j).setCoords(i-mapGen.sizeX/2,j-mapGen.sizeY/2);
            }
        gridGen.setPos(mapGen.spawn.x,mapGen.spawn.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
