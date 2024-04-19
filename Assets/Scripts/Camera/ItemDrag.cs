using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour
{
    public float forceAmount = 500; //Force on object draw

    [HideInInspector]
    public GameObject selectedGameObject; //Variable to store the selected GameObject
    [HideInInspector]
    public Rigidbody selectedRigidbody; //Variable that stores the Rigidbody of the selected GameObject
    Camera targetCamera; //Camera public variable
    Vector3 originalScreenTargetPosition, originalRigidbodyPos; //Vector3 variables to store positions
    [HideInInspector]
    public float selectionDistance; //Variable to store the distance between origin and hit point on Raycast
    private float tapTime, tapThreshold = 0.25f;
    private Color selectedItemOutlineColor = new Color(0, 0.5f, 1, 0.9f);

    // Start is called before the first frame update
    void Start()
    {
        targetCamera = GetComponent<Camera>(); //Get camera component from the Camera GameObject
    }

    void Update()
    {
        if (!targetCamera)
            return;

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) //This makes sure the mouse is not over a UI gameObject (This name makes 0 sense)
            selectedRigidbody = GetRigidbodyFromMouseClick();
        if (Input.GetMouseButtonUp(0) && selectedRigidbody)
            selectedRigidbody = null; //Clear the selectedRigidbody
    }

    void FixedUpdate()
    {
        if (selectedRigidbody)
        {
            Vector3 mousePositionOffset = targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance)) - originalScreenTargetPosition; //Get the target position offset
            selectedRigidbody.velocity = (originalRigidbodyPos + mousePositionOffset - selectedRigidbody.transform.position) * forceAmount * Time.deltaTime; //Move the target using velocity (So we can use physics)
        }
    }

    Rigidbody GetRigidbodyFromMouseClick() //Method that gets the rigidBody from a Mouse Click
    {
        RaycastHit hitInfo = new RaycastHit(); //New Raycast
        Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition); //Cast from the mousePosition on the camera
        bool hit = Physics.Raycast(ray, out hitInfo); //Did it hit?
        if (hit) //If it did
        {
            if (hitInfo.collider.gameObject.GetComponent<Rigidbody>()) //If the hit target is a Rigidbody
            {
                selectionDistance = Vector3.Distance(ray.origin, hitInfo.point); //Save the distance between origin and hit point
                originalScreenTargetPosition = targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance)); //Save the initial position of the target on the screen
                originalRigidbodyPos = hitInfo.collider.transform.position; //Save the initial position of the target in the world
                selectedGameObject = hitInfo.collider.gameObject; //Save the gameobject hit
                return hitInfo.collider.gameObject.GetComponent<Rigidbody>(); //Return the Rigidbody of the target
            }
        }

        return null;
    }
}