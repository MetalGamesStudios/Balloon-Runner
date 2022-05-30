using System;
using MetalGamesSDK;

public class HapticsManager : Singleton<HapticsManager>
{
    public void SmallPop()
    {
        Vibration.VibratePop();
    }

    public void StrongHaptic()
    {
        Vibration.VibratePeek();
    }
}