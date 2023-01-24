using System.Collections.Generic;
using UnityEngine;

public class UnitClick : MonoBehaviour
{
    private Camera myCam;

    public LayerMask clickable;
    public LayerMask ground;
    public LayerMask ennemy;
    private static UnitFormation form;

    public bool line = true;
    public bool square = false;
    public bool triangle = false;
    // Start is called before the first frame update
    void Start()
    {
        form = gameObject.AddComponent<LineFormation>() as LineFormation;
        myCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenu.isPaused){

            if(Input.GetMouseButtonDown(0)){
                //
                Vector2 mousePos = myCam.ScreenToWorldPoint(Input.mousePosition);
                Collider2D hit = null;
                hit = Physics2D.OverlapCircle(new Vector2(mousePos.x,mousePos.y),(float)0.2f,clickable);

                if(hit != null){
                    
                    
                    if(Input.GetKey(KeyCode.LeftShift)){
                        UnitSelection.Instance.ShiftClickSelect(hit.gameObject);
                    }
                    else{
                        UnitSelection.Instance.ClickSelect(hit.gameObject);
                    }
                    
                }
                else{
                    if(!Input.GetKey(KeyCode.LeftShift)){
                        UnitSelection.Instance.DeselectAll();
                    }
                }
            }
            if(Input.GetMouseButtonDown(1)){
                Vector2 mousePos = myCam.ScreenToWorldPoint(Input.mousePosition);
                Collider2D hit = null;
                hit = Physics2D.OverlapCircle(new Vector2(mousePos.x,mousePos.y),0.5f,ground);
                if(hit != null){
                    bool attack = false;
                    hit = null;
                    hit = Physics2D.OverlapCircle(new Vector2(mousePos.x,mousePos.y),0.2f,ennemy);

                    if(hit != null){
                        foreach(var unit in UnitSelection.Instance.unitsSelected){
                            unit.gameObject.GetComponent<Unit>().SetNewTarget(hit.gameObject);
                            attack = true;
                        }
                    }
                    else{
                        foreach(var unit in UnitSelection.Instance.unitsSelected){
                            unit.gameObject.GetComponent<Unit>().SetNewTarget(null);
                        }
                        form.formation(mousePos,attack);

                    }
                }

                
    
            }
            

        }
    }
    public static void setFormation(UnitFormation unit){
        form = unit;
    }

}
