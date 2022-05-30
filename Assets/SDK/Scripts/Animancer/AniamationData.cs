using Animancer;
using UnityEngine;


[CreateAssetMenu(fileName = "AniamtionData", menuName = "Touseef/AniamitonData", order = 0)]
public class AniamationData : ScriptableObject
{
    public ClipTransition Run, Idle;
    public ClipTransition grabGun, putBackGun, limping;
    public ClipTransition hurt1, hurt2, hurt3, hurt4;
    public ClipTransition defeat, won;
}