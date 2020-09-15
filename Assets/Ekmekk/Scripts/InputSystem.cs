using UnityEngine;

public class InputSystem : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    private bool isMouseDragging;
    private Camera mainCamera;
    private Clickable clickable;
    private float planeY = 0;
    private Transform clickedObject;

    private bool isGameEnd;

    private void Awake()
    {
        Input.multiTouchEnabled = false;
        mainCamera = Camera.main;

        FindObjectOfType<GameManager>().OnGameEnd1 += () => { isGameEnd = true; };
    }

    private void Update()
    {
        if (isGameEnd)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            MouseDown();
        }

        if (Input.GetMouseButtonUp(0))
        {
            MouseUp();
        }

        if (isMouseDragging)
        {
            MouseDrag();
        }
    }

    public void SetForceClickable(Transform newClickedobj)
    {
        clickedObject = newClickedobj;
        clickable = clickedObject.GetComponent<Clickable>();
    }

    void MouseDown()
    {
        RaycastHit hitInfo;
        clickedObject = GetClickedObject(out hitInfo);
        if (clickedObject != null)
        {
            clickable = clickedObject.GetComponent<Clickable>();
            clickable?.OnClickDown();

            isMouseDragging = true;
            Vector3 pos = clickedObject.transform.position;
            clickedObject.transform.position = new Vector3(pos.x, pos.y + 1, pos.z);


            if (planeY == 0)
                planeY = clickedObject.position.y;
        }
    }

    void MouseUp()
    {
        isMouseDragging = false;
        clickable?.OnClickUp();
        clickable = null;
        clickedObject = null;
    }

    private void MouseDrag()
    {
        Plane plane = new Plane(Vector3.up, new Vector3(0, planeY, 0));
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            clickedObject.transform.position = ray.GetPoint(distance);
        }
    }

    Transform GetClickedObject(out RaycastHit hit)
    {
        GameObject targetObject = null;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, layerMask))
        {
            targetObject = hit.collider.gameObject;
            return targetObject.transform;
        }

        return null;
    }
}