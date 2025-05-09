using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    // Singleton
    public static TaskManager instance;

    [Header("References")]
    [SerializeField] private GameObject _taskContainer;
    [SerializeField] private GameObject _taskPrefab;
    [SerializeField] private GameObject _finishLevelMenu;
    [SerializeField] private AudioClip _finishTask;

    // Task list
    private List<Task> _taskList = new List<Task>();
    int tracker = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void AddTask(string description, TaskTypes type, int goalNumber)
    {
        // Initialize the task
        Task task = new Task();
        GameObject go = Instantiate(_taskPrefab, _taskContainer.transform);
        task.InitializeTask(description, type, goalNumber, go);

        // Add task to list
        _taskList.Add(task);

        // Update task to reflect the assigned components
        task.UpdateTask();
    }

    // Remove task
    public void RemoveTask(Task task)
    {
        _taskList.Remove(task);
        Destroy(task.go);
    }

    // Removes all tasks
    public void Clear()
    {
        for (int i = _taskList.Count - 1; i >= 0; i--)
        {
            RemoveTask(_taskList[i]);
        }
    }

    // Triggers whenever a task parameter gets called and increments tasks that are under that
    public virtual void IncrementTask(TaskTypes type)
    {
        foreach (Task task in _taskList)
        {
            if (task.taskType == type) {
                bool wasCompleted = task.IsFinished();
                task.IncrementTask();
                if (!wasCompleted && task.IsFinished())
                    AudioManager.instance.PlaySound(_finishTask);
            }
        }

        if (IsLevelComplete())
            CompleteLevel();
    }

    // Sets a task's progress
    public void SetTaskProgress(TaskTypes type, int progress)
    {
        foreach (Task task in _taskList)
        {
            if (task.taskType == type)
            {
                task.SetTaskProgress(progress);
            }
        }

        if (IsLevelComplete())
            CompleteLevel();
    }

    // Force finishes a task
    public void ForceFinishTask(TaskTypes type)
    {
        // Specific task finish behaviors
        GameObject[] objectsToDestroy = null;
        switch (type)
        {
            case TaskTypes.RemoveDebris:
                objectsToDestroy = GameObject.FindGameObjectsWithTag("Debris");
                break;
            case TaskTypes.RemoveBattery:
                objectsToDestroy = GameObject.FindGameObjectsWithTag("Battery");
                break;
            case TaskTypes.RemoveNails:
                objectsToDestroy = GameObject.FindGameObjectsWithTag("Nail");
                break;
            case TaskTypes.RemovePanel:
                objectsToDestroy = GameObject.FindGameObjectsWithTag("Panel");
                break;
            default:
                break;
        }

        // Deleting the objects marked for deletion
        if (objectsToDestroy != null)
        {
            foreach (GameObject objectToDestroy in objectsToDestroy)
            {
                if (objectToDestroy != null)
                {
                    Destroy(objectToDestroy);
                }
            }
        }

        // Completes the task
        foreach (Task task in _taskList)
        {
            if (task.taskType == type)
            {
                task.SetTaskProgress(task.taskGoalNumber);
            }
        }

        if (IsLevelComplete())
            CompleteLevel();
    }

    // Returns the number of tasks
    public int TaskCount()
    {
        return _taskList.Count;
    }

    // Returns the number of completed tasks
    public int FinishedTaskCount() {
        int i = 0;
        foreach (Task task in _taskList)
        {
            if (task.IsFinished())
                i++;
        }
        return i;
    }

    // Returns the type of tasks that are done through bits
    public int GetFinishedTaskBits()
    {
        int i = 0;

        foreach (Task task in _taskList)
        {
            if (task.IsFinished())
                i += (int) task.taskType;
        }

        return i;
    }

    // Checks if the level is completed (checking if all tasks are completed)
    public bool IsLevelComplete()
    {
        foreach (Task task in _taskList)
        {
            if (!task.IsFinished())
                return false;
        }

        return true;
    }

    // Increments the failure of a task
    public void FailedTask(TaskTypes type) 
    {
        
        foreach (Task task in _taskList)
        {
            if (task.taskType == type)
                task.IncrementFailure();
        }
    }

    // Displays amount of failure for a task
    public int DisplayFail(TaskTypes type)
    {
        foreach (Task task in _taskList)
        {
            if (task.taskType == type) {
                tracker = task.taskfail;
                Debug.Log($"Amount of fails: " + tracker);
            }
        }
        return tracker;
    }

    // Completes the level
    private void CompleteLevel()
    {
        _finishLevelMenu.SetActive(true);
        GameManager.instance.CompleteLevel();
    }
}

