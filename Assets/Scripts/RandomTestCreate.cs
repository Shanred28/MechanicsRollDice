using System.Collections.Generic;
using UnityEngine;

//����� ��� ���������� �������� ���������. 
public class RandomTestCreate
{
    //��� �������� ��������.
    private const int MIN_DIFFICULT_CLASS = 8;
    //������������ �������� ��������.
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

    //��������� ��� ���������.
    private CharacterStatsType GetRandomCharacterStatsType()
    {
        int index = Random.Range(0, _enumCharacterStatsTypeList.Count);
        return _type = _enumCharacterStatsTypeList[index];
    }

    //��������� �������� ���������.
    private int GetClassDifficult()
    {
        int classDiffucult = Random.Range(MIN_DIFFICULT_CLASS, MAX_DIFFICULT_CLASS + 1);
        return classDiffucult;
    }
}
