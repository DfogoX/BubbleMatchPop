using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "LevelTest", order = 0)]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private List<PlatformInfo> _platformsInfo;

        public List<PlatformInfo> GetPlatformsInfo()
        {
            return _platformsInfo;
        }

        public void SetPlatformsInfo(List<PlatformInfo> platforms)
        {
            _platformsInfo = new List<PlatformInfo>(platforms);
        }
    }
    

}