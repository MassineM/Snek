using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level3Config : MonoBehaviour
{
    public mapGen gridGen;
    public GameObject cellGen;
    // Start is called before the first frame update
    void Start()
    {
        mapGen.sizeX=30;
        mapGen.sizeY=20;
        mapGen.spawn=new Vector2(5f,21f);

        for (int i=0; i < mapGen.sizeX; i++)
            for (int j=0; j < mapGen.sizeY; j++){
                GameObject tempCell=Instantiate(cellGen) as GameObject;
                gridGen.getMap().Add(tempCell.GetComponent<cell>());
            }
        
        for (int i=0; i < mapGen.sizeX; i++)
            for (int j=0; j < mapGen.sizeY; j++){
                if(i==0 || j==0 || i== mapGen.sizeX-1 || j== mapGen.sizeY-1 || (j>=6 && j<=13 && (i<=5 || i>=24 || (i>=12 && i<=17)))){
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
