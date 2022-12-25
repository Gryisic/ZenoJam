using TMPro;
using UnityEngine;
using UnityEngine.Playables;

namespace ZenoJam.Common
{
    public class SubtitleTrackMixer : PlayableBehaviour 
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            TextMeshProUGUI text = playerData as TextMeshProUGUI;
            Color color = Color.white;
            string currentText = string.Empty;
            float currentAlpha = 0f;

            if (text == null)
                return;

            int inputCount = playable.GetInputCount();

            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);

                if (inputWeight > 0) 
                {
                    ScriptPlayable<SubtitleBehaviour> inputPlayable = (ScriptPlayable<SubtitleBehaviour>) playable.GetInput(i);

                    SubtitleBehaviour input = inputPlayable.GetBehaviour();
                    currentText = input.Sentence;
                    currentAlpha = inputWeight;
                    color = input.Color;
                }
            }

            text.text = currentText;
            text.color = new Color(color.r, color.g, color.b, currentAlpha);
        }
    }
}