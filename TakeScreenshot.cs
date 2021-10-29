using UnityEngine;
using System.Collections;

public class TakeScreenshot : MonoBehaviour
{
    int resWidth;
    int resHeight;

    public static string ScreenShotName(int width, int height)
    {
        return string.Format("{0}/Screenshots/screen_{1}x{2}_{3}.png",
                             Application.dataPath,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            resWidth = Screen.width;
            resHeight = Screen.height;

            ScreenCapture.CaptureScreenshot(ScreenShotName(resWidth, resHeight), 4);
        }
    }
}

