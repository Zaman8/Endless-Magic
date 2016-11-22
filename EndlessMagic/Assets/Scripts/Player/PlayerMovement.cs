using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    Animator anim;
    Rigidbody2D rd;
    public int speed;
	// Use this for initialization
	void Start () {
        anim = this.gameObject.GetComponent<Animator>(); //Need animator component for setting correct animation during movement
        rd = this.gameObject.GetComponent<Rigidbody2D>(); //need rigidbody for possible force movement
	}
	
	// check for axis input and translate player
	void FixedUpdate () {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical"); //get inputs


	}
    internal void SetAnim(float x, float y) {
        if(x==0 && y == 0) {
            anim.SetBool("isStanding", true); //if both are 0 then he's standing, the anim controller setup decides which he goes to 
        }
        if(x != 0) {
            if (x == 1) {
                anim.SetBool("Wright", true); //right then play right
                if (y == 1) //trying to create illusion of both by playing the y right after quickly
                    anim.SetBool("Wup", true);
                if (y == -1)
                    anim.SetBool("Wdown", true);
            }
            if (x == -1)
                anim.SetBool("Wleft", true); //left then play left
        }

    }
}
