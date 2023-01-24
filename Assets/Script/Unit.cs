using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    private static KeyCode recrut = KeyCode.E; 
    
    private static KeyCode consum = KeyCode.A; 

    [Header("CombatSection")]
    private float maxLife;
    [SerializeField]
    private float lifePoint;
    [SerializeField]
    private float range;
    [SerializeField]
    private float attackDelay;
    private float timeBtwAttack;
    [SerializeField]   
    private float damage;
    [SerializeField]
    private float detectionRange;
    private GameObject target;
    private bool isAttacking;
    private NavMeshAgent agent;
    [SerializeField]
    private LayerMask ennemy;
    [SerializeField]//
    private int team;
    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        target = null;
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        isAttacking = false;
        UnitSelection.Instance.unitList.Add(this.gameObject);

    }

    // Update is called once per frame
    void OnDestroy()
    {
        UnitSelection.Instance.unitList.Remove(this.gameObject);
    }

    void Update()
    {
        if(lifePoint <= 0){
            if(gameObject.layer == 7){
                SpriteRenderer sp = GetComponent<SpriteRenderer>();
                sp.color = Color.gray;

                LayerMask human = LayerMask.GetMask("Clickable");
                if(Physics2D.OverlapCircle(transform.position,detectionRange,human)){
                    if(Input.GetKeyDown(recrut)){
                        ennemy = LayerMask.GetMask("ennemy");
                        gameObject.layer = 3;
                        lifePoint = 100;
                        sp.color = Color.red;

                    }
                    else if(Input.GetKeyDown(consum)){
                        //commande pour boost le temps
                        // anim pour le squelette 
                        Destroy(this.gameObject);
                    }
                }
            }
        }
        else{
            if(pathComplete()){
                isMoving = false;
            }
    
            if(target != null){
                
                if(CheckDestroy.IsNullOrDestroyed(target) || target.GetComponent<Unit>().getLifePoint() <= 0){
                    target = null;
                }
                else if(Vector2.Distance(gameObject.transform.position,target.GetComponent<Transform>().position) <= range && !isAttacking){
                    isAttacking = true;
                }
                else{
                    isAttacking = false;
                }
                
                if(isAttacking && timeBtwAttack <= 0){
                    AttackEnnemy();
                    timeBtwAttack = attackDelay;
                }else{
                    timeBtwAttack -= Time.deltaTime;
                }

            }
            else{
                isAttacking = false;
                if(!isMoving){
                    Collider2D hit = Physics2D.OverlapCircle(transform.position,detectionRange,ennemy);
                    if(hit != null){
                        SetNewTarget(hit.gameObject);
                    }
                }

            }
        }  

    }

    public bool pathComplete()
    {
        if(agent.remainingDistance > 0.1f) {
            return false;
        }
        return true;
     }

    private void AttackEnnemy(){

        if(target != null){
            target.GetComponent<Unit>().TakeDamage(damage);
        }        

    }


    public float getRange(){
        return range;
    }


    public GameObject getTarget(){
        return target;
    }

    public float getLifePoint(){
        return lifePoint;
    }

    public void SetDestination(Vector3 dest,float stopingDistance){
        isMoving = true;
        agent.stoppingDistance = stopingDistance;
        Debug.Log("set");
        agent.SetDestination(dest);
    }

    public void SetNewTarget(GameObject targ){
        isAttacking = false;
        if(targ != null){
            this.SetDestination(targ.transform.position,range);

            Debug.Log("UNIT TARGET");
        }

        target = targ;
    }

    public void TakeDamage(float damage){
        Debug.Log("UNit damaged");
        lifePoint -= damage;
    }

    public void GetHealed(float heal){
        if(lifePoint + heal >= maxLife){
            lifePoint = maxLife;
        }else{
            lifePoint += heal;
        }
    }

    public void SetTeam(int team){
        this.team = team;
    }
}
