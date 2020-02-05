using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Seperate dev

public class Kinematic : MonoBehaviour
{
    public Vector3 linearVelocity;
    public float angularVelocity;  // Millington calls this rotation
    // because I'm attached to a gameobject, we also have:
    // rotation <<< Millington calls this orientation
    // position
    public string behaviorType;

    public GameObject myTarget;
    public Kinematic alsoMyTargetForPursueTho;

    pathFollow myPathFollow = new pathFollow();
    Seperate mySeperate = new Seperate();

    // Set of gameobjects to create path
    public GameObject[] pathToFollow;

    // Set of targets to seperate away from
    public Kinematic[] targetsForSeperate;

    public enum Behavior
    {
        Seek,
        Flee
    }

    // Update is called once per frame
    void Update()
    {
        // update my position and rotation
        this.transform.position += linearVelocity * Time.deltaTime;
        Vector3 v = new Vector3(0, angularVelocity, 0);
        this.transform.eulerAngles += v * Time.deltaTime;

        // update linear and angular velocities
        SteeringOutput steering = new SteeringOutput();

        // Seek: target is "alsoMyTargetForPursueTho"
        if (behaviorType == "Seek" || behaviorType == "seek")
        {
            Seek mySeek = new Seek();

            mySeek.target = alsoMyTargetForPursueTho;
            mySeek.character = this;

            steering = mySeek.getSteering();
            linearVelocity += steering.linear * Time.deltaTime;
            angularVelocity += steering.angular * Time.deltaTime;
        }
        // Flee
        else if (behaviorType == "Flee" || behaviorType == "flee")
        {
            Flee myFlee = new Flee();
            myFlee.character = this;
            myFlee.target = myTarget;
            steering = myFlee.getSteering();
            linearVelocity += steering.linear * Time.deltaTime;
            angularVelocity += steering.angular * Time.deltaTime;
        }
        // Arrive
        else if (behaviorType == "Arrive" || behaviorType == "arrive")
        {
            Arrive myArrive = new Arrive();
            myArrive.character = this;
            myArrive.target = myTarget;
            steering = myArrive.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
            else
            {
                linearVelocity = Vector3.zero;
            }
        }
        // Align
        else if (behaviorType == "Align" || behaviorType == "align")
        {
            Align myAlign = new Align();
            myAlign.character = this;
            myAlign.target = myTarget;
            steering = myAlign.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }
        // Face
        else if (behaviorType == "Face" || behaviorType == "face")
        {
            Face myFace = new Face();
            myFace.character = this;
            myFace.target = myTarget;
            steering = myFace.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }
        // Look where you're going
        else if (behaviorType == "LWYG" || behaviorType == "lwyg" || behaviorType == "Lwyg")
        {
            LWYG myLook = new LWYG();
            myLook.character = this;
            steering = myLook.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }
        // Path following
        else if (behaviorType == "pathFollow" || behaviorType == "PathFollow")
        {
            myPathFollow.character = this;
            myPathFollow.path = pathToFollow;
            steering = myPathFollow.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }
        // Pursue
        else if (behaviorType == "Pursue" || behaviorType == "pursue")
        {
            Pursue myPursue = new Pursue();
            myPursue.character = this;
            myPursue.target = alsoMyTargetForPursueTho;
            steering = myPursue.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }
        // Seperate
        else if (behaviorType == "Seperate" || behaviorType == "seperate")
        {
            mySeperate.character = this;
            mySeperate.targets = targetsForSeperate;
            steering = mySeperate.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }
        
    }
}
