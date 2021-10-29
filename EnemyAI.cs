using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Main Properties")]
    public float distance;
    public float maxDistance;
    public float speed;
    [Header("Scatter")]
    public float scatterRadius = 1;
    public float scatterTime = 1.5f;
    [Header("Other")]
    public float variation;
    [Header("Miscellaneous")]
    public Collider2D colliderIgnore;
    public LayerMask filter;
    [HideInInspector]
    public bool flip;
    float time, rtime;
    float rx, ry;
    float step;
    Player player;
    Coroutine scatterCor;
    PlayerMovement m_player;
    Vector2 lookDirection;
    Vector2 des;
    Vector2 scatterDes;
    Vector2 scatterLookDirection;
    Vector2 chaseDirection;
    Vector2 lastPlayerLocation;
    bool move;
    bool scatter;
    bool search;
    [Header("Force Stop")]
    public bool forceStop;
    public bool forceStopForceStop; //this is the solution to every dev problem
    public enum EnemyState{
        Idling,
        Chasing,
        Scattering,
        Searching
    };
    [HideInInspector]
    public EnemyState enemyState;

    Vector2 ReturnDirection(float radian, float radius = 1){
        return new Vector2(Mathf.Cos(radian) * radius, Mathf.Sin(radian) * radius);
    }
    float ReturnAngle(Vector2 origin, Vector2 direction){
        return Mathf.Asin(Mathf.Abs(direction.y - origin.y) / direction.magnitude);
    } 

    void Start(){
        player = FindObjectOfType<Player>();
        m_player = FindObjectOfType<PlayerMovement>();
        des = player.transform.position;
        maxDistance = (maxDistance < distance)? distance : maxDistance;

        speed += Random.Range(-variation, variation);
        distance += Random.Range(-variation, variation);
        chaseDirection = ReturnDirection(VectorAngle(des - (Vector2)transform.position, Vector2.right, true));
    }

    void Update(){
        if (!forceStop && !forceStopForceStop){
            rtime += Time.deltaTime;
            flip = transform.position.x < player.transform.position.x;

            if (rtime > 2){
                rtime = 0;
                rx = Random.Range(-variation, variation);
                ry = Random.Range(-variation, variation);
            }

            Vector2 vct1 = (Vector2)player.transform.position - (Vector2)transform.position;
            Vector2 vct2 = Vector2.right;

            chaseDirection = ReturnDirection(VectorAngle(vct1, vct2, true));

            if (!Physics2D.Raycast(transform.position, chaseDirection, vct1.magnitude, filter)){
                Debug.DrawRay(transform.position, chaseDirection * vct1.magnitude, Color.green);
                move = true;

                lastPlayerLocation = player.transform.position;
            }

            else if (Physics2D.Raycast(transform.position, chaseDirection, vct1.magnitude, filter)){
                Debug.DrawRay(transform.position, chaseDirection * vct1.magnitude, Color.red);
                move = false;

                float dis = Vector2.Distance(transform.position, player.transform.position);
                if (scatter == false) { search = true; }
            }
        }
    }

    float VectorAngle(Vector2 vct1, Vector2 vct2, bool radian = false){
        float sign = (vct2.y < vct1.y)? 1 : -1;
        float angle = Vector2.Angle(vct1, vct2) * sign;

        if (radian) { angle *= Mathf.Deg2Rad; }
        return angle;    
    }

    Vector2 RotateVector(Vector2 v, float radians)
    {
        var ca = Mathf.Cos(radians);
        var sa = Mathf.Sin(radians);
        return new Vector2(ca*v.x - sa*v.y, sa*v.x + ca*v.y);
    }

    void FixedUpdate(){
        //Fixed update is necessary for calculating physics and collisions
        if (!forceStop && !forceStopForceStop){
            Vector2 vct1 = (Vector2)player.transform.position - (Vector2)transform.position;
            Vector2 vct2 = Vector2.right;

            float dis = Vector2.Distance(transform.position, player.transform.position);

            if (dis > maxDistance){
                search = true;
            }

            if (dis > distance && dis < maxDistance){
                if (dis < maxDistance){
                    enemyState = EnemyState.Chasing;
                }
                if (m_player.IsMoving == true && search == false){
                    scatter = false;
                }

                if (scatter == false && move == true){
                    des = player.transform.position;
                    step = speed * Time.fixedDeltaTime;

                    lookDirection = (new Vector2(des.x + rx, des.y + ry) - (Vector2)transform.position).normalized;
                    transform.Translate(lookDirection * step);
                }
            }

            else if (dis < distance){
                if (scatter == false){
                    enemyState = EnemyState.Scattering;
                    scatter = true;

                    if (scatterCor != null)
                        StopCoroutine(scatterCor);

                    scatterCor = StartCoroutine(Scatter());
                    scatterDes = transform.position;
                }
            }

            if (scatter == true){
                scatterLookDirection = (scatterDes - (Vector2)transform.position).normalized;
                if (((Vector2)transform.position - scatterDes).magnitude > .1f){            //This help stopping jittering movement
                   transform.Translate(scatterLookDirection * Time.fixedDeltaTime);
                }
            }

            if (search){
                enemyState = EnemyState.Searching;
                Vector2 dir = (lastPlayerLocation - (Vector2)transform.position).normalized;
    
                if (((Vector2)transform.position - lastPlayerLocation).magnitude > .1f){
                    transform.Translate(dir * Time.deltaTime * speed / 2);
                }            

                else if (((Vector2)transform.position - lastPlayerLocation).magnitude < .1f){
                    enemyState = EnemyState.Idling;
                }

                if (!Physics2D.Raycast(transform.position, chaseDirection, vct1.magnitude, filter)){
                    //Found player, chase again
                    search = false;
                    move = true;
                }

            }
        }
    }

    IEnumerator Scatter(){
        Vector2 curr = transform.position;
        Vector2 playerPos = player.transform.position;
        while (scatter == true)
        {
            if (playerPos != (Vector2)player.transform.position){
                curr = transform.position;
                playerPos = player.transform.position;
            }
            Vector2 pre = ReturnDirection(Random.Range(0, 2*Mathf.PI), scatterRadius);
            scatterDes = curr + pre;

            yield return new WaitForSeconds(scatterTime);
        }
    }
}
