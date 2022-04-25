using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlatformData", menuName = "MENUNAME", order = 0)]
    public class PlatformInfo : ScriptableObject
    {
        private List<BubbleInfo> _bubbleInfos;
    }
}