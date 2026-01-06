using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
        public Transform target;
        public float 
        minX,
        maxX,
        minY,
        maxY,
        currentX,
        currentY;

    void Update()
    {
        if(target == null) { return; }
        else 
        {
            if (target.position.x <= minX){ currentX = minX; }
            else if (target.position.x >= maxX) { currentX = maxX; }
            else { currentX = target.transform.position.x; }

            if (target.position.y <= minY){ currentY = minY; }
            else if (target.position.y >= maxY) { currentY = maxY; }
            else { currentY = target.transform.position.y; }
        }
        transform.position = new Vector3 (currentX, currentY, -10);
    }
}
