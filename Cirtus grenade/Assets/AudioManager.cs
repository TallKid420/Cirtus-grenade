using UnityEngine;
using System;

[CreateAssetMenu(fileName = "AudioTrack", menuName = "ScriptableObjects/Audio", order = 1)]
public class AudioManager : ScriptableObject
{
    public AudioSource AudioTrack;
    public String Name;
    public bool ThreeDimentionalSound;
}
