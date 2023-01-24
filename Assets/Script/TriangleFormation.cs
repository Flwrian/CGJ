using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleFormation : UnitFormation
{

    override
    public void formation(Vector2 mousePos,bool attacking){
        int squareWidth = 1;
        Debug.Log(squareWidth);
        float a = 2.0f;
        float b = 1.0f;
        int i = 0;
        int j = 0;
        foreach(var unit in UnitSelection.Instance.unitsSelected){

            unit.GetComponent<Unit>().SetDestination(new Vector3(mousePos.x + (a * i)-b,mousePos.y - (a * j)+b,unit.transform.position.z),unit.GetComponent<Unit>().getRange());
            i++;
            if(i == squareWidth){
                j++;
                i = -squareWidth;
                squareWidth++;
            }
        }
    }


}
