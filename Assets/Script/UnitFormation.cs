using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitFormation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void formation(Vector2 mousePos,bool attack);
}
