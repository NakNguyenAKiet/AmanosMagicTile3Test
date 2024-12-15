using System;
using TTHUnityBase.Base.DesignPattern;

public class GameEventManager
{
    public event Action<bool, Tile> OnGetPoint;
    public void GetPoint(bool isCenter, Tile Tile) => OnGetPoint?.Invoke(isCenter, Tile);

    public event Action OnEndGame;
    public void EndGame()
    {
        OnEndGame?.Invoke();
    }

    public event Action<string> OnLosingGame;
    public void LosingGame(string message)
    {
        OnLosingGame?.Invoke(message);
    }
}

public class MyGameEvent: Singleton <GameEventManager>
{

}
