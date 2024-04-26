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


    bool hasObj = false;


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

        throwPoint.transform.position = transform.position + ((grabDistance*1.5f) * rot);



        hasObj = (myGrabbedObject == null) ? false : true;

        if (!hasObj)
        {
            GameObject[] throwables = GameObject.FindGameObjectsWithTag("Throwable");

            GameObject myTarget = null;
            float dist = grabDistance;

            foreach (GameObject throwable in throwables)
            {
                if(Vector2.Distance(transform.position, throwable.transform.position) < dist && throwable.GetComponent<Rigidbody2D>().velocity.magnitude < objCatchSpeed)
                {
                    myTarget = throwable;
                    dist = Vector2.Distance(transform.position, throwable.transform.position);
                }
            }

            myGrabbedObject = myTarget;


            if (myTarget != null)
            {
                myGrabbedObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }

        } else
        {
            throwPoint.transform.position = transform.position + ((myGrabbedObject.transform.GetChild(0).transform.localScale.x) * rot);


            myGrabbedObject.transform.position = throwPoint.transform.position;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                myGrabbedObject.GetComponent<Rigidbody2D>().velocity = rot * myGrabbedObject.GetComponent<Object>().speed;
                myGrabbedObject.GetComponent<Rigidbody2D>().mass = myGrabbedObject.GetComponent<Object>().weight;
                myGrabbedObject.GetComponent<Rigidbody2D>().drag = myGrabbedObject.GetComponent<Object>().drag;

                myGrabbedObject.GetComponent<Rigidbody2D>().AddTorque(rotForce, ForceMode2D.Force);

                hasObj = false;
                myGrabbedObject = null;
            }
        }






    }
}
