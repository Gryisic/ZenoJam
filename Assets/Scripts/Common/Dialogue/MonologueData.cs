using System.Collections.Generic;
using UnityEngine;

namespace ZenoJam.Common
{
    [System.Serializable]
    public class MonologueData 
    {
        [SerializeField] private string _name;
        [SerializeField][TextArea(3, 10)] private string[] _sentences;
        [SerializeField] private bool _canBeListened;

        public string Name => _name;
        public IEnumerable<string> Sentences => _sentences;
        public bool CanBeListened => _canBeListened;

        public void SetListened() => _canBeListened = false;
    }
}