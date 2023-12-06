using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public enum ResultCheck
{ 
    Win,
    CrtWin,
    Lose,
    CrtLose
}

//����� ��� �������� ���������� ��������, �������� �����. ��������� ���������� � UI_ResultPanel.
public class ResultTest : MonoBehaviour
{
    //�������� ����� ������.
    private const int MAX_DICE_NUMB = 19;

    public event UnityAction<ResultCheck, int> OnResultTest;

    [SerializeField] private StartTest _startTest;    
    [SerializeField] private BeginDiceTest _setActivePanels;
    [SerializeField] private UI_CheckTest _checkTest;

    [SerializeField] private float _waitBonusApplyVisual;


    private DiceController _diceController;
    private int _difficultClass;
    private int _bonusStat;
    private int _resultRollDice;

    private void Awake()
    {
        _diceController = _setActivePanels.DiceController;
    }

    private void Start()
    {
        OnCreateTest(_startTest.ClassDifficult, _startTest.BonusTestStat);

       _diceController = _setActivePanels.DiceController;
        _diceController.OnStartRollDice += OnStartRollDice;
        _diceController.OnEndRollDice += OnEndRollDice;

        _startTest.OnCreateTest += OnCreateTest;
    }

    private void OnDestroy()
    {
        _diceController.OnStartRollDice -= OnStartRollDice;
        _startTest.OnCreateTest -= OnCreateTest;
    }

    private void OnEndRollDice()
    {
        //�������� �� ����������� ������.
        if (_resultRollDice == MAX_DICE_NUMB || _resultRollDice == 0)
        {
            OnResultTest.Invoke(GetResutCheck(_resultRollDice), _resultRollDice);
        }
        //���� ��� ������, ������� ��� ���������� ��������.
        else if (_bonusStat == 0)
        {
            OnResultTest.Invoke(GetResutCheck(_resultRollDice), _resultRollDice);
        }
        else
        {
            SetBonusAndRollDice();
        }
    }

    //������� �����, ��������� �� ������������ � ������������ ����� �� ����� �������.
    private void SetBonusAndRollDice()
    {
        int finishResult = _resultRollDice + _bonusStat;

        if (finishResult <= MAX_DICE_NUMB)
        {
            _checkTest.ApplyBonuses();
            StartCoroutine(WaitSecond(_waitBonusApplyVisual, finishResult));
        }
        //��� �� ������ ������ 20 ��������, � �� ����� �������� ��������� ������. ���� ������ ������� ���������, ���� ����� 20.
        //TODO ������� �������� � ������ ����������. 
        else
        {
            OnResultTest.Invoke(GetResutCheck(finishResult), finishResult);
        }
    }

    private IEnumerator WaitSecond(float timeSecondWait, int finishResult)
    {
        yield return new WaitForSeconds(timeSecondWait);

        _diceController.RollDiceFinishNumb(finishResult);
        OnResultTest.Invoke(GetResutCheck(finishResult), finishResult);
    }

    //��� �������� ���������, ����� �������� ��� ��������.
    private void OnCreateTest(int difficultClassTest, int bonus)
    {
        _difficultClass = difficultClassTest;
        _bonusStat = bonus;
    }

    private void OnStartRollDice(int resultRollDice)
    {
        _resultRollDice = resultRollDice;
        _setActivePanels.DisableResultPanel();
    }

    //��������� ������.
    private ResultCheck GetResutCheck(int finishResult)
    {
        bool isWin;
        if (finishResult == 19)
        {
            isWin = true;
            _diceController.StartParticle(isWin);
            return ResultCheck.CrtWin;
        }
        if (finishResult == 0)
        {
            isWin = false;
            _diceController.StartParticle(isWin);
            return ResultCheck.CrtLose;
        }
        if (finishResult >= _difficultClass)
        {
            isWin = true;
            _diceController.StartParticle(isWin);
            return ResultCheck.Win;
        }
        else
        {
            isWin = false;
            _diceController.StartParticle(isWin);
            return ResultCheck.Lose;
        }
    }
}
