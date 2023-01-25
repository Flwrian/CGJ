using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkUnit : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> liste = new List<GameObject>();
    // Start is called before the first frame update
    private static LinkUnit _instance;

    public static LinkUnit Instance {
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
   
    public List<GameObject> getAll(){
        return liste;
    }

    public void addUnit(GameObject g){
        liste.Add(g);
    }
}
