using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class Interactable: MonoBehaviour
{
    public string objectName;
    private Outline outline;

    protected virtual void Awake()
	{
		outline = GetComponent<Outline>();
        if (outline != null) {
            outline.enabled = false;
        }
	}

	public virtual void Interact()
    {
        Debug.LogError($"Interact method not implemented in {gameObject}");
    }

    // Returns the feedback text on hover
    public virtual string OnHover()
    {
        if (outline != null) {
            outline.enabled = true;
        }

        string text = "Press [E] to Interact — ";
        text += objectName;
        return text;
    }

    public virtual void OnHoverExit() {
        if (outline != null) {
            outline.enabled = false;
        }
    }
}
