using TMPro;
using UnityEngine;

public class Task
{
    [Header("Task Properties")]
    public string taskDescription;
    public int taskGoalNumber;
    public TaskTypes taskType;

    // References
    public GameObject go;
    public TextMeshProUGUI taskText;

    // Logic
    private int _currentTaskNumber;

    // Updates the text and number of the task
    public void UpdateTask()
    {
        string text = "";
        // If task contains a target goal number more than one
        if (taskGoalNumber > 1)
        {
            text += $"({_currentTaskNumber} / {taskGoalNumber}) ";
        }

        text += taskDescription;
        taskText.text = text;
    }

    // Increments a task when its goal is triggered
    public void IncrementTask()
    {
        _currentTaskNumber++;

        UpdateTask();

        if (IsFinished())
            Finish();
    }

    public bool IsFinished()
    {
        return _currentTaskNumber >= taskGoalNumber;
    }

    private void Finish()
    {
        taskText.text = $"<s> {taskText.text} </s>";
    }
}
