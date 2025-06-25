using TMPro;
using UnityEngine;

[RequireComponent(typeof(AlienScore))]
public class AlienScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private AlienScore _score;

    private void Awake()
    {
        _score = GetComponent<AlienScore>();
    }

    private void OnEnable()
    {
        _score.Changed += UpdateUserInterface;
    }

    private void OnDisable()
    {
        _score.Changed -= UpdateUserInterface;
    }

    private void UpdateUserInterface(float value)
    {
        _text.text = value.ToString();
    }
}
