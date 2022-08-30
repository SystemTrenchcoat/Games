using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entities : MonoBehaviour
{
    public enum Direction { Up, Down, Left, Right };

    public Direction direction = Direction.Down;
    public bool isAttacking = false;
    public int health = 20;
    public int hp = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //Debug.Log(health);
    }

    public void changeDirection(string dir)
    {
        if (dir.Equals("Up"))
        {
            direction = Direction.Up;
        }
        else if (dir.Equals("Down"))
        {
            direction = Direction.Down;
        }
        else if (dir.Equals("Right"))
        {
            direction = Direction.Right;
        }
        else if (dir.Equals("Left"))
        {
            direction = Direction.Left;
        }
    }

    public void changeDirection(int dir)
    {
        if (dir == 0)
        {
            direction = Direction.Down;
        }
        else if (dir == 1)
        {
            direction = Direction.Up;
        }
        else if (dir == 2)
        {
            direction = Direction.Right;
        }
        else if (dir == 3)
        {
            direction = Direction.Left;
        }
    }

    public void Damage(int damage)
    {
        health -= damage;
    }

    public int changeXOffset()
    {
        int x = 0;

        if(direction == Direction.Left)
        {
            x = -1;
        }

        else if(direction == Direction.Right)
        {
            x = 1;
        }

        return x;
    }

    public int changeYOffset()
    {
        int y = 0;

        if (direction == Direction.Down)
        {
            y = -1;
        }

        else if (direction == Direction.Up)
        {
            y = 1;
        }

        return y;
    }
}
