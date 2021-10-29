using UnityEngine;

[RequireComponent(typeof(AnimationCreator))]
public class AnimationController : MonoBehaviour
{
    public AnimationCreator[] animations;
    public int currentIndex;
    public bool playOnStart;
    [HideInInspector]
    public int frame;
    int preIndex;
    void Start()
    {
        if (playOnStart)
            animations[currentIndex].PlayAnimation();
        else
            currentIndex = -1;

        preIndex = currentIndex;
    }

    void Update()
    {
        if (preIndex != currentIndex)
        {
            foreach (var animation in animations)
            {
                animation.StopAnimation();
            }

            if (currentIndex >= 0)
                animations[currentIndex].PlayAnimation();

            preIndex = currentIndex;
        }

        if (currentIndex >= 0)
            frame = animations[currentIndex].i;
    }
}
