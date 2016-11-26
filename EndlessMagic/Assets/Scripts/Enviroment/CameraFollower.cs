using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour {
    public Transform target;
    [SerializeField]
    float smoothing;

    Vector3 offset;
	// Use this for initialization
	void Start () {
        offset = transform.position - target.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        
	}
}
