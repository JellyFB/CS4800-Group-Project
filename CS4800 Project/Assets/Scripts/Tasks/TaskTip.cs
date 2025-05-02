using UnityEngine;
using TMPro;

public class TaskTip : MonoBehaviour
{
    [SerializeField] private GameObject taskTipPanel;
    public TextMeshProUGUI textMesh;
    private void OnTriggerEnter()
    {
        textMesh.text = GetComponent<UnityEngine.UI.Text>().text;
        taskTipPanel.SetActive(true);
    }

    private void OnTriggerExit()
    {
        taskTipPanel.SetActive(false);
    }
}
