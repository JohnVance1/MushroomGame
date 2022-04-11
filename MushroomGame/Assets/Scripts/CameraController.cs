using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance { get; private set; }
    public bool IsFollowing { get; set; }

    // bounds of the level
    public BoxCollider2D LevelBounds;
    private Vector3 _min;
    private Vector3 _max;

    private Camera cam;
    public float x;
    public float y;

    public Player player;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            //Destroy(this);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }


    public void Start()
    {
        _min = LevelBounds.bounds.min;
        _max = LevelBounds.bounds.max;
        IsFollowing = true;
        cam = GetComponent<Camera>();
        DontDestroyOnLoad(gameObject);
    }


    public void Update()
    {
        _min = LevelBounds.bounds.min;
        _max = LevelBounds.bounds.max;
        if (IsFollowing)
        {
            x = player.transform.position.x;
            y = player.transform.position.y;

            CheckCameraLocation();
        }
    }

    public void CheckCameraLocation()
    {
        float cameraHalfWidth = cam.orthographicSize * ((float)Screen.width / Screen.height);

        // lock the camera to the right or left bound if we are touching it
        x = Mathf.Clamp(x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);

        // lock the camera to the top or bottom bound if we are touching it
        y = Mathf.Clamp(y, _min.y + cam.orthographicSize, _max.y - cam.orthographicSize);

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
