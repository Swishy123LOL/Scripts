using UnityEngine;

public class SaveCrystal : MonoBehaviour
{
    RippleEffect rippleEffect;
    public float interval;
    float time;
    void Start()
    {
        rippleEffect = FindObjectOfType<RippleEffect>();
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time > interval)
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector2 newPos = new Vector2(pos.x / Camera.main.pixelWidth, pos.y / Camera.main.pixelHeight);
            time = 0;
            if (newPos.x >= 0 && newPos.x <= 1 && newPos.y >= 0 && newPos.y <= 1)
            {
                rippleEffect.Emit(newPos);
            }
        }
    }
}
