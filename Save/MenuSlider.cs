using UnityEngine;

public class MenuSlider : MonoBehaviour
{
    public RectTransform Middle;
    public RectTransform Bottom;
    public RectTransform RangeMin;
    public RectTransform RangeMax;
    public float Value;
    public float MaxValue = 1f;
    public float MinValue = 1f;
    public float DeltaValue = 3.2284f;
    float delta = 1f;

    Vector2 Pos;
    float scaleX;
    public float BottomOffset;
    float currentDelta = 0f;

    float _scaleX;
    float _posX;

    bool DefaultValueUpdate;

    void Start()
    {
        _scaleX = Middle.sizeDelta.x;
        _posX = Middle.localPosition.x;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            UpdateStat();
            Debug.Log("Stat Updated !");
        }
    }

    public void UpdateStat()
    {
        if (DefaultValueUpdate == false)
        {
            _scaleX = Middle.sizeDelta.x;
            _posX = Middle.localPosition.x;
            DefaultValueUpdate = true;
        }
        FindObjectOfType<PlayerStatBehaviour>().UpdateCount();
        delta = (Value / MaxValue);
        if (currentDelta != delta)
        {
            currentDelta = delta;
            //Calculate position and scale
            scaleX = _scaleX * delta;
            Pos = new Vector2(_posX - (_scaleX * (1f - delta) / 2), Middle.localPosition.y);
            //Apply position and scale 
            Middle.sizeDelta = new Vector2(scaleX, Middle.sizeDelta.y);
            Middle.localPosition = Pos;
            Bottom.localPosition = new Vector2(Middle.localPosition.x + Middle.sizeDelta.x / 2 + BottomOffset, Middle.localPosition.y);
            //Update the displaying value  
        }
    }

}
