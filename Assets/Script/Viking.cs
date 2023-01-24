using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking : MonoBehaviour
{
    [SerializeField]
    private float autoHeal;
    private float timeBtwHeal;
    [SerializeField]    
    private float healDelay;

    // Start is called before the first frame update
    void Start()
    {
        timeBtwHeal = healDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwHeal <= 0){
            GetComponent<Unit>().GetHealed(autoHeal);
            timeBtwHeal = healDelay;
        }else{
            timeBtwHeal -= Time.deltaTime;
        }
    }
}
