using UnityEngine;
using UnityEngine.Audio;

namespace ZenoJam.Common
{
    public class VolumeSlider : MenuButton 
    {
        private AudioMixer _mixer;

        public void SetMixer(AudioMixer mixer) 
        {
            _mixer = mixer;
        }

        public void ChangeVolume(float value) 
        {
            _mixer.SetFloat("Master", Mathf.Lerp(-80, 0, value));
        }
    }
}
