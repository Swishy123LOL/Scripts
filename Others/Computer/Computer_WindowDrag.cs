using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Computer_WindowDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //World record for number of interfaces in one damn screen!
    public RectTransform m_transform;
    public Image image;
    public Sprite DragSprite;
    Sprite NormalSprite;

    bool IsDrag;

    Vector3 offset;

    public static bool Drag = true;

    void Start() {
        NormalSprite = image.sprite;
    }
    public void OnBeginDrag(PointerEventData eventData){
        if (Drag) {
            IsDrag = true;

            offset = m_transform.position - Input.mousePosition;

            image.sprite = DragSprite;
            image.SetNativeSize();
        }
    }

    public void _OnDrag() {
        Vector3 pos = Input.mousePosition + offset;

        m_transform.position = pos;
    }

    public void OnDrag(PointerEventData eventData) {
        //Fake method, Unity actually requires OnDrag in order for OnBeginDrag to work
    }

    public void OnEndDrag(PointerEventData eventData) {
        IsDrag = false;

        image.sprite = NormalSprite;
        image.SetNativeSize();
    }

    public void OnPointerEnter(PointerEventData eventData){
        if (Drag) {
            image.sprite = DragSprite;
            image.SetNativeSize();
        }
    }

    public void OnPointerExit(PointerEventData eventData){
        image.sprite = NormalSprite;
        image.SetNativeSize();
    }

    public void OnPointerClick(PointerEventData eventData) {
        IsDrag = false;
    }

    void Update() {
        if (IsDrag && Drag) {
            _OnDrag();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            IsDrag = false;
        }
    }
}
