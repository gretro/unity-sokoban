using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public ObjectiveLayout objectiveLayout;
        
        private IObjective[] objectives = Array.Empty<IObjective>();
        private bool gameWon = false;
        private int playerMoves = 0;

        private int objectivesFilled = 0;

        public UnityEvent GameWon;

        // Use this for initialization
        void Start()
        {
            DiscoverObjectives();
        }

        // Update is called once per frame
        void Update()
        {
            if (!gameWon)
            {
                var updatedObjectivesFilled = objectives.Count(objective => objective.IsOnObjective);
                
                if (objectivesFilled != updatedObjectivesFilled)
                {
                    objectivesFilled = updatedObjectivesFilled;

                    UpdateUI();

                    gameWon = updatedObjectivesFilled == objectives.Length;
                    if (gameWon)
                    {
                        GameWon.Invoke();
                        Debug.Log("Game won");
                    }
                }
                
            }
        }

        public void DiscoverObjectives()
        {
            objectives = FindObjectsOfType<MonoBehaviour>().OfType<IObjective>().ToArray();
            objectiveLayout.SetObjectives(objectives);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void OnPlayerMoved(Vector2Int movement)
        {
            if (movement != Vector2Int.zero)
            {
                playerMoves++;
            }
            
            UpdateUI();
        }

        private void UpdateUI()
        {
            objectiveLayout.UpdateUI(playerMoves);
        }
    }
}