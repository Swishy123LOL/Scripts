using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.Reflection;
using Cinemachine;
using UnityEditor;

public class TimelineActions : MonoBehaviour
{
    public enum ActionType
    {
        MoveX, MoveY, SwapPos, SetPos,
        RotX, RotY, RotZ,
        ScaleX, ScaleY,
        Flip, SwapSprite,
        TriggerDialogue, EndDialogue,
        EnableObject,
        LockCamera, FollowCamera, ShakeCamera,
        PlaySound, StopSound, StopAllSound,
        Fade, FSound,
        PlayAnimation, 
        LoadScene, LoadNextScene, LoadPreviousScene
    };

    public static ActionType actionType;

    #region Transform Actions

    public static void MoveX(string value){
        string[] values = SeparateAction(value);
        GameObject obj = TimelineControl.FindGameobject(values[0]);

        LeanTween.moveX(obj, obj.transform.position.x + float.Parse(values[1]), float.Parse(values[2]))
        .setEase( ( values.Length > 3 )? (LeanTweenType)int.Parse(values[3]) : LeanTweenType.notUsed );
    }

    public static void MoveY(string value){
        string[] values = SeparateAction(value);
        GameObject obj = TimelineControl.FindGameobject(values[0]);

        LeanTween.moveY(obj, obj.transform.position.y + float.Parse(values[1]), float.Parse(values[2]))
        .setEase( ( values.Length > 3 )? (LeanTweenType)int.Parse(values[3]) : LeanTweenType.notUsed );
    }

    public static void SetPos(string value)
    {
        string[] values = SeparateAction(value);
        GameObject obj = TimelineControl.FindGameobject(values[0]);

        LeanTween.move(obj, new Vector3(float.Parse(values[1]), float.Parse(values[2]), float.Parse(values[3])), float.Parse(values[4]))
        .setEase((values.Length > 5) ? (LeanTweenType)int.Parse(values[5]) : LeanTweenType.notUsed);
    }

    public static void SwapPos(string value){
        string[] values = SeparateAction(value);

        GameObject obj1 = TimelineControl.FindGameobject(values[0]);
        GameObject obj2 = TimelineControl.FindGameobject(values[1]);

        Vector3 oldpos = obj1.transform.position;

        obj1.transform.position = new Vector3(obj2.transform.position.x, obj2.transform.position.y,
        obj1.transform.position.z);
        obj2.transform.position = oldpos;
    }

    public static void RotX(string value){
        string[] values = SeparateAction(value);
        GameObject obj = TimelineControl.FindGameobject(values[0]);

        LeanTween.rotateX(obj, obj.transform.rotation.x + float.Parse(values[1]), float.Parse(values[2]))
        .setEase( ( values.Length > 3 )? (LeanTweenType)int.Parse(values[3]) : LeanTweenType.notUsed );
    }

    public static void RotY(string value){
        string[] values = SeparateAction(value);
        GameObject obj = TimelineControl.FindGameobject(values[0]);

        LeanTween.rotateY(obj, obj.transform.rotation.y + float.Parse(values[1]), float.Parse(values[2]))
        .setEase( ( values.Length > 3 )? (LeanTweenType)int.Parse(values[3]) : LeanTweenType.notUsed );
    }

    public static void RotZ(string value){
        string[] values = SeparateAction(value);
        GameObject obj = TimelineControl.FindGameobject(values[0]);

        LeanTween.rotateZ(obj, obj.transform.rotation.z + float.Parse(values[1]), float.Parse(values[2]))
        .setEase( ( values.Length > 3 )? (LeanTweenType)int.Parse(values[3]) : LeanTweenType.notUsed );
    }

    public static void ScaleX(string value){
        string[] values = SeparateAction(value);
        GameObject obj = TimelineControl.FindGameobject(values[0]);

        LeanTween.scaleX(obj, obj.transform.localScale.x + float.Parse(values[1]), float.Parse(values[2]))
        .setEase( ( values.Length > 3 )? (LeanTweenType)int.Parse(values[3]) : LeanTweenType.notUsed );
    }

    public static void ScaleY(string value){
        string[] values = SeparateAction(value);
        GameObject obj = TimelineControl.FindGameobject(values[0]);

        LeanTween.scaleY(obj, obj.transform.localScale.y + float.Parse(values[1]), float.Parse(values[2]))
        .setEase( ( values.Length > 3 )? (LeanTweenType)int.Parse(values[3]) : LeanTweenType.notUsed );
    }

    #endregion
    
    #region Components Action
    public static void Flip(string value){
        string[] values = SeparateAction(value);
        GameObject obj = TimelineControl.FindGameobject(values[0]);

        SpriteRenderer ren = obj.GetComponent<SpriteRenderer>();
        
        if (bool.Parse( values[1] ) == true) { ren.flipX = !ren.flipX; }
            else { ren.flipY = !ren.flipY; }
    }

