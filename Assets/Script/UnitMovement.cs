using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    public Vector3 movePosition;
    public float speed;
    


    public void setMovePosition(Vector3 move){
        movePosition = move;
    }
    private void Awake(){
        movePosition = transform.position;
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, movePosition, step);
        if(transform.position.Equals(movePosition)){
            this.GetComponent<UnitMovement>().enabled = false;
        }
    }


}
