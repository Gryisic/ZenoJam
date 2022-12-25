using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ZenoJam.Common
{
    [System.Serializable]
    public class Monologue  
    {
        [SerializeField] private List<MonologueData> _data;

        public IEnumerable<MonologueData> Data => _data;

        public bool CanBeListened => _data.Select(m => m.CanBeListened).First();

        public void SetListened() => _data.ForEach(m => m.SetListened());
    }
}