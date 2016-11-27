using UnityEngine;
using System.Collections;

public class Quick : MonoBehaviour {
    Rigidbody2D rd; 
	// Use this for initialization
	void Start () {
        rd = GetComponent<Rigidbody2D>();
        rd.AddForce(new Vector2(0, 15));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
