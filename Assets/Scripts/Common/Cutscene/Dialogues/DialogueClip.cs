using UnityEngine;
using UnityEngine.Playables;

namespace ZenoJam.Common
{
    public class DialogueClip : PlayableAsset
    {
        [SerializeField] private DialogueProvider _provider;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<DialogueBehaviour>.Create(graph);

            DialogueBehaviour behaviour = playable.GetBehaviour();

            return playable;
        }
    }
}