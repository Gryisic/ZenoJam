using UnityEngine;
using UnityEngine.Playables;

namespace ZenoJam.Common
{
    public class SubtitleBehaviour : PlayableBehaviour 
    {
        public string Sentence { get; private set; }
        public Color Color { get; private set; }

        public void SetSubtitle(string sentence, Color color) 
        {
            Sentence = sentence;
            Color = color;
        }
    }
}