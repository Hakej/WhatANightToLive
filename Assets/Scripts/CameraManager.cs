using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public Camera CameraSecurityRoom;
    public Camera Camera1A;
    public Camera Camera1B;
    public Camera Camera1C;
    public Camera Camera2A;
    public Camera Camera2B;
    public Camera Camera3;
    public Camera Camera4A;
    public Camera Camera4B;
    public Camera Camera5;
    public Camera Camera6;
    public Camera Camera7;

    protected Camera[] cameras;

    [SerializeField]
    protected int currentCamera;

    private void Awake()
    {
        cameras = new Camera[12];

        cameras[0] = CameraSecurityRoom;
        cameras[1] = Camera1A;
        cameras[2] = Camera1B;
        cameras[3] = Camera1C;
        cameras[4] = Camera2A;
        cameras[5] = Camera2B;
        cameras[6] = Camera3;
        cameras[7] = Camera4A;
        cameras[8] = Camera4B;
        cameras[9] = Camera5;
        cameras[10] = Camera6;
        cameras[11] = Camera7;
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            IncCamera();
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            DecCamera();
        }
    }

    private void IncCamera()
    {
        cameras[currentCamera].GetComponent<AudioListener>().enabled = false;
        cameras[currentCamera].enabled = false;

        currentCamera++;

        if (currentCamera >= cameras.Length)
        {
            currentCamera = 0;
        }

        cameras[currentCamera].enabled = true;
        cameras[currentCamera].GetComponent<AudioListener>().enabled = true;
    }

    private void DecCamera()
    {
        cameras[currentCamera].GetComponent<AudioListener>().enabled = false;
        cameras[currentCamera].enabled = false;

        currentCamera--;

        if (currentCamera < 0)
        {
            currentCamera = cameras.Length - 1;
        }

        cameras[currentCamera].enabled = true;
        cameras[currentCamera].GetComponent<AudioListener>().enabled = true;
    }
}
