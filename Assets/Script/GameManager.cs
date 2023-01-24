using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private List<GameObject> game = new List<GameObject>(); 
    void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }

    public void addUnit(GameObject toAdd){
        game.Add(toAdd);
    }

    public void placeAllUnit(GameObject position){

    }
}
