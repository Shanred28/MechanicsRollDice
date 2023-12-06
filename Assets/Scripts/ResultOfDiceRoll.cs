using UnityEngine;

//Класс для получения рандомного результата броска. Если успею реализую тут кармические кубики, с разным шансом выпадения значений. По аналогии с БГ3.
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
