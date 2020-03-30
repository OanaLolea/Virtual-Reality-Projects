using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scr : MonoBehaviour
    
{
    public Rigidbody girl;

    public TextMeshPro counter;

    Animator anim;

    public Rigidbody flower01;
    public Rigidbody flower02;
    public Rigidbody flower03;
    public Rigidbody flower04;
    public Rigidbody flower05;
    public Rigidbody flower06;

    public Clock clock;

    public GameObject pickFlowerSound;
    public GameObject backgroundMusic;


    public float movementSpeed { get; set; }
    public float turnSpeed = 500f;

    private int gatheredFlowers = 0;

    private int totalFlowers = 6;

    void print()
    {
        counter.text = "Flowers: "+ gatheredFlowers.ToString() + "/" + totalFlowers.ToString();
    }

    void Start()
    {

        backgroundMusic.GetComponent<AudioSource>().Play(0);
        gatheredFlowers = 0;

        anim = GetComponent<Animator>();

        counter = counter.GetComponent<TextMeshPro>();

        print();

        movementSpeed = 5;

        //clock = clock.GetComponent<Clock>();

    }




    // Update is called once per frame
    void Update()
    {

        float move = Input.GetAxis("Vertical");
        anim.SetFloat("Speed", move);

        float angle = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg;


        if (Input.GetKey(KeyCode.UpArrow))
        {
            girl.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            anim.Play("run");

        }
        else
        if (Input.GetKey(KeyCode.DownArrow))
        {
            girl.transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
            anim.Play("run");

        }
        else
        if (Input.GetKey(KeyCode.Space))
        {
            anim.Play("jump");
            return;
        }

        
        else
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.Rotate(-Vector3.up * 25 * Time.deltaTime, Space.Self);
            //girl.transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
            //girl.transform.Rotate(0, -1, 0);
           
            girl.transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
            
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.Rotate(Vector3.up * 25 * Time.deltaTime, Space.Self);
            //girl.transform.Rotate(Vector3.up * turnSpeed* Time.deltaTime);
            //girl.transform.Rotate(0, 1, 0);

            girl.transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        }
        else
            anim.Play("idle");


        if (clock.isOver())
            if (gatheredFlowers == totalFlowers)
            {
                // play a sound
                counter.text = "Congratulations! You can visit granny";
            }
            else
            {
                // play another sound
                counter.text = "You failed collecting flowers in time!";
                //girl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                //girl.GetComponent<Rigidbody>().detectCollisions = false;
                //StartCoroutine(waitToClose(5));

                Application.Quit();
            }

    }

    IEnumerator waitToClose(float duration)
    {
        
        yield return new WaitForSeconds(duration);   //Wait
    }


    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.attachedRigidbody.name);
        if (other.attachedRigidbody.name == "flower01" || other.attachedRigidbody.name == "flower02" || other.attachedRigidbody.name == "flower03" || other.attachedRigidbody.name == "flower04" || other.attachedRigidbody.name == "flower05" || other.attachedRigidbody.name == "flower06")
            if(!clock.isOver()){
                pickFlowerSound.GetComponent<AudioSource>().Play(0);
                Destroy(other);
                gatheredFlowers++;
                print();
                Destroy(other.gameObject);
            }
    }
}
   
