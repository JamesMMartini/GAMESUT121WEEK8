using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomManager : MonoBehaviour
{
    public GameObject door;

    public GameObject room;

    public SpriteRenderer sprite;
    System.Random rand;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        rand = new System.Random();
    }

    public void ScaleDoor(float scale)
    {
        door.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void SetColor()
    {
        int value = rand.Next(150, 226);
        sprite.color = new Color(value, value, value, 255);
    }

    public void AddDoor(MapMaker.Direction dir)
    {
        if (dir == MapMaker.Direction.UP)
        {
            GameObject wall = transform.Find("Top").gameObject;
            wall.SetActive(false);

            GameObject newDoor = Instantiate(door);
            newDoor.transform.parent = room.transform;
            newDoor.transform.localPosition = wall.transform.localPosition;

        }
        else if (dir == MapMaker.Direction.DOWN)
        {
            GameObject wall = transform.Find("Bottom").gameObject;
            wall.SetActive(false);

            GameObject newDoor = Instantiate(door);
            newDoor.transform.parent = room.transform;
            newDoor.transform.localPosition = wall.transform.localPosition;
        }
        else if (dir == MapMaker.Direction.LEFT)
        {
            GameObject wall = transform.Find("Left").gameObject;
            wall.SetActive(false);

            GameObject newDoor = Instantiate(door);
            newDoor.transform.Rotate(0f, 0f, 90f, Space.World);
            newDoor.transform.parent = room.transform;
            newDoor.transform.localPosition = wall.transform.localPosition;
        }
        else
        {
            GameObject wall = transform.Find("Right").gameObject;
            wall.SetActive(false);

            GameObject newDoor = Instantiate(door);
            newDoor.transform.Rotate(0f, 0f, 90f, Space.World);
            newDoor.transform.parent = room.transform;
            newDoor.transform.localPosition = wall.transform.localPosition;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
