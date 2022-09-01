using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entities : MonoBehaviour
{
    public enum Direction { Up, Down, Left, Right };

    public Direction direction = Direction.Down;
    public Direction defendDirection = Direction.Up;
    public bool isAttacking = false;
    public bool isDefending = false;
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
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void changeDirection(string dir)
    {
        if (dir.Equals("Up"))
        {
            direction = Direction.Up;
            defendDirection = Direction.Down;
        }
        else if (dir.Equals("Down"))
        {
            direction = Direction.Down;
            defendDirection = Direction.Up;
        }
        else if (dir.Equals("Right"))
        {
            direction = Direction.Right;
            defendDirection = Direction.Left;
        }
        else if (dir.Equals("Left"))
        {
            direction = Direction.Left;
            defendDirection = Direction.Right;
        }
    }

    public void changeDirection(int dir)
    {
        if (dir == 3)
        {
            direction = Direction.Down;
            defendDirection = Direction.Up;
        }
        else if (dir == 1)
        {
            direction = Direction.Up;
            defendDirection = Direction.Down;
        }
        else if (dir == 0)
        {
            direction = Direction.Right;
            defendDirection = Direction.Left;
        }
        else if (dir == 2)
        {
            direction = Direction.Left;
            defendDirection = Direction.Right;
        }
    }

    public int defendDirectionOffsetX()
    {
        int off = 0;

        if (defendDirection == Direction.Left)
        {
            off = -1;
        }

        else if (defendDirection == Direction.Right)
        {
            off = 1;
        }

        return off;
    }

    public int defendDirectionOffsetY()
    {
        int off = 0;

        if (defendDirection == Direction.Down)
        {
            off = -1;
        }

        else if (defendDirection == Direction.Up)
        {
            off = 1;
        }

        return off;
    }

    public void Damage(int damage)
    {
        if (!isDefending)
        {
            health -= damage;
        }
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
