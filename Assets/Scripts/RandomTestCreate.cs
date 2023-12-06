using System.Collections.Generic;
using UnityEngine;

//Класс для рандомного создания испытаний. 
public class RandomTestCreate
{
    //Мин значения проверки.
    private const int MIN_DIFFICULT_CLASS = 8;
    //Максимальное значения проверки.
    private const int MAX_DIFFICULT_CLASS = 24;
    
    private CharacterStatsType _type;
    public CharacterStatsType TypeCharacterStatsType => _type;
    private int _classDifficult;
    public int ClassDifficult => _classDifficult;

    private List<CharacterStatsType> _enumCharacterStatsTypeList = new List<CharacterStatsType>() {
               CharacterStatsType.Str,
               CharacterStatsType.Dex,
               CharacterStatsType.Con,
               CharacterStatsType.Int,
               CharacterStatsType.Wis,
               CharacterStatsType.Cha
    };

    public RandomTestCreate()
    {
        _type = GetRandomCharacterStatsType();
        _classDifficult = GetClassDifficult();
    }

    //Рандомный тип испытания.
    private CharacterStatsType GetRandomCharacterStatsType()
    {
        int index = Random.Range(0, _enumCharacterStatsTypeList.Count);
        return _type = _enumCharacterStatsTypeList[index];
    }

    //Рандомная проверка испытания.
    private int GetClassDifficult()
    {
        int classDiffucult = Random.Range(MIN_DIFFICULT_CLASS, MAX_DIFFICULT_CLASS + 1);
        return classDiffucult;
    }
}
