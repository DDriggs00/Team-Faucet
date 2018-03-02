using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_CameraController : MonoBehaviour {
    Transform player;
    Transform camT;
    Vector3 target = Vector2.zero;
    float moveSpeed = 40f;
    float minSmooth = 10f;
    bool inBigRoom = false;
    float minX, maxX, minY, maxY;

    private void Awake()
    {
        camT = Camera.main.transform;
        player = this.transform;
    }

    private void Update()
    {
        if (!inBigRoom)
            MoveToTarget();
        else
            MoveInBigRoom();
       
    }

    void MoveToTarget()
    {
        if (camT.position.x != target.x || camT.position.y != target.y)
        {
            Vector3 newPos = new Vector3(target.x, target.y, camT.position.z);
            // The speed of the camera will decrease as the camera gets closer to its target, to a minimum speed of minSmooth
            float tempSpeed = Mathf.Max(minSmooth, moveSpeed * (Vector2.Distance(target, (Vector2)camT.position) / 5));
            camT.position = Vector3.MoveTowards(camT.position, newPos, tempSpeed * Time.deltaTime);
        }
    }

    void MoveInBigRoom()
    {
        target = player.position;
        target.x = Mathf.Max(minX, target.x);
        target.x = Mathf.Min(maxX, target.x);
        target.y = Mathf.Max(minY, target.y);
        target.y = Mathf.Min(maxY, target.y);

        MoveToTarget();
    }

    public void SetTarget(Vector2 pos)                                                       // this is called by room objects when a player enters the room
    {
        target = pos;
        inBigRoom = false;
    }

    public void SetInBigRoom(float minX, float maxX, float minY, float maxY)        // called by room objects with more than one room unit
    {                                                                                       // sets up bounds for camera to move within
        if (!inBigRoom)
        {
            inBigRoom = true;
            this.minX = minX;
            this.maxX = maxX;
            this.minY = minY;
            this.maxY = maxY;
        }
    }

    public bool IsInBigRoom()
    { return inBigRoom; }

}
