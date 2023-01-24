using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    private static UnitSelection _instance;

    public static UnitSelection Instance {
        get {
            return _instance;
        }
    }
    // Start is called before the first frame updatedsq
    void Awake()
    {
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        }
        else{
            _instance = this;
        }
    }

    public void ClickSelect(GameObject unitToAdd){
        DeselectAll();
        unitsSelected.Add(unitToAdd);
        unitToAdd.transform.GetChild(0).gameObject.SetActive(true);

        unitToAdd.GetComponent<UnitMovement>().enabled = true;
    }

    public void ShiftClickSelect(GameObject unitToAdd){
        if(!unitsSelected.Contains(unitToAdd)){
            unitsSelected.Add(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);

        }
        else{
            unitsSelected.Remove(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(false);

        }
    }
    public void DragSelect(GameObject unitToAdd){
        if(!unitsSelected.Contains(unitToAdd)){
            unitsSelected.Add(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);

        }
    }

    public void DeselectAll(){
        foreach(var unit in unitsSelected){
            unit.transform.GetChild(0).gameObject.SetActive(false);

        }
        unitsSelected.Clear();
    }

    public void Deselect(GameObject unitToDeselect){
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
