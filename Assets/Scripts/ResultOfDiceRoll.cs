using UnityEngine;

//����� ��� ��������� ���������� ���������� ������. ���� ����� �������� ��� ����������� ������, � ������ ������ ��������� ��������. �� �������� � ��3.
public class ResultOfDiceRoll
{
    private const int AMOUNT_SIDE = 20;

    //TODO
    //private bool _isCarmaDice = false;

    public int ResultDiceRoll()
    {        
        return Random.Range(0, AMOUNT_SIDE);
    }
}
