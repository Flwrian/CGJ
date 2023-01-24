using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucher : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private float timeBtwSpeedBoost;
    private float timeForSpeedDuration;
    [SerializeField]    
    private float btwSpeedDelay;
    [SerializeField]
    private float speedDelay;
    private bool isSpeeded;
    private float oldSpeed;
    // Start is called before the first frame update
    void Start()
    {
        oldSpeed = GetComponent<Unit>().getAttackSpeed();
        isSpeeded = false;
        timeBtwSpeedBoost = btwSpeedDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSpeeded){
            if(timeForSpeedDuration <= 0){
                GetComponent<Unit>().setAttackSpeed(oldSpeed);
                isSpeeded = false;
            }else{
                timeForSpeedDuration -= Time.deltaTime;
            }
        }
        else{

        
            if(timeBtwSpeedBoost <= 0){
                oldSpeed = GetComponent<Unit>().getAttackSpeed();
                GetComponent<Unit>().setAttackSpeed(speed);
                isSpeeded = true;
                timeBtwSpeedBoost = btwSpeedDelay;
                timeForSpeedDuration = speedDelay;
            }else{
                timeBtwSpeedBoost -= Time.deltaTime;
            }
        }
    }
}
