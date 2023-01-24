using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField]
    private float attack;
    private float timeBtwAttackBoost;
    private float timeForAttackDuration;
    [SerializeField]    
    private float btwAttackDelay;
    [SerializeField]
    private float AttackDelay;
    private bool isBoosted;
    private float oldAttack;
    // Start is called before the first frame update
    void Start()
    {
        isBoosted = false;
        timeBtwAttackBoost = btwAttackDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(isBoosted){
            if(timeForAttackDuration <= 0){
                GetComponent<Unit>().setDamage(oldAttack);
                isBoosted = false;
            }else{
                timeForAttackDuration -= Time.deltaTime;
            }
        }
        else{

        
            if(timeBtwAttackBoost <= 0){
                oldAttack = GetComponent<Unit>().getDamage();
                GetComponent<Unit>().setDamage(attack);
                isBoosted = true;
                timeBtwAttackBoost = btwAttackDelay;
                timeForAttackDuration = AttackDelay;
            }else{
                timeBtwAttackBoost -= Time.deltaTime;
            }
        }
    }
}
