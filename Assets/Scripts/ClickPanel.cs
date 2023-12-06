using UnityEngine;

//Класс для кнопки запуска вращения кубика.
public class ClickPanel : MonoBehaviour
{
    [SerializeField] private BeginDiceTest _setActivePanels;

    private DiceController _diceController;

    private void Start()
    {
        _diceController = _setActivePanels.DiceController;
    }
    public void OnClick()
    {        
        _diceController.StartRollDice();
    }
}
