using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fps : MonoBehaviour
{

    public float speed, sens, jumpForce;
    float rotY;
    public Transform cam;
    bool onGround = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.W)) 
            transform.Translate(0,0,speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S)) 
            transform.Translate(0,0,-speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A)) 
            transform.Translate(-speed * Time.deltaTime,0,0);

        if (Input.GetKey(KeyCode.D)) 
        transform.Translate(speed * Time.deltaTime,0,0);
    
        float mouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);
        rotY -= mouseY;
        rotY = Mathf.Clamp(rotY,-90,90);

        cam.transform.localRotation = Quaternion.Euler(rotY,0,0);
        //transform.GetChild(0).localRotation.y = Mathf.Clamp(transform.GetChild(0).localRotation.y,-90,90);

        // Interract
        if (Input.GetKeyDown(KeyCode.E)) {

            RaycastHit hit; 
            if (Physics.Raycast(cam.position,cam.forward, out hit)) {
                
                
                if (hit.collider.tag == "Door") {
                    
                    door dr = hit.collider.transform.parent.GetComponent<door>();

                    if (hit.collider.transform.rotation.eulerAngles.y == dr.defaultRot) {
                        hit.collider.transform.parent.Rotate(0,-90,0);
                    }else {
                        hit.collider.transform.parent.localRotation = Quaternion.Euler(0,dr.defaultRot,0);

                    }
                }

                if (hit.collider.tag == "Door2") {
                    
                    door dr = hit.collider.transform.parent.GetComponent<door>();

                    if (hit.collider.transform.rotation.eulerAngles.y == dr.defaultRot) {
                        hit.collider.transform.parent.Rotate(0,90,0);
                    }else {

                        hit.collider.transform.parent.localRotation = Quaternion.Euler(0,dr.defaultRot,0);
                    }
                }
            }

        }


        // Jumping
        if (onGround && Input.GetKeyDown(KeyCode.Space)) {

            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce); 
            onGround = false;
        }

    }

    void OnCollisionEnter(Collision col) {
        Debug.Log("colisionPos: " + col.gameObject.transform.position.y + " playerPos: " + transform.position.y);
        if (transform.position.y - col.gameObject.transform.position.y <= 1.2f)
            onGround = true;

    }
}
