using TMPro;
using UnityEngine;

public class TextView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private float pointsMultiplier = 100f;

    public void ChangeText()
    {
        textField.text = $"Score: {(int)(GameParameters.Instance.MaxReachedY * pointsMultiplier):d6}\nRecord: {(int)(GameParameters.Record * pointsMultiplier):d6}";
    }
}
