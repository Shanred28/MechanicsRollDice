using System;
using System.Collections.Generic;
using UnityEngine;

// ласс след€щий за изменени€ми характеристик
public class EventCollectorChangeStats : MonoBehaviour
{
    public event Action<CharacterStatsType, int> ChangeStats;
    private PlayerCharacter _playerCharacter;
    public  Dictionary<CharacterStatsType, int>  PlayerCharacterDictionary => _playerCharacter.CharStatsDictionary;

    private UI_Character[] _uiCharacters;

    private void Start()
    {
        _playerCharacter = PlayerCharacter.Instance;
        _uiCharacters = GetComponentsInChildren<UI_Character>();
        for (int i = 0; i < _uiCharacters.Length; i++)
        {
            _uiCharacters[i].OnCharacterStatsChange += OnCharacterStatsChange;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _uiCharacters.Length; i++)
        {
            _uiCharacters[i].OnCharacterStatsChange -= OnCharacterStatsChange;
        }
    }

    private void OnCharacterStatsChange(CharacterStatsType type, int stats)
    {
        ChangeStats.Invoke(type, stats);
    }
}
