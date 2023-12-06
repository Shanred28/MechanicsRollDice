using System.Collections.Generic;
using TMPro;
using UnityEngine;

//����� ������������ ������� ��������� � UI. 
public class UI_CheckTest : MonoBehaviour
{
    [SerializeField] private TMP_Text _titleCheckText;
    [SerializeField] private TMP_Text _valueOfDifficultyText;

    [SerializeField] private RectTransform _parentIconBonus;
    [SerializeField] private UI_BonusIcone _bonusPref;
    [SerializeField] private Sprite[] _sprits;

    [SerializeField] private Transform _posPopupBonus;

    private Sprite _spriteBonus;
    private string _nameCheck;
    private List<UI_BonusIcone> _bonusList = new List<UI_BonusIcone>();

    public void SetTextCheck(CharacterStatsType type, int numb, int bonus)
    {
        for (int i = 0; i < _parentIconBonus.childCount; i++)
        {
            Destroy(_parentIconBonus.GetChild(i).gameObject);
        }

        switch (type) 
        {
            case CharacterStatsType.Str:
                _nameCheck = "����";
                _spriteBonus = _sprits[0];
                break;

            case CharacterStatsType.Dex:
                _nameCheck = "��������";
                _spriteBonus = _sprits[1];
                break;

            case CharacterStatsType.Con:
                _nameCheck = "�����������";
                _spriteBonus = _sprits[2];
                break;

            case CharacterStatsType.Int:
                _nameCheck = "����������";
                _spriteBonus = _sprits[3];
                break;

            case CharacterStatsType.Wis:              
                _nameCheck = "��������";
                _spriteBonus = _sprits[4];
                break;

            case CharacterStatsType.Cha:
                _nameCheck = "�������";
                _spriteBonus = _sprits[5];
                break;
        }

        _titleCheckText.text = "�������� " + _nameCheck;
        _valueOfDifficultyText.text = numb.ToString();

        if (bonus != 0 || _bonusList.Count != 0)
        {
            var go = Instantiate(_bonusPref, _parentIconBonus);        
            go.SetBonus(bonus, _nameCheck, _spriteBonus);
            _bonusList.Add(go);
        }
    }

    //��������� ���������� ���������� �������.
    public void ApplyBonuses()
    {
        for (int i = 0; i < _bonusList.Count; i++)
        {
            var go = Instantiate(((Resources.Load("Popup", typeof(GameObject))) as GameObject), _posPopupBonus);
            go.GetComponent<PopupBonus>().SetBonusValue((_bonusList[i].BonusVelue));
        }
    }
}
