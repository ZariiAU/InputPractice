using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent OnInteracted;

    public virtual void Interact()
    {
        OnInteracted.Invoke();
    }
}
