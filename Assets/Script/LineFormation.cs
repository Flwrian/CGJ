using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFormation : UnitFormation
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
        float a = 2.0f;
        float b = 0.0f;
        int i = (-UnitSelection.Instance.unitsSelected.Count/2) ;
        if(UnitSelection.Instance.unitsSelected.Count % 2 == 0){
            b = 1.0f;
            i = (-UnitSelection.Instance.unitsSelected.Count/2)+1 ;
        }    
        foreach(var unit in UnitSelection.Instance.unitsSelected){

            if(attacking){
                unit.GetComponent<Unit>().SetDestination(new Vector3(mousePos.x + (a * i)-(float)b,mousePos.y,unit.transform.position.z),unit.GetComponent<Unit>().getRange());
            }
            else{
                unit.GetComponent<Unit>().SetDestination(new Vector3(mousePos.x + (a * i)-(float)b,mousePos.y,unit.transform.position.z),0);

            }

            i++;
        }
    }
}
