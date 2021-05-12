using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour {
    /*
    public Vector3 TrueNorth;
    public Transform player;
    public Quaternion PointOfAttention;
    public Transform target;
    public Transform NorthLayer;
    public Transform POALayer;

    public Transform POALocation;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        // ChangeNorthDirection();
        //ChangePOADirection();
        this.transform.LookAt(target, Vector3.RotateTowards(this.transform.position,target.position,10f,10f));

    }

    public void ChangeNorthDirection()
    {
        TrueNorth.z = player.eulerAngles.y;
        NorthLayer.localEulerAngles = TrueNorth;
    }

    public void ChangePOADirection()
    {
        Vector3 direction = transform.position - POALocation.position;

        PointOfAttention = Quaternion.LookRotation(direction);

        PointOfAttention.z = -PointOfAttention.y;

        PointOfAttention.x = 0;
        PointOfAttention.y = 0;

        POALayer.localRotation = PointOfAttention * Quaternion.Euler(TrueNorth);

    }*/

    private LineRenderer lineRender;
    private float counter;
    private float dist;

    public Transform origin;
    public Transform destination;

    public float lineDrawSpeed = 6f;


    // Use this for initialization
    void Start()
    {
        lineRender = GetComponent<LineRenderer>();
        //lineRender.SetPosition(0, origin.position);
        //lineRender.SetWidth(.01f)


    }

    // Update is called once per frame
    void Update()
    {
        lineRender.SetPosition(0, origin.position);
        // not working when running 
        dist = Vector3.Distance(origin.position, destination.position);
        //if (counter < dist)
        // {
        counter += .1f / lineDrawSpeed;

            float x = Mathf.Lerp(0, dist, counter);

            Vector3 pointA = origin.position;
            Vector3 pointB = destination.position;

            Vector3 pointALongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

            lineRender.SetPosition(1, pointALongLine);
       // }
    }
}
