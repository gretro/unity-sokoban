using System;
using Assets.Scripts;
using UnityEngine;

public class ObjectiveLayout : MonoBehaviour
{
    private const float POS_X = 50.0f;
    private const float GAP_Y = 25.0f;
    private const float INITIAL_GAP_Y = 50.0f;

    public GameObject objectiveUIPrefab;

    private ObjectiveUI[] objectiveUIs = Array.Empty<ObjectiveUI>();

    public void SetObjectives(IObjective[] objectives)
    {
        foreach (var objUI in objectiveUIs)   
        {
            Destroy(objUI);
        }

        objectiveUIs = new ObjectiveUI[objectives.Length];
        for (var i = 0; i < objectives.Length; i++)
        {
            var ui = CreateObjectiveUI(i, objectives[i]);
            objectiveUIs[i] = ui;
        }
    }

    public void UpdateUI()
    {
        foreach (var objUI in objectiveUIs)
        {
            objUI.UpdateUI();
        }
    }

    private ObjectiveUI CreateObjectiveUI(int index, IObjective obj)
    {
        var objective = Instantiate(objectiveUIPrefab, transform);
        var objTransform = objective.GetComponent<RectTransform>();

        var y = ((objTransform.rect.height + GAP_Y) * index + INITIAL_GAP_Y) * -1;
        Debug.Log($"Creating Objective UI for objective: {obj.ObjectiveName} at Y: {y}");

        objTransform.anchoredPosition = new Vector2(POS_X, y);

        var ui = objective.GetComponent<ObjectiveUI>();
        ui.objective = obj;
        
        ui.UpdateUI();
        
        return ui;
    }
}
