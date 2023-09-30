using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    
    public float defaultRot;
    // Start is called before the first frame update
    void Start()
    {
        defaultRot = transform.rotation.eulerAngles.y;
    }

   
}
