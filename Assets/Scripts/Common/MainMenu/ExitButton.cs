using System;

namespace ZenoJam.Common
{
    public class ExitButton : MenuButton 
    {
        public event Action Clicked;

        public void Execute() => Clicked?.Invoke();
    }
}
