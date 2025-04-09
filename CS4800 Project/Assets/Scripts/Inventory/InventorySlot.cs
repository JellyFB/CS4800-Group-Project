using UnityEngine;
using UnityEngine.UI;

// A gameobject that handles an inventory slot of an inventory.
public class InventorySlot : MonoBehaviour
{
    private Image _image;

    // Selection alpha colors
    private const float _COLOR_ALPHA_SELECTED = 1f;
    private const float _COLOR_ALPHA_DESELECTED = 0.40f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GetComponentInParent<InventoryManager>() == null)
            Debug.LogError($"{this} is not under a parent InventoryManager!");

        _image = GetComponent<Image>();
    }

    public void Select()
    {
        ChangeImageAlpha(_COLOR_ALPHA_SELECTED);
    }

    public void Deselect()
    {
        ChangeImageAlpha(_COLOR_ALPHA_DESELECTED);
    }

    private void ChangeImageAlpha(float newAlpha)
    {
        Color newColor = _image.color;
        newColor.a = newAlpha;

        _image.color = newColor;
    }
}
