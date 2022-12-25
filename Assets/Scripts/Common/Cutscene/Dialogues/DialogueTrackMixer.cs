using UnityEngine.Playables;

namespace ZenoJam.Common
{
    public class DialogueTrackMixer : PlayableBehaviour 
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            DialogueTrigger trigger = playerData as DialogueTrigger;

            trigger.DialogueInitiated += ctx => { };

            trigger.Interact();
        }
    }
}