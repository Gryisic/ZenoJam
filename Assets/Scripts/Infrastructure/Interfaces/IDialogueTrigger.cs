using System;
using ZenoJam.Common;

namespace ZenoJam.Infrastructure.Interfaces
{
    public interface IDialogueTrigger 
    {
        public event Action<DialogueProvider> DialogueInitiated;
    }
}
