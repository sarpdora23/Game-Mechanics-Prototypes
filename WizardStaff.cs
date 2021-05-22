using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStaff : MonoBehaviour
{
    public Camera camera;
    private RaycastHit hit;
    public Material chosen_material;
    private Material default_material;
    public float force;
    private LineRenderer lineren;
    public GameObject staff;
    // Start is called before the first frame update
    void Start()
    {
        lineren = gameObject.GetComponent<LineRenderer>();
        lineren.enabled = false;
        //lineren.SetPosition(1, new Vector3(0.11f, 0.88f, 6.03f));
    }

    // Update is called once per frame
    void Update()
    {
        lineren.SetPosition(0, staff.transform.position);
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            GameObject selected_object = hit.transform.gameObject;
            if (selected_object.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                Debug.Log(hit.transform.gameObject.name);
                if (Input.GetMouseButtonDown(1))
                {
                    Vector3 direction = hit.point - gameObject.transform.position;
                    rb.AddForce(direction.normalized * force, ForceMode.Impulse);
                }
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
                {
                    lineren.enabled = true;
                    rb.isKinematic = true;
                    default_material = selected_object.GetComponent<Renderer>().material;
                    selected_object.GetComponent<Renderer>().material = chosen_material;
                    selected_object.transform.parent = camera.transform;
                    lineren.SetPosition(1, hit.point);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    lineren.enabled = false;
                    rb.isKinematic = false;
                    rb.useGravity = true;
                    selected_object.transform.parent = null;
                    selected_object.GetComponent<Renderer>().material = default_material;
                }
            }
        }
    }
}

