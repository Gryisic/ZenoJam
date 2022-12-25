using UnityEngine;
using UnityEngine.Playables;

namespace ZenoJam.Common
{
    public class SubtitleClip : PlayableAsset
    {
        [SerializeField][TextArea(3, 10)] private string _sentence;

        [SerializeField] private Color _color = Color.white;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<SubtitleBehaviour>.Create(graph);

            SubtitleBehaviour behaviour = playable.GetBehaviour();
            behaviour.SetSubtitle(_sentence, _color);

            return playable;
        }
    }
}