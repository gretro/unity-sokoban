using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveUI : MonoBehaviour
{
    public Sprite successSprite;
    public Sprite failSprite;
    
    public Image iconRenderer;
    public Image successRenderer;
    
    public IObjective objective;

    public void UpdateUI()
    {
        iconRenderer.sprite = objective.Visual;
        successRenderer.sprite = objective.IsOnObjective ? successSprite : failSprite;
    }
}
