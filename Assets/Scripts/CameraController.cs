using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform charater;
    private float sizeCamara;
    private float heightScreen;

    // Start is called before the first frame update
    void Start()
    {
        sizeCamara = Camera.main.orthographicSize;
        heightScreen = sizeCamara * 2;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateCameraPosition();
    }

    void CalculateCameraPosition()
    {
        int characterScreen = (int)(charater.position.y / heightScreen);
        float heightCamera = (characterScreen * heightScreen);

        transform.position = new Vector3(transform.position.x, heightCamera, transform.position.z);
    }
}