    public static void SwapSprite(string value){
        string[] values = SeparateAction(value);
        GameObject obj1 = TimelineControl.FindGameobject(values[0]);
        SpriteRenderer ren1 = obj1.GetComponent<SpriteRenderer>();    

        GameObject obj2 = TimelineControl.FindGameobject(values[1]);
        SpriteRenderer ren2 = obj2.GetComponent<SpriteRenderer>();    

        ren1.sprite = ren2.sprite;
    }

    public static void TriggerDialogue(string value){
        string[] values = SeparateAction(value);
        if (values.Length < 2) values = HelpingHand.Append(values, "true");

        GameObject obj = TimelineControl.FindGameobject(values[0]);
        
        DialogueTrigger dialogue = obj.GetComponent<DialogueTrigger>();
        dialogue?.TriggerDialogue();

        if (bool.Parse(values[1])) FindObjectOfType<TimelineControl>().PauseTimeline(true);   
        else if (!bool.Parse(values[1])) FindObjectOfType<TimelineControl>().PauseTimeline(true, false);
    }

    public void EndDialogue(string value)
    {
        DialogueManager dialogue = FindObjectOfType<DialogueManager>();
        dialogue?.EndDialogue();
    }

    public void LockCamera(string value){
        FindObjectOfType<CinemachineVirtualCamera>().Follow = null;
    }

    public void FollowCamera(string value){
        GameObject obj = TimelineControl.FindGameobject(value);
        FindObjectOfType<CinemachineVirtualCamera>().Follow = obj.transform;
    }

    public void ShakeCamera(string value){
        CinemachineVirtualCamera cam = FindObjectOfType<CinemachineVirtualCamera>();
        CinemachineBasicMultiChannelPerlin noise = cam?.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        string[] values = SeparateAction(value);

        noise.m_FrequencyGain = float.Parse(values[0]);
        noise.m_AmplitudeGain = float.Parse(values[1]);
        LeanTween.value(noise.m_AmplitudeGain, 0, float.Parse(values[2]))
        .setOnUpdate( (float val)=> {noise.m_AmplitudeGain = val;} )
        .setEase( ( values.Length > 3 )? (LeanTweenType)int.Parse(values[3]) : LeanTweenType.notUsed );
    }

    public void PlaySound(string value){
        AudioManager audio = FindObjectOfType<AudioManager>();
        audio.Play(value);
    }

    public void StopSound(string value){
        AudioManager audio = FindObjectOfType<AudioManager>();
        audio.Stop(value);
    }

    public void StopAllSound(string value){
        AudioManager audio = FindObjectOfType<AudioManager>();
        foreach (var s in audio.audioSources)
        {
            s.Stop();
        }
    }

    public void Fade(string value){
        string[] values = SeparateAction(value);
        GameObject obj = TimelineControl.FindGameobject(values[0]);

        obj.GetComponent<SpriteRenderer>().material.color = (bool.Parse(values[1]))? Color.clear : Color.black;
        Color to = (bool.Parse(values[1]))? Color.black : Color.clear;

        LeanTween.color(obj, to, float.Parse(values[2]))
        .setEase( ( values.Length > 3 )? (LeanTweenType)int.Parse(values[3]) : LeanTweenType.notUsed );
    }

    public void FSound(string value){
        string[] values = SeparateAction(value);
        AudioManager audio = FindObjectOfType<AudioManager>();

        int to = (bool.Parse(values[1]))? 0 : 1;
        float start = audio.Find(values[0]).Volume;

        LeanTween.value(start, to, float.Parse(values[2]))
        .setOnUpdate( (float val)=> {audio.SetVolume(values[0], val);} )
        .setEase( ( values.Length > 3 )? (LeanTweenType)int.Parse(values[3]) : LeanTweenType.notUsed );
    }

    public void PlayAnimation(string value){
        GameObject obj = TimelineControl.FindGameobject(value);
        AnimationCreator anim = obj.GetComponent<AnimationCreator>();

        anim.PlayAnimation();
    }

    public void LoadPreviousScene(string value){
        int i = SceneManager.GetActiveScene().buildIndex - 1;
        SceneManager.LoadScene(i);
    }

    public void LoadNextScene(string value){
        int i = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(i);
    }

    public void LoadScene(string value){
        string[] values = SeparateAction(value);
        SceneManager.LoadScene(int.Parse(values[0]));
    }

    public void CloseGame(string value){
        Application.Quit();
    }

    #endregion
    
    public static void EnableObject(string value){
        string[] values = SeparateAction(value);
        GameObject obj = TimelineControl.FindInGameobject(values[0]);

        obj.SetActive(bool.Parse(values[1]));
    }

    public static string[] SeparateAction(string action){
        action = action.Replace(" ", "");
        string[] result = new string[Regex.Matches(action, ",").Count + 1];

        if (Regex.Matches(action, ",").Count == 0) {result[0] = action; return result;}
        result = action.Split(",".ToCharArray());

        return result;
    }

    public void InvokeAction(string name, string value){
        MethodInfo mi = this.GetType().GetMethod(name);
        mi.Invoke(this, new object[] { value });
    }
}
