using UnityEngine;

public abstract class Interactable: MonoBehaviour
{
    public string objectName;
    public virtual void Interact()
    {
        Debug.LogError($"Interact method not implemented in {gameObject}");
    }

    // Returns the feedback text on hover
    public virtual string OnHover()
    {
        string text = "Press [E] to Interact — ";
        text += objectName;

        return text;
    }
}
