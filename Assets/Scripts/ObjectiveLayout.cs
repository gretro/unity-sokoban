using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ObjectiveLayout : MonoBehaviour
{
    public UnityEvent RestartLevel;
    
    private UIDocument uiDocument;
    private VisualElement objectivePanel;

    private Label PlayerMoves => uiDocument.rootVisualElement.Q<Label>("PlayerMoves");
    private Button RestartLevelBtn => uiDocument.rootVisualElement.Q<Button>("RestartLevelBtn");

    private ObjectiveElement[] objectiveElements;

    private void Start()
    {
        uiDocument = GetComponent<UIDocument>();

        objectivePanel = uiDocument.rootVisualElement.Q<VisualElement>("ObjectivePanel");
        RestartLevelBtn.clicked += RestartLevel.Invoke;
    }

    public void SetObjectives(IEnumerable<IObjective> objectives)
    {
        objectivePanel.Clear();

        objectiveElements = objectives.Select(objective =>
        {
            var objectiveElement = new ObjectiveElement(objective);
            objectivePanel.Add(objectiveElement);

            return objectiveElement;
        }).ToArray();
    }

    public void UpdateUI(int playerMoves)
    {
        PlayerMoves.text = playerMoves.ToString();
        
        if (objectiveElements == null)
        {
            return;
        }
        
        foreach (var objectiveElement in objectiveElements)
        {
            objectiveElement.UpdateUI();
        }
    }
}
