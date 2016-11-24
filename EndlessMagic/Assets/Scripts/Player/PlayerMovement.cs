using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    Animator anim;
    Rigidbody2D rd;
    public float speed;
	// Use this for initialization
	void Start () {
        anim = this.gameObject.GetComponent<Animator>(); //Need animator component for setting correct animation during movement
        rd = this.gameObject.GetComponent<Rigidbody2D>(); //need rigidbody for possible force movement
	}
	
	// check for axis input and translate player
	void FixedUpdate () {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical"); //get inputs
                                                // this.gameObject.transform.Translate(new Vector2(transform.position.x + (speed/10) * x, transform.position.y + (speed/10) * y)); //gets current position and translates in both x and y from it
        rd.AddForce(new Vector2(speed * x, speed * y));
        SetAnim(x, y);
	}
    private void SetAnim(float x, float y) {
        if(x==0 && y == 0) {
            anim.SetBool("isStanding", true); //if both are 0 then he's standing, the anim controller setup decides which he goes to 
        }
        if(x != 0) {
            if (x == 1) {
                anim.SetBool("Wright", true); //right then play right
                addYMove(y);
            }
            if (x == -1)
                anim.SetBool("Wleft", true); //left then play left
                addYMove(y);
        } else
        {
            addYMove(y); //just move in a y direction
        }

    }
    private void addYMove(float y) {
        if (y == 1) //trying to create illusion of both by playing the y right after quickly
            anim.SetBool("Wup", true);
        if (y == -1)
            anim.SetBool("Wdown", true);
    }
}
