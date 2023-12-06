using System;
using UnityEngine;
using UnityEngine.UI;

//Класс запускающий создания испытания. 
public class StartTest : MonoBehaviour
{
    public event Action<int, int> OnCreateTest;

    [SerializeField] private UI_CheckTest _uiCheckTest;
    private PlayerCharacter _characterStat;

    [SerializeField] private Button _buttonStart;
    [SerializeField] private Button _buttonNextTest;

    private RandomTestCreate _randomTestCreate;

    private int _classDifficult;
    public int ClassDifficult => _classDifficult;
    private int _bonusTestStat;
    public int BonusTestStat => _bonusTestStat;

    private void Start()
    {
        _characterStat = PlayerCharacter.Instance;
        _buttonStart.onClick.AddListener(OnClickButton);
        _buttonNextTest.onClick.AddListener(OnClickButton);
    }

    private void OnDestroy()
    {
        _buttonStart.onClick.RemoveListener(OnClickButton);
        _buttonNextTest.onClick.RemoveListener(OnClickButton);
    }

    private void OnClickButton()
    {
        _uiCheckTest.gameObject.SetActive(true);
        _randomTestCreate = new RandomTestCreate();

        _classDifficult = _randomTestCreate.ClassDifficult;
        _bonusTestStat = _characterStat.GetBonus(_randomTestCreate.TypeCharacterStatsType);
        

        _uiCheckTest.SetTextCheck(_randomTestCreate.TypeCharacterStatsType, _randomTestCreate.ClassDifficult, _bonusTestStat);

        OnCreateTest?.Invoke(_randomTestCreate.ClassDifficult, _bonusTestStat);
    }
}
