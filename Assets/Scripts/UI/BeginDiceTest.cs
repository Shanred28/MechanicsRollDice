using UnityEngine;

// ласс дл€ управлени€ми панел€ми в UI и запуска испытани€.
public class BeginDiceTest : MonoBehaviour
{
    [SerializeField] private RectTransform _menuCharacter;
    [SerializeField] private RectTransform _bgRollDice;
    [SerializeField] private RectTransform _startButton;
    [SerializeField] private RectTransform _rollDicePanel;
    [SerializeField] private UI_ResultPanel _resultPanel;

    private DiceController _diceController;
    public DiceController DiceController => _diceController;

    public void OnButtonSetCharactComp()
    {
        _menuCharacter.gameObject.SetActive(false);
        _bgRollDice.gameObject.SetActive(true);       
        _startButton.gameObject.SetActive(true);
    }

    public void StartTesting()
    {
        var go = Instantiate(Resources.Load("DiceSystem", typeof(GameObject))) as GameObject;

        _diceController = go.GetComponent<DiceController>();
        _rollDicePanel.gameObject.SetActive(true);
    }

    public void DisableResultPanel()
    {
        _resultPanel.DisableResultTest();
    }
}
