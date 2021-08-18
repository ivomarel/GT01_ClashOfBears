using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CamControls : Singleton<CamControls>
{

    [Header("Zooming")]
    public float zoomSensitivity = 1f;
    [Range(0,1)]
    public float smoothing = 0.1f;
    public Transform minZoomTransform;
    public Transform maxZoomTransform;
    public bool zoomToMouse;

    [Header("Panning")]
    public bool enableEdgePan;
    public float panSpeed = 0.5f;
    public float cursorPanBoundary = 2;
    public Bounds bounds;

    [Header("Rotating")]
    public float rotationSpeed = 1;


    private float zoomValue = 1;

    //Camera shake
    private float shakeDuration = 2f;

    //We force the camera to go back to 0,0,0 after the tween has completed. This is to avoid a strange offset after each tween
    private void OnShakeComplete ()
    {
        Camera.main.transform.localPosition = new Vector3(0, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enableEdgePan)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        
        Pan();         
        
        Zoom();

        Rotate();
    }

    void Pan ()
    {
        //Pan by keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        this.transform.Translate(horizontalInput * panSpeed, 0, verticalInput * panSpeed);

        if (enableEdgePan)
        {
            //Pan by mouse on sides
            if (Input.mousePosition.x < cursorPanBoundary)
            {
                this.transform.Translate(-panSpeed, 0, 0);
            }
            if (Input.mousePosition.y < cursorPanBoundary)
            {
                this.transform.Translate(0, 0, -panSpeed);
            }
            if (Input.mousePosition.x > Screen.width - cursorPanBoundary)
            {
                this.transform.Translate(panSpeed, 0, 0);
            }
            if (Input.mousePosition.y > Screen.height - cursorPanBoundary)
            {
                this.transform.Translate(0, 0, panSpeed);
            }
        }

        //We clamp the camera to stay inside the bounds at all times
        transform.position = bounds.ClosestPoint(transform.position);
    }

    void Zoom ()
    {
        //We store the position before we zoom, so we can check how much our position is offset after the zoom
        Vector3 mousePosBeforeZoom = GetMouseHitPoint();


        if (!EventSystem.current.IsPointerOverGameObject())
        {
            //By using GetAxis we do NOT need an if-statement since it automatically is negative, positive or 0 based on the scrolling
            float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

            zoomValue -= scrollDelta * zoomSensitivity;

            zoomValue = Mathf.Clamp(zoomValue, 0, 1);
        }
        //Since zoomValue goes between 0 and 1, we can use it to lerp between these two Transforms
        Vector3 newPosition = Vector3.Lerp(minZoomTransform.position, maxZoomTransform.position, zoomValue);
        Quaternion newRotation = Quaternion.Lerp(minZoomTransform.rotation, maxZoomTransform.rotation, zoomValue);

        Transform camContainer = Camera.main.transform.parent;
        //This way of Lerping will create smooth movement to the new position
        //The difference is that we are now using the cam-position (which updates every frame) as the from-Vector
        camContainer.position = Vector3.Lerp(camContainer.position, newPosition, smoothing);
        camContainer.rotation = Quaternion.Slerp(camContainer.rotation, newRotation, smoothing);

        if (zoomToMouse)
        {
            Vector3 mousePosAfterZoom = GetMouseHitPoint();

            Vector3 offset = mousePosBeforeZoom - mousePosAfterZoom;
            //We move towards where our mouse hit the world, to create a 'Google Maps' style zooming
            transform.Translate(offset, Space.World);
        }
    }

    void Rotate ()
    {
        float rotationInput = Input.GetAxis("Rotation");
        //Rotating the pivot
        transform.Rotate(0, rotationInput * rotationSpeed, 0);
    }



    /// <summary>
    /// Returns where the mouse hits our terrain
    /// </summary>
    /// <returns></returns>
    private Vector3 GetMouseHitPoint()
    {
        //This creates a Ray object, starting at our mouse position, pointing into the world
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        //This shoots the ray into the world
        //We use a LayerMask to avoid hitting other layers than the terrain layer
        if (Physics.Raycast(mouseRay, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Terrain")))
        {
            //We set the building to where the ray hit our terrain                
            return hitInfo.point;
        }

        return Vector3.zero;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
}
