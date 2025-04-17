using UnityEngine;

public abstract class Interactable: MonoBehaviour
{
    public string objectName;
    public virtual void Interact()
    {
        Debug.LogError($"Interact method not implemented in {gameObject}");
    }

    public virtual void OnHover()
    {
        Debug.LogError($"OnHover method not implemented in {gameObject}");
    }
}
