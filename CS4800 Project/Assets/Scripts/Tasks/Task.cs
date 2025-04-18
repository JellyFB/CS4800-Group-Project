using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Task
{
    [Header("Task Properties")]
    public string taskDescription;
    public int taskGoalNumber;
    public TaskTypes taskType;

    [Header("References")]
    public GameObject go;
    public TextMeshProUGUI taskText;
    public Image panel;

    // Logic
    private int _currentTaskNumber;

    // Assign the components of the task
    public void InitializeTask(string description, TaskTypes type, int goalNumber, GameObject go)
    {
        this.go = go;
        taskText = go.GetComponentInChildren<TextMeshProUGUI>();
        panel = go.GetComponent<Image>();
        taskDescription = description;
        taskType = type;
        taskGoalNumber = goalNumber;
    }

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

    // Checks if the task is finished
    public bool IsFinished()
    {
        return _currentTaskNumber >= taskGoalNumber;
    }

    // Finish behavior of tasks
    private void Finish()
    {
        // Strikesthrough the text
        taskText.text = $"<s> {taskText.text} </s>";

        // Changing color of the panel to green
        Color newColor = Color.green;
        newColor.a = 0.75f;
        panel.color = newColor;
    }
}
