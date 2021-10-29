using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class LoopingTile : MonoBehaviour
{
    public int loopCount;
    public bool direction;
    SpriteRenderer ren;
    Component[] comps;

    void Start(){
        //TOMMOROW TASK
        //Instead of making a flexible script that won't work
        //Make the "Attack Indicator" a seperate script instead
        
        ren = GetComponent<SpriteRenderer>();
        int dir = (direction)? 1 : -1;

        float width = ren.size.x;

        for (int i = 0; i < loopCount; i++)
        {
            GameObject obj = new GameObject(transform.name);
            obj.transform.parent = transform;
            obj.transform.position = new Vector2(transform.position.x + width * (i+1) * dir, transform.position.y);
            
            comps = new Component[GetComponents<Component>().Length - 2];
            int o = 0;

            foreach (var comp in GetComponents<Component>())
            {
                if (comp.GetType().Name != "Transform" && comp.GetType().Name != "LoopingTile"){
                    comps[o] = comp;
                    o++;
                }
            }

            foreach (var comp in comps)
            {
                Component copy = obj.AddComponent(comp.GetType());
                HelpingHand.GetCopyOf(copy, comp);
                //DAMN IT HELPING HAND DIDN'T WORK 
                //why
            }
        }
    }
}
