using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using UnityEngine.UI;

public class SolarSystemController : MonoBehaviour
{
	public GameObject SolarSystemPrefab;
    public Text text;
    private bool surfacesFound = false;
    private bool addSystem = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(surfacesFound && addSystem) {
            Touch touch;
            touch = Input.GetTouch (0);  // Input.getTouch(0) for accessing first finger, Input.getTouch(1) for accessing second finger on screen

            TrackableHit hit;  // this is the object which hits trackable Plane and holds info about trackable plane

            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon;

            if (Input.touchCount<1 || touch.phase != TouchPhase.Began ) {
            	return ;
            }

            // if our hit object touch trackable plane then Frame.Raycast function returns true else false
            if ( Frame.Raycast( touch.position.x, touch.position.y, raycastFilter, out hit) ) { // top left of our phone screen is (0,0) 
            	var anchor = hit.Trackable.CreateAnchor (hit.Pose); // to maintain position of trackable plane , and translates real world coordinates to unity verse coordinates
            	Instantiate(SolarSystemPrefab ,hit.Pose.position, Quaternion.identity, anchor.transform);
                addSystem = false;
            }
        }
        else {
            List<DetectedPlane> trackedPlanes = new List<DetectedPlane> ();
            Session.GetTrackables<DetectedPlane> (trackedPlanes); // gets all trackable planes found by arcore
            if (trackedPlanes.Count > 0){
                surfacesFound = true;
                text.text = "Surfaces Found";
            }
        }
    }

    public void AddSystem() {
        addSystem = true;
    }
}
