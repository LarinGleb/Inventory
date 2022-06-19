using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public void DragObjectToMouse(GameObject gameObjectHold) {

        Plane dragPlane = new Plane(Camera.main.transform.forward, transform.position);
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter = 0.0f;
        if (dragPlane.Raycast(camRay, out enter)) 
        {
            Vector3 fingerPosition = camRay.GetPoint(enter);
            gameObjectHold.transform.position = fingerPosition;
        }
    }
}
