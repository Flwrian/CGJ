using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    
    private static KeyCode leftMove = KeyCode.Q;
    private static KeyCode rightMove = KeyCode.D;
    private static KeyCode upMove = KeyCode.Z;
    private static KeyCode downMove = KeyCode.S;

    [SerializeField]
    public float speed = 25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PanCamera();
    }

    private void PanCamera(){
        if(Input.GetKey(leftMove)){
            cam.transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey(rightMove)){
            cam.transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey(upMove)){
            cam.transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey(downMove)){
            cam.transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
        }


        // Zoom in
        if(Input.GetAxis("Mouse ScrollWheel") > 0f){
            cam.orthographicSize -= 0.5f;
        }
        // Zoom out
        if(Input.GetAxis("Mouse ScrollWheel") < 0f){
            cam.orthographicSize += 0.5f;
        }



        // Limit zoom
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 3f, 8f);
    }
}
