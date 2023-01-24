using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public void changerFormationIsPressed(){
        
        bool n = gameObject.transform.GetChild(1).gameObject.activeSelf;
        gameObject.transform.GetChild(1).gameObject.SetActive(!n);

    }

    public void SquareFormationIsPressed(){
        UnitClick.setFormation( new SquareFormation());

    }

    public void LineFormationIsPressed(){
        UnitClick.setFormation(new LineFormation());
    }

    public void TriangleFormationIsPressed(){
        UnitClick.setFormation(new TriangleFormation());
    }

}
