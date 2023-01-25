using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private int seconds;
    public Text texte;
    private static Timer instance;

    public static Timer getInstance()
    {
        if (instance == null)
        {
            instance = new Timer();
        }
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        seconds = 30;
        texte.text = " 0 : 30";
    }

    // Update is called once per frame
    void Update()
    {
        seconds--;
        texte.text = seconds / 60 + " : " + seconds % 60;
    }

    public void addTime(int sec)
    {
        seconds+=sec;
    }
}
