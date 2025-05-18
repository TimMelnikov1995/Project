using System;
using UnityEngine;

public partial class SfxSoundsPool
{
    [Serializable]
    public class Sound
    {
        public string Name;
        public AudioClip[] Clips;
    }
}