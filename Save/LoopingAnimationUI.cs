using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class LoopingAnimationUI : MonoBehaviour
{
    public Image ReferingImage;
    public Transform Parent;
    public GameObject prefab;
    public float time;
    float scale;

    bool moved;
    public Color ImageColor;
    Image[] images = new Image[3];
    public bool allowChangeColor;
    void Start() {
        GameObject pre = Instantiate(prefab, Parent);
        images[0] = pre.GetComponent<Image>();

        RectTransform pretrans = pre.GetComponent<RectTransform>();
        RectTransform trans = ReferingImage.GetComponent<RectTransform>();

        scale = trans.rect.width;

        pretrans.localPosition = new Vector3 (pretrans.localPosition.x - scale, pretrans.localPosition.y);


        GameObject next = Instantiate(pre, Parent);
        images[1] = next.GetComponent<Image>();

        RectTransform nexttrans = next.GetComponent<RectTransform>();

        nexttrans.localPosition = new Vector3 (nexttrans.localPosition.x - 2 * scale, nexttrans.localPosition.y);


        GameObject mid = Instantiate(pre, Parent);
        images[2] = mid.GetComponent<Image>();

        RectTransform midtrans = mid.GetComponent<RectTransform>();

        midtrans = trans;

        ReferingImage.enabled = false;

        //Instantiated gameobjects' name can be a bit confusing
    }

    void Update() {
        if (allowChangeColor == true) {
            foreach (var image in images)
            {
                image.color = ImageColor;
            }
        }

        RectTransform trans = ReferingImage.GetComponent<RectTransform>();
        scale = trans.rect.width;

        trans.localPosition = new Vector3(trans.localPosition.x + (Time.deltaTime * scale) / time, trans.localPosition.y);

        if (moved == false) {
            StartCoroutine(UpdatePos(trans, scale, time));
        }
    }

    IEnumerator UpdatePos(RectTransform _transform, float scale, float time) {
        moved = true;

        yield return new WaitForSeconds(time);
        float posx = _transform.localPosition.x;

        _transform.localPosition = new Vector3(posx - scale, _transform.localPosition.y);
    
        moved = false;   
    }
}
