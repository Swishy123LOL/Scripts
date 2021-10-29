using UnityEngine;
using UnityEngine.UI;

public class Computer_FolderDisplacement : MonoBehaviour
{
    [Header("Position")]
    public RectTransform LeftMargin;
    public RectTransform RightMargin;
    [Space]
    public RectTransform TopMargin;
    public RectTransform BottomMargin;
    [Space]
    public RectTransform StartPos;
    [Space]
    public float XOffset;
    public float YOffset;

    [Space]
    [Header("Folder")]
    public Computer_FolderProperties[] folderProperties;
    public int SpaceBetweenFolderX;
    public int SpaceBetweenFolderY;

    [Space]
    [Header("Folder Gameobject")]
    public RectTransform Parrent;
    public GameObject FolderPrefab;
    public GameObject ColliderPrefab;

    //Hidden
    float DisBetweenMarginX;
    float MarginDisX;

    float DisBetweenMarginY;
    float MarginDisY;

    Vector2 CurrMousePos;

    Vector2[] FolderPosMax;
    Vector2[] FolderPosMin;

    bool Changed;

    void Start()
    {
        FolderPosMax = new Vector2[folderProperties.Length];
        FolderPosMin = new Vector2[folderProperties.Length];

        DisBetweenMarginX = RightMargin.position.x - LeftMargin.position.x;
        MarginDisX = (DisBetweenMarginX + 2) / SpaceBetweenFolderX;

        DisBetweenMarginY = TopMargin.position.y - BottomMargin.position.y;
        MarginDisY = (DisBetweenMarginY + 2) / SpaceBetweenFolderY;

        for (int i = 0; i < folderProperties.Length; i++)
        {
            GameObject gameObject = Instantiate(FolderPrefab, Parrent);
            Vector3 pos = new Vector3(MarginDisX * (i + 1) + XOffset, -MarginDisY * folderProperties[i].Column + YOffset);

            gameObject.name = folderProperties[i].Name;
            gameObject.GetComponent<RectTransform>().position = pos + StartPos.transform.position;

            Image image = gameObject.GetComponent<Image>();
            image.sprite = folderProperties[i].Icon;
            image.SetNativeSize();

            Button button = gameObject.GetComponent<Button>();
            SpriteState spriteState;

            spriteState.highlightedSprite = folderProperties[i].HoverIcon;
            spriteState.selectedSprite = folderProperties[i].Icon;
            spriteState.pressedSprite = folderProperties[i].HoverIcon;
            spriteState.disabledSprite = folderProperties[i].Icon;

            button.spriteState = spriteState;
            button.onClick = GetComponent<Computer_WindowBehaviour>().OnWindowOpen;
        }

    }

    void Update()
    {
        CurrMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        for (int i = 0; i < folderProperties.Length; i++)
        {
            GameObject gameObject = GameObject.Find(folderProperties[i].Name);
            Image image = gameObject.GetComponent<Image>();

            if (IsWithin(CurrMousePos.x, FolderPosMin[i].x, FolderPosMax[i].x) == true && IsWithin(CurrMousePos.y, FolderPosMin[i].y, FolderPosMax[i].y) == true && Changed == false)
            {
                image.sprite = folderProperties[i].HoverIcon;
                image.SetNativeSize();

                Changed = true;
            }

            else
            {
                image.sprite = folderProperties[i].Icon;
                image.SetNativeSize();

                Changed = false;
            }
        }
    }

    public bool IsWithin(float value, float minimum, float maximum)
    {
        return value >= minimum && value <= maximum;
    }

}
