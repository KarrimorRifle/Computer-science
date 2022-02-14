using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxBackground : MonoBehaviour
{
    [SerializeField]private Vector2 parallaxEffectMultiplier;//multiplier for how much the background moves
    private Transform cameraTransform; //creates a private transform variable for the camera (will be referenced with unity)
    private Vector3 lastCameraPosition; //creates a vector3 component the remember the postion of the camera
    private float textureUnitSizeX;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }
    private void FixedUpdate(){
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition; //checks how much the camera has moved since the last frame
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
        lastCameraPosition = cameraTransform.position;

        if (cameraTransform.position.x - transform.position.x >= textureUnitSizeX){
            float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(cameraTransform.position.x + offsetPositionX , transform.position.y);
        }

    }
}
