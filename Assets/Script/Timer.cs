using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private int seconds;
    public  TMPro.TextMeshProUGUI texte;

    private float time;

    public GameObject wrap1;

    public GameObject wrap2;

    private int wrapTime;
    private static Timer _instance;

    public static Timer Instance {
        get {
            return _instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        texte =gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        wrapTime = 0;
        time = 1;
        seconds = 30;
        texte.text = " 0 : 30";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(time <= 0){
            seconds--;
            time = 1;
        }
        else{
            time -= Time.deltaTime;
        }
        texte.text = seconds / 60 + " : " + seconds % 60;
        if(seconds == 0){
            if(wrapTime == 0){
                        int squareWidth = Mathf.CeilToInt(Mathf.Sqrt(UnitSelection.Instance.unitsSelected.Count));
                    Debug.Log(squareWidth);
                    float a = 2.0f;
                    float b = 1.0f;
                    int i = 0;
                    int j = 0;
                wrapTime++;
                Camera.main.transform.position = new Vector3(wrap1.transform.position.x,wrap1.transform.position.y,-10);
                List<GameObject> l = LinkUnit.Instance.getAll();
                foreach(var unit in l){
                    unit.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                    unit.gameObject.GetComponent<Unit>().GetHealed(100000);
                    unit.gameObject.transform.position = wrap1.transform.position;
                    unit.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;

                }
            }
            else if(wrapTime == 1){
                        int squareWidth = Mathf.CeilToInt(Mathf.Sqrt(UnitSelection.Instance.unitsSelected.Count));
                    Debug.Log(squareWidth);
                    float a = 2.0f;
                    float b = 1.0f;
                    int i = 0;
                    int j = 0;
                wrapTime++;
                Camera.main.transform.position = new Vector3(wrap2.transform.position.x,wrap2.transform.position.y,-10);
                List<GameObject> l = LinkUnit.Instance.getAll();
                foreach(var unit in l){
                    unit.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

                    unit.gameObject.GetComponent<Unit>().GetHealed(100000);
                    unit.gameObject.transform.position = new Vector3(wrap2.transform.position.x + (a * i)-b,wrap2.transform.position.y - (a * j)+b,unit.transform.position.z);
                    unit.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;

                }
                wrapTime++;
            }
            if(wrapTime == 2){
                
            }
            seconds = 30;
            
        }
    }

    public void addTime(int sec)
    {
        seconds += sec;

    }
}
