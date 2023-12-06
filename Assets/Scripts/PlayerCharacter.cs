using System.Collections.Generic;
using UnityEngine;

public enum CharacterStatsType
{ 
    Str,
    Dex,
    Con,
    Int,
    Wis,
    Cha
}

// ласс хран€щий характеристики и бонусу.“ак же провер€ет возможно ли изменени€, записывает бонусы. 
public class PlayerCharacter : MonoBehaviour
{
    //„то бы сделать закрытую систему требуетс€ доступ к классу с характеристиками. 
    public static PlayerCharacter Instance;

    private const int BONUS_STAT = 10;

    [SerializeField] private EventCollectorChangeStats _changeStats;

    [SerializeField] private int _str = 8;
    [SerializeField] private int _dex = 8;
    [SerializeField] private int _con = 8;
    [SerializeField] private int _int = 8;
    [SerializeField] private int _wis = 8;
    [SerializeField] private int _cha = 8;

    [SerializeField] private int _lowerValueStat = 7;
    [SerializeField] private int _upperValueStat = 21;

    private Dictionary<CharacterStatsType, int> _dictCharStats = new Dictionary<CharacterStatsType, int>();
    public  Dictionary<CharacterStatsType, int> CharStatsDictionary => _dictCharStats;
    #region Init Dict, List
    private Dictionary<CharacterStatsType, int> _bonusCharStats = new Dictionary<CharacterStatsType, int>() {
        [CharacterStatsType.Str] = -1,
        [CharacterStatsType.Dex] = -1,
        [CharacterStatsType.Con] = -1,
        [CharacterStatsType.Int] = -1,
        [CharacterStatsType.Wis] = -1,
        [CharacterStatsType.Cha] = -1,
    };

    private List<CharacterStatsType> characterStatsTypes = new List<CharacterStatsType>() {
          CharacterStatsType.Str,
          CharacterStatsType.Dex,
          CharacterStatsType.Con,
          CharacterStatsType.Int,
          CharacterStatsType.Wis,
          CharacterStatsType.Cha,
    };
    #endregion
    public Dictionary<CharacterStatsType, int> BonusCharStats => _bonusCharStats;

    private void Awake()
    {
        Instance = this;

        _dictCharStats.Add( CharacterStatsType.Str, _str); 
        _dictCharStats.Add( CharacterStatsType.Dex, _dex); 
        _dictCharStats.Add( CharacterStatsType.Con, _con); 
        _dictCharStats.Add( CharacterStatsType.Int, _int); 
        _dictCharStats.Add( CharacterStatsType.Wis, _wis); 
        _dictCharStats.Add( CharacterStatsType.Cha, _cha);

        for (int i = 0; i < characterStatsTypes.Count; i++)
        {
            _bonusCharStats[characterStatsTypes[i]] = SetBonus(_dictCharStats[characterStatsTypes[i]]);
        }
    }
    private void Start()
    {
        _changeStats.ChangeStats += OnChangeStats;
    }

    private void OnDestroy() 
    {
        _changeStats.ChangeStats -= OnChangeStats;
    }

    //ѕровер€ет, можно ли изменить значение характеристики. 
    public bool CanChange(int value)
    {
        if (value > _lowerValueStat && value < _upperValueStat)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //«аписывает изменени€ характеристики. “ак же провер€ет нужно ли изменить бонус.
    private void OnChangeStats(CharacterStatsType type, int value)
    {
        _dictCharStats[type] = value;
        
        for (int i = 0; i < _bonusCharStats.Count; i++)
        {
            _bonusCharStats[type] = SetBonus(value);
        }
    }

    //”станавливает значение бонуса.
    private int SetBonus( int value)
    {
        return  (value - BONUS_STAT) / 2;
    }

    //ќтдает значени€ бонуса.
    public int GetBonus(CharacterStatsType type)
    {
        int bonus;
        foreach (var statBonus in _bonusCharStats)
        {
            if (statBonus.Key == type)
            {
                bonus = statBonus.Value;
                return bonus;
            }
        }

        return 0;
    }
}
