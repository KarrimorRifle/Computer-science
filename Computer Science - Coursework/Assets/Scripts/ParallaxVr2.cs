using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxVr2 : MonoBehaviour
{
    public Camera cam; //reference to the camera
    public Transform subject; //reference to the main subject (player)
    Vector2 startPos; //the start poisition of XY
    float startZ; //position of z will always stay the same
    Vector2 travel => (Vector2)cam.transform.position - startPos; //how far the camera is travelling
    
    float distanceFromSubject => transform.position.z - subject.position.z;
    float clippingPlane => (cam.transform.position.z + (distanceFromSubject > 0? cam.farClipPlane : cam.nearClipPlane));
    //float parallaxFactor => Mathf.Abs(distanceFromSubject) / clippingPlane; //multiplier for how far we need to move the background
    public Vector2 parallaxFactor;
    public void Start(){
        startPos = transform.position;
        startZ = transform.position.z;

    }
    public void FixedUpdate(){
        Vector2 newPos = (startPos + travel * parallaxFactor);
        transform.position = new Vector3(newPos.x, newPos.y, startZ);
    }
}
