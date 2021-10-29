using UnityEngine;
using System.Collections;
using TMPro;
using System;

public class PlayerCollision : MonoBehaviour
{
    public float invisibilityTime;     
    public float flashingTime;
    public GameObject damageText;
    public Material flash;
    public Action OnHit;
    SpriteRenderer ren;
    Material normal;
    bool vulnerable;
    
    void Start(){
        ren = GetComponent<SpriteRenderer>();
        normal = ren.material;
    }
    void InstantiateDamage(int dmg){
        GameObject obj = Instantiate(damageText, GameObject.Find("Enemy's Health").transform);
        RectTransform rect = obj.GetComponent<RectTransform>();
        TextMeshProUGUI txt = obj.GetComponentInChildren<TextMeshProUGUI>();

        txt.text = dmg.ToString();

        rect.position = new Vector3(transform.position.x, transform.position.y + UnityEngine.Random.Range(-.5f, .5f), transform.position.z);
    }

    IEnumerator Hurt(){
        StartCoroutine(Wait(invisibilityTime));
        FindObjectOfType<AudioManager>().Play("Hurt2");

        ren.material = flash;
        yield return new WaitForSeconds(.1f);
        ren.material = normal;
    }

    IEnumerator Wait(float time){
        vulnerable = true;
        StartCoroutine(InvisibleWait(flashingTime));
        yield return new WaitForSeconds(time);
        vulnerable = false;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.layer == 13 && vulnerable == false){
            BulletStat stat = collider.GetComponent<BulletStat>();
            Player player = FindObjectOfType<Player>();
            OnHit?.Invoke();

            if (stat != null) { 
                player.PlayerHealth -= stat.damage;
                FindObjectOfType<HealthBar_Player>().text.text = player.PlayerHealth.ToString();
                FindObjectOfType<HealthBar_Player>().TakeDamage(stat.damage);
            }
            StartCoroutine(Hurt());
            InstantiateDamage(stat.damage);

            stat.onCollidePlayer?.Invoke();
        }
    }

    IEnumerator InvisibleWait(float time){
        if (vulnerable == true){
            int num = Mathf.FloorToInt(invisibilityTime / time / 2);
            for (int i = num; i > 0; i--)
            {
                LeanTween.color(gameObject, new Color(1, 1, 1 , 0), time);
                yield return new WaitForSeconds(time);
                LeanTween.color(gameObject, Color.white, time);
                yield return new WaitForSeconds(time);
            }
        }
    }
}
