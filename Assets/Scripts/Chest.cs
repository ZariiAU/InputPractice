using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Interactable))]
public class Chest : MonoBehaviour 
{
    Interactable interactable;
    Animator anim;
    private void Start()
    {
        interactable = GetComponent<Interactable>();
        anim = GetComponent<Animator>();
    }

    public void Open()
    {
        if (anim.GetBool("Open") == false)
            anim.SetBool("Open", true);
        else
            anim.SetBool("Open", false);
    }
}
