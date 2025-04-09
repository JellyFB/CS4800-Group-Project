using UnityEngine;

public interface Interactable
{
    public void Interact();
    public bool IsPickable();
    public Item Pick();
}
