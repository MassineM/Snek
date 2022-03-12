using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class food : MonoBehaviour
{
    public bool eaten=false;
    public int foodX;
    public int foodY;
    public int mealsCount=0;
    public bool isFoodOn=false;
    public mapGen temp;
    public cell apple;
    public Text txt;
    public static int score;


    //public void setRand(){}

    public void putFood(){
        isFoodOn = true;
    }

    public int getFoodX(){
        return foodX;
    }

    public int getFoodY(){
        return foodY;
    }
    
    public bool isEaten(){
        return (eaten && isFoodOn);
    }

    public void eat(){
        temp.getCellAt(foodX,foodY).isFood(false);
        eaten=true;
        mealsCount++;
    }

    public int getMealsCount(){
        return mealsCount;
    }

    void putFoodHere(){}

    // Start is called before the first frame update
    void Start()
    {
        eaten=true;
        isFoodOn=true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (eaten && isFoodOn){
            foodX = Random.Range(0,mapGen.sizeX);
            foodY = Random.Range(0,mapGen.sizeY);
            apple = (cell) temp.getCellAt(foodX,foodY);
            while(apple.getObst() || apple.getSnek() || (temp.getPosXn()==foodX && temp.getPosYn()==foodY)){
                foodX = Random.Range(0,mapGen.sizeX);
                foodY = Random.Range(0,mapGen.sizeY);
                apple = (cell) temp.getCellAt(foodX,foodY);
            }
            this.transform.position = new Vector3(apple.getCellXn()-mapGen.sizeX/2,apple.getCellYn()-mapGen.sizeY/2,0);
            eaten=false;
        }

        ((cell) temp.getCellAt(foodX,foodY)).isFood(true);
        score = mealsCount * 10;
        txt.text = "Score=" + score;
    }
}
