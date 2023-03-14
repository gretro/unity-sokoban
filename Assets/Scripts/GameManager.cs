using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private IObjective[] objectives = new IObjective[0];
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
            this.objectives = FindObjectsOfType<MonoBehaviour>().OfType<IObjective>().ToArray();
            var objectiveNames = this.objectives.Select(obj => obj.ObjectiveName).ToArray();

            Debug.Log($"Objectives discovered: {string.Join(", ", objectiveNames)}");
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
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            Debug.Log($"Player moves: {playerMoves}. Objectives filled: {objectivesFilled}/{objectives.Length}");
        }
    }
}