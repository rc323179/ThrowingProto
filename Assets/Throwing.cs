using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    GameObject myGrabbedObject;

    public float grabDistance;
    public float objCatchSpeed;

    public Transform throwPoint;
    public float fireSpeed;

    public float rotForce;

    public float delay;

    public bool hasObj = false;

    public float weight;

    Object myGrabbedStats;

    bool isStabbing;

    float AnimationTimer;

    bool forwardStab;

    private void Start()
    {
        myGrabbedObject = null;
        hasObj = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasObj)
        {
            GameObject[] ts = GameObject.FindGameObjectsWithTag("Throwable");


            GameObject myT = null;
            float d = Mathf.Infinity;

            foreach (GameObject throwable in ts)
            {
                if (Vector2.Distance(transform.position, throwable.transform.position) < d && throwable.GetComponent<Rigidbody2D>().velocity.magnitude < objCatchSpeed)
                {
                    myT = throwable;
                    d = Vector2.Distance(transform.position, throwable.transform.position);
                }
            }

            if (myT != null)
            {
                grabDistance = myT.transform.GetChild(0).transform.localScale.x;
            }
        }

        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        

        Vector3 rot = mouse - new Vector2(transform.position.x, transform.position.y);

        rot = rot.normalized;

        delay -= Time.deltaTime; // Added Code (RyanC5)
        throwPoint.transform.position = transform.position + ((grabDistance*1.5f) * rot);


        hasObj = (myGrabbedObject == null) ? false : true;

        if (!hasObj)
        {
            if(delay <= 0)
            {
                GameObject[] throwables = GameObject.FindGameObjectsWithTag("Throwable");

                GameObject myTarget = null;
                float dist = grabDistance;

                foreach (GameObject throwable in throwables)
                {
                    if(Vector2.Distance(transform.position, throwable.transform.position) < dist && throwable.GetComponent<Rigidbody2D>().velocity.magnitude < objCatchSpeed)
                    {
                        myTarget = throwable;
                        delay = throwable.GetComponent<Object>().delay/1000f; // Added Code (RyanC5)
                        weight = throwable.GetComponent<Object>().weight; // Added Code (RyanC5)
                        GetComponent<PlayerMovement>().speed = GetComponent<PlayerMovement>().displaySpeed - weight/20; // Added Code (RyanC5)
                        dist = Vector2.Distance(transform.position, throwable.transform.position);
                    }
                }

                myGrabbedObject = myTarget;

                if (myTarget != null)
                {
                   myGrabbedStats = myTarget.GetComponent<Object>();
                    myGrabbedObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                }
            }

        } else
        {

            throwPoint.transform.position = transform.position + ((myGrabbedObject.transform.GetChild(0).transform.localScale.x+AnimationTimer) * rot);


            myGrabbedObject.transform.position = throwPoint.transform.position;


            if(delay <= 0f)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    myGrabbedObject.GetComponent<Rigidbody2D>().velocity = rot * myGrabbedObject.GetComponent<Object>().speed;
                    myGrabbedObject.GetComponent<Rigidbody2D>().mass = myGrabbedObject.GetComponent<Object>().weight;
                    myGrabbedObject.GetComponent<Rigidbody2D>().drag = myGrabbedObject.GetComponent<Object>().drag;

                    myGrabbedObject.GetComponent<Rigidbody2D>().AddTorque(rotForce, ForceMode2D.Force);

                        GetComponent<PlayerMovement>().speed = GetComponent<PlayerMovement>().displaySpeed; // Added Code (RyanC5)
                    hasObj = false;
                    myGrabbedObject = null;
                    AnimationTimer = 0f;
                    delay = myGrabbedStats.dropDelay/1000f;
                }
                else if(Input.GetKeyDown(KeyCode.Mouse1)) // Added Code (RyanC5)
                {
                    delay = myGrabbedStats.stabTime/1000f;
                    isStabbing = true;
                    forwardStab = true;
                }
            }
            if(isStabbing) // Added Code (RyanC5)
            {
                if(forwardStab)
                {
                    AnimationTimer += myGrabbedStats.stabRange * Time.deltaTime / myGrabbedStats.stabTime * 1000f;
                    if(delay <= myGrabbedStats.stabTime/2000f)
                    {
                        forwardStab = false;
                    }
                }
                else
                {
                    AnimationTimer -= myGrabbedStats.stabRange * Time.deltaTime / myGrabbedStats.stabTime * 1000f;
                    if(delay <= 0)
                    {
                        isStabbing = false;
                        AnimationTimer = 0f;
                    }
                }
            }
        }






    }
}
