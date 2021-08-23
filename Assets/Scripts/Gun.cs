using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Transform gunTransform;
    float speed = 4;   
    // Start is called before the first frame update
    void Start()
    {
        gunTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            gunTransform.position = new Vector3(gunTransform.position.x , gunTransform.position.y, gunTransform.position.z + Time.deltaTime * speed); 
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gunTransform.position = new Vector3(gunTransform.position.x + speed * Time.deltaTime, gunTransform.position.y, gunTransform.position.z + Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            gunTransform.position = new Vector3(gunTransform.position.x - speed * Time.deltaTime, gunTransform.position.y, gunTransform.position.z + Time.deltaTime * speed);
        }
    }
}
