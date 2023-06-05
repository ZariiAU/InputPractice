using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    Camera cam;
    public float rayLength = 2;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Movement>().cam;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInteractable();
    }

    void CheckForInteractable()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, rayLength))
            {
                if (hit.collider.GetComponent<Interactable>() != null)
                {
                    Interactable i = hit.collider.GetComponent<Interactable>();

                    i.Interact();
                }
            }
        }

        Debug.DrawRay(cam.transform.position, cam.transform.forward * rayLength, Color.red);
    }
}
