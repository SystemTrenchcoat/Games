using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.FilePathAttribute;

public class Controller : MonoBehaviour
{
    Entities entity;

    Rigidbody2D rb;

    public float speed = .2f;

    public Tilemap dangers;
    public Tilemap barriers;

    public GameObject attack;
    public float xOffset = 0;
    public float yOffset = 0;

    //private void Awake()
    //{
    //    Debug.Log("Awake");
    //    rb.GetComponent<Rigidbody2D>();
    //    entity.GetComponent<Entities>();
    //}

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        entity = GetComponent<Entities>();
    }

    // Update is called once per frame
    void Update()
    {
        //Tile-based movement
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            entity.changeDirection("Left");
            xOffset = -1;
            yOffset = 0;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            entity.changeDirection("Down");
            xOffset = 0;
            yOffset = -1;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            entity.changeDirection("Right");
            xOffset = 1;
            yOffset = 0;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            entity.changeDirection("Up");
            xOffset = 0;
            yOffset = 1;
        }
        else
        {
            xOffset = 0;
            yOffset = 0;
        }
        //Debug.Log(entity.direction);

        if(!NextTile(barriers))
        {
            gameObject.transform.Translate(new Vector3(xOffset, yOffset, 0));
        }


        //end of movement

        //attack
        entity.isAttacking = false;

        if (Input.GetKeyDown("left ctrl"))
        {
            Debug.Log("Attack");
            entity.isAttacking = true;
            Instantiate(attack, new Vector3(transform.position.x + entity.changeXOffset(), transform.position.y + entity.changeYOffset(), 0), Quaternion.identity);
        }
        //end of attack
    }

    void FixedUpdate()
    {
        //Movement
        //Smooth movement
        /*float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (vertical > 0)
        {
            entity.changeDirection("Up");
            yOffset = 1;
            xOffset = 0;
        }
        else if (vertical < 0)
        {
            entity.changeDirection("Down");
            yOffset = -1;
            xOffset = 0;
        }
        
        if (horizontal > 0)
        {
            entity.changeDirection("Right");
            xOffset = 1;
            yOffset = 0;
        }
        else if (horizontal < 0)
        {
            entity.changeDirection("Left");
            xOffset = -1;
            yOffset = 0;
        }

        gameObject.transform.Translate(new Vector3(horizontal, vertical, 0) * speed);*/


    }

    public bool NextTile(Tilemap tiles)
    {
        bool tileExist = true;

        Vector3 next = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, 0);
        Vector3Int tilesMapTile = tiles.WorldToCell(next);

        //is next tile a wall? if no, return coordinate, if yes, return current position
        if (barriers.GetTile(tilesMapTile) == null)
        {
            Debug.Log("No wall\n");
            tileExist = false;
        }

        return tileExist;
    }
}
