using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Класс следящий за UI панелью, для изменений характеристик. 
public class UI_Character : MonoBehaviour
{
    public  Action<CharacterStatsType, int> OnCharacterStatsChange ;

    [SerializeField] private CharacterStatsType _statsType;
    private EventCollectorChangeStats _collectorChangeStats;

    private PlayerCharacter _playerCharacter;

    [SerializeField] private TMP_Text _valueCharacter;
    [SerializeField] private TMP_Text _valueBonus;

    [SerializeField] private Button _addValueStatButton;
    [SerializeField] private Button _removeValueStatButton;

    private int _stat;

    private void Start()
    {
        _playerCharacter = PlayerCharacter.Instance;
        _collectorChangeStats = GetComponentInParent<EventCollectorChangeStats>();
        foreach (var stats in _playerCharacter.CharStatsDictionary)
        {
            if (stats.Key == _statsType)
            {
                _stat = stats.Value;
                _valueCharacter.text = _stat.ToString();
                _valueBonus.text = _playerCharacter.BonusCharStats[_statsType].ToString();
            }
        }
    }

    public void AddValueStat()
    {
        foreach (var stats in _collectorChangeStats.PlayerCharacterDictionary)
        {
            if (stats.Key == _statsType)
            {
                int stat = _stat + 1;
                ChangeValueStat(stat);

                return;
            }
        }           
    }

    public void RemoveValueStat()
    {
        foreach (var stats in _collectorChangeStats.PlayerCharacterDictionary)
        {
            if (stats.Key == _statsType)
            {
                int stat = _stat - 1;      
                ChangeValueStat(stat);

                return;
            }
        }       
    }

    private void ChangeValueStat(int stat)
    {

        if (_playerCharacter.CanChange(stat) == true)
        {
            _stat = stat;
            OnCharacterStatsChange?.Invoke(_statsType, _stat);
            _valueCharacter.text = _stat.ToString();
            _valueBonus.text = _playerCharacter.BonusCharStats[_statsType].ToString();
        }

        if (stat > _stat)
        {
            _addValueStatButton.interactable = false;
        }
        else if (stat < _stat)
        {
            _removeValueStatButton.interactable = false;
        }
        else
        {
            _addValueStatButton.interactable = true;
            _removeValueStatButton.interactable = true;
        }
    }
}
