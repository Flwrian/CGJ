using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class Unit : MonoBehaviour
{

    private static KeyCode recrut = KeyCode.E; 
    
    private static KeyCode consum = KeyCode.A; 

    [Header("CombatSection")]
    [SerializeField]
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

    /* ------------ Fog of War -------------- */
    public Tilemap fogOfWar;

    public Tile fogTile;
    public int appearRange = 10;
    public int disappearRange = 4;

    private List<Vector2Int> visited = new List<Vector2Int>();

    /* ------------ Fog of War -------------- */


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

        updateFogOfWar();

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

    public void setAttackSpeed(float speed){
        attackDelay = speed;
    }
    
    public float getAttackSpeed(){
        return attackDelay;
    }


    public void setDamage(float damage){
        this.damage = damage;
    }

    public float getDamage(){
        return damage;
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

    public void updateFogOfWar(){
        if(gameObject.layer != 3){
            return;
        }
        // Create a range around the player that create the fog of war
        Vector3Int pos = fogOfWar.WorldToCell(transform.position);
        for(int x = -appearRange; x <= appearRange; x++){
            for(int y = -appearRange; y <= appearRange; y++){

                if(appearRange <= disappearRange){
                    continue;
                }


                Vector3Int newPos = new Vector3Int(pos.x + x, pos.y + y, pos.z);
                Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(newPos.x, newPos.y), 0.5f);
                foreach(Collider2D collider in colliders){
                    if(collider.gameObject.layer == 7){
                        Color col = collider.gameObject.GetComponent<SpriteRenderer>().color;
                        col.a = 0.0f;
                        collider.gameObject.GetComponent<SpriteRenderer>().color = col;
                    }
                }


                if(visited.Contains(new Vector2Int(newPos.x, newPos.y))){
                    Tile tile = new Tile();
                    tile.sprite = fogTile.sprite;
                    tile.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                    fogOfWar.SetTile(newPos, tile);
                }
                if(fogOfWar.GetTile(newPos) == null){
                    fogOfWar.SetTile(newPos, fogTile);
                }

            }
        }

        // Create a range around the player that destroy the fog of war
        for(int x = -disappearRange; x <= disappearRange; x++){
            for(int y = -disappearRange; y <= disappearRange; y++){

                Vector3Int newPos = new Vector3Int(pos.x + x, pos.y + y, pos.z);
                Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(newPos.x, newPos.y), 0.5f);
                foreach(Collider2D collider in colliders){
                    if(collider.gameObject.layer == 7){
                        Color col = collider.gameObject.GetComponent<SpriteRenderer>().color;
                        col.a = 1.0f;
                        collider.gameObject.GetComponent<SpriteRenderer>().color = col;
                    }
                }

                if(fogOfWar.GetTile(newPos) != null){
                    if(!visited.Contains(new Vector2Int(newPos.x, newPos.y))){
                        visited.Add(new Vector2Int(newPos.x, newPos.y));
                    }
                    fogOfWar.SetTile(newPos, null);
                }
            }
        }
    }
}
