using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour

    
{

    public GameObject sphere;
    // Start is called before the first frame update
    void Start()
    {
        sphere = GameObject.Find("ball");
        //sphere.GetComponent<Rigidbody>().AddForce(Vector3.forward * 100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStart(Collision col)
    {
        //sphere.GetComponent<Rigidbody>().AddForce(Vector3.back * 100);
    }

    void OnCollisionExit(Collision col)
    {
        
        //sphere.GetComponent<Rigidbody>().AddForce(Vector3.back * 100);
    }
}
