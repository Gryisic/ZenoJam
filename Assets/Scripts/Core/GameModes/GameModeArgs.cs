using System;
using UnityEngine.Playables;
using ZenoJam.Common;
using static ZenoJam.Utils.Enums;

namespace ZenoJam.Core
{
    public class GameModeArgs : EventArgs
    {
        public GameModeType NewMode { get; }
        public DialogueProvider DialogueProvider { get; }
        public CutsceneProvider CutsceneProvider { get; }

        public GameModeArgs(GameModeType newMode, DialogueProvider dialogueProvider, CutsceneProvider cutsceneProvider) 
        {
            NewMode = newMode;
            DialogueProvider = dialogueProvider;
            CutsceneProvider = cutsceneProvider;
        }

        public GameModeArgs(GameModeType newMode) : this(newMode, null, null) { }
        public GameModeArgs(GameModeType newMode, DialogueProvider dialogueProvider) : this(newMode, dialogueProvider, null) { }
        public GameModeArgs(GameModeType newMode, CutsceneProvider cutsceneProvider) : this(newMode, null, cutsceneProvider) { }
    }
}