using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareFormation : UnitFormation
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    override
    public void formation(Vector2 mousePos,bool attacking){
        int squareWidth = Mathf.CeilToInt(Mathf.Sqrt(UnitSelection.Instance.unitsSelected.Count));
        Debug.Log(squareWidth);
        float a = 2.0f;
        float b = 1.0f;
        int i = 0;
        int j = 0;
        foreach(var unit in UnitSelection.Instance.unitsSelected){

            if(attacking){
                unit.GetComponent<Unit>().SetDestination(new Vector3(mousePos.x + (a * i)-b,mousePos.y - (a * j)+b,unit.transform.position.z),unit.GetComponent<Unit>().getRange());
            }
            else{
                unit.GetComponent<Unit>().SetDestination(new Vector3(mousePos.x + (a * i)-b,mousePos.y - (a * j)+b,unit.transform.position.z),0);
            }
            i++;
            if(i == squareWidth){
                j++;
                i = 0;
            }
        }
    }

}
