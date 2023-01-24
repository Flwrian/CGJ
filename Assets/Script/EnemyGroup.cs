using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> liste=new List<GameObject>();

    [SerializeField]
    public List<GameObject> points=new List<GameObject>();

    private int indexPoint;

    float time;

    private GameObject targetDetected;
    private Boolean hasAlreadyDetected;

    // Start is called before the first frame update
    void Start()
    {
        targetDetected = null;
        hasAlreadyDetected = false;
        indexPoint= 0;
        formation();
        time = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetDetected == null)
        {
            foreach (GameObject g in liste)
            {
                GameObject t = g.GetComponent<Unit>().getTarget();
                if(t != null)
                {
                    targetDetected= t;
                }
            }
            if (targetDetected == null)
            {
                if (time <= 0)
                {
                    walkToPoint();
                    indexPoint++;
                    if(indexPoint==points.Count)
                    {
                        indexPoint = 0;
                    }
                    
                }
                else
                {
                    time -= Time.deltaTime;
                }
            }
        }
        if (targetDetected != null && !hasAlreadyDetected)
        {
            foreach(GameObject g in liste)
            {
                g.GetComponent<Unit>().SetNewTarget(targetDetected);
            }
            hasAlreadyDetected = true;
        }
    }

    void walkToPoint()
    {
        GameObject pointed = points[indexPoint];
        Transform pt=pointed.transform;
        foreach(GameObject g in liste)
        {
            g.GetComponent<Unit>().SetDestination(pt.position, 0);
        }
        formation();
        time = 5;
    }

    public void formation()
    {
        GameObject pointed = points[indexPoint];
        Transform pt = pointed.transform;
        int squareWidth = Mathf.CeilToInt(Mathf.Sqrt(liste.Count));
        Debug.Log(squareWidth);
        float a = 2.0f;
        float b = 1.0f;
        int i = 0;
        int j = 0;
        foreach (var unit in liste)
        {
            unit.GetComponent<Unit>().SetDestination(new Vector3(pt.position.x + (a * i) - b, pt.position.y - (a * j) + b, unit.transform.position.z), 0);
            i++;
            if (i == squareWidth)
            {
                j++;
                i = 0;
            }
        }
    }
}
