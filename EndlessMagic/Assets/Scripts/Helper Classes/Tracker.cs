using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Timers;

public class Tracker : MonoBehaviour
{
    public void CalcuateProjectiles(string tag) {
        StartCoroutine(CalProjectiles(tag)); //public method to call
        }
    private LinearEquation[] Lines; //store all equations of projectiles
    private  Vector2[] Plocation; //first point
    private Vector2[] FPlocation; //second point
    private Transform[] ProjectilesTr; //transform component of projectiles
    private int finished = 0;
    IEnumerator CalProjectiles(string tag) { //gets all projectiles and puts thems in arrays and makes linear equations for them
        GameObject[] Projectiles = GameObject.FindGameObjectsWithTag(tag);
        Debug.Log(Projectiles.Length);
        ProjectilesTr = new Transform[Projectiles.Length]; //transforms of projectiles, because we only need to know where they are
        for (int i = 0; i < Projectiles.Length; i++)
        {
            ProjectilesTr[i] = Projectiles[i].transform; //get all the transforms from the Gameobjects
        }

        Plocation = new Vector2[ProjectilesTr.Length]; //initialize arrays for points
        FPlocation = new Vector2[ProjectilesTr.Length]; //initialize array for second point for equation
        StartCoroutine(NextLocation()); //set all the points

        yield return new WaitUntil(() => finished < 0); //wait until we get the points
        Debug.Log("first location y is " + Plocation[0].y); Debug.Log("second location y is " + FPlocation[0].y);
        LinearEquation[] Lines = new LinearEquation[ProjectilesTr.Length];
        for (int i = 0; i < Lines.Length; i++)
        {
            Lines[i] = new LinearEquation(Plocation[i], FPlocation[i], ProjectilesTr[i]); //make linear equations for all projectiles
        }
    }
    public int[] inAnyPath() { //method to see if in any paths, returns int array containing references to other arrays
        bool[] conflicts = new bool[Lines.Length]; //bool array to see if conflict
        for (int i = 0; i < Lines.Length; i++)
        {
            conflicts[i] = Lines[i].isInPath(this.gameObject.transform.position); //see if enemy is in path with method in Linear Equation
        }
        List<int> references = new List<int>();
        for (int i = 0; i < conflicts.Length; i++)
        {
            if (conflicts[i] == true) //for each element, check if its true then add it to the list
                references.Add(i);
        }
        return references.ToArray(); // return the list as an array
    }
    IEnumerator NextLocation()
    {
        for (int i = 0; i < ProjectilesTr.Length; i++)
        {
            Plocation[i] = ProjectilesTr[i].position;
        }
        yield return new WaitForSeconds(0.01f);
        Debug.Log("in next location");
        for (int i = 0; i < ProjectilesTr.Length; i++)
        {
            FPlocation[i] = ProjectilesTr[i].position;
        }
        finished = -1;
        yield break;
    }
}
internal class LinearEquation {
    // y1 - y2 = m(x1 - x2)
    private float m;
    private float xDirection;
    private float yDirection;
    private Vector2 first;
    private Vector2 second;
    private double speed;
    private Transform self;
    public LinearEquation( Vector2 first, Vector2 second, Transform self)
    {
        this.first = first;
        this.second = second;
        try
        {
            m = (first.y - second.y) / (first.x - second.x);
        }
        catch (DivideByZeroException)
        {
            m = (first.y - second.y) / 0.000001f;
            throw;
        }

        xDirection = second.x - first.x;
        yDirection = second.y - first.y;
        this.self = self;
        speed = calSpeed(first, second);
    }
    public bool isInPath(Vector2 location)
    {
        if (m * (self.position.x-second.x) + second.y + 1 >= self.position.y && self.position.y >= m * (self.position.x-second.x) + second.y - 1)
            return true;
        else
            return false;
    }
    private Direction[] DontGo()
    {
        List<Direction> cantGo = new List<Direction>();
        if(m >= 0 - 1/4 && m <= 0 + 1 / 4)
        {
            if (xDirection > 0)
                cantGo.Add(Direction.West);
            else
                cantGo.Add(Direction.East);
        }
        if(m >= 20 || m <= -20)
        {
            if (yDirection > 0)
                cantGo.Add(Direction.South);
            else
                cantGo.Add(Direction.North);
        }  
    } 
    private Direction[] buyTime() {

    }
    internal double calSpeed(Vector2 first, Vector2 second) //just used to calcuate the speed of the projectile
    {
        double distance = Math.Sqrt(Math.Abs(Math.Pow(first.x - second.x, 2)) + Math.Abs(Math.Pow(first.y - second.y, 2))); //distance formula
        double speed = distance / 0.01f; //0.01 wait time between them
        return speed;
    }
    public enum Direction
    { //North is positive y, south is negative y, East is postive x, west is negative x
        NorthEast,
        North,
        NorthWest,
        West,
        SouthWest,
        South,
        SouthEast,
        East,
    }
}
 