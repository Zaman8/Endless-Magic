using UnityEngine;
using System.Collections;

public class Quick2 : MonoBehaviour {
    Tracker tracker;

	// Use this for initialization
	void Start () {
        tracker = GetComponent<Tracker>();
        LinearEquation[] equations = tracker.CalculateProjectiles("Projectile");
        Debug.Log(equations[0]);
       bool inpath = equations[0].isInPath(this.gameObject.transform.position);
        Debug.Log(inpath);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
