using Assets.Scripts;
using UnityEditor;
using UnityEngine.UIElements;

public class ObjectiveElement : VisualElement
{
   public new class UxmlFactory: UxmlFactory<ObjectiveElement> {}
   
   private VisualElement ObjectiveImage => this.Q("ObjectiveImage");
   private VisualElement ObjectiveSuccess => this.Q("ObjectiveSuccess");
   private VisualElement ObjectivePending => this.Q("ObjectivePending");

   private readonly IObjective objective = null;

   public ObjectiveElement()
   {
      var template = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI/ObjectiveElement.uxml");
      template.CloneTree(this);
   }

   public ObjectiveElement(IObjective objective)
      :this()
   {
      this.objective = objective;
      
      UpdateUI();
   }

   public void UpdateUI()
   {
      ObjectiveImage.style.backgroundImage = new StyleBackground(objective.Visual);

      var success = objective?.IsOnObjective ?? false;
      var pendingDisplay = success ? DisplayStyle.None : DisplayStyle.Flex;
      var successDisplay = success ? DisplayStyle.Flex : DisplayStyle.None;

      ObjectivePending.style.display = new StyleEnum<DisplayStyle>(pendingDisplay);
      ObjectiveSuccess.style.display = new StyleEnum<DisplayStyle>(successDisplay);
   }
}
