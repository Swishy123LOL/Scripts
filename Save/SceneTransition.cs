using BayatGames.SaveGameFree;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public int SceneToLoad;
    public int Spawn;
    Animator animator;
    BoxCollider2D playerCollider;
    public BoxCollider2D boxCollider;
    public enum Direction
    {
        Any,
        Up,
        Down,
        Left,
        Right,
        Ignore
    }

    public Direction direction;
    Vector2 dir;
    float disX;
    float disY;
    bool facing;
    bool transition;
    KeyCode key1;
    KeyCode key2;

    void Awake()
    {
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode loadSceneMode) =>
        {
            Player plyr = FindObjectOfType<Player>();
            if (GameObject.Find("Spawn1") && plyr && transition)
            {
                plyr.transform.position = GameObject.Find("Spawn" + SaveManager.Spawn).transform.position;
            }
        };
    }

    void Start()
    {
        disX = FindObjectOfType<Player>().GetComponent<BoxCollider2D>().bounds.extents.x;
        disY = FindObjectOfType<Player>().GetComponent<BoxCollider2D>().bounds.extents.y;

        animator = GameObject.FindGameObjectWithTag("DialougeManag").GetComponent<Animator>();
        if (boxCollider == null) boxCollider = GetComponent<BoxCollider2D>();
        playerCollider = FindObjectOfType<Player>().GetComponent<BoxCollider2D>();

        StartCoroutine(Enable());

        if (direction == Direction.Up) { dir = new Vector2(0, disY); facing = true; key1 = KeyCode.W; key2 = KeyCode.UpArrow; }
        if (direction == Direction.Down) { dir = new Vector2(0, -disY); facing = true; key1 = KeyCode.S; key2 = KeyCode.DownArrow; }

        if (direction == Direction.Left) { dir = new Vector2(-disX, 0); facing = false; key1 = KeyCode.A; key2 = KeyCode.LeftArrow; }
        if (direction == Direction.Right) { dir = new Vector2(disX, 0); facing = false; key1 = KeyCode.D; key2 = KeyCode.RightArrow; }
    
    }

    public void TransitionScene(){
        if (facingCheck() == true || Input.GetKeyDown(key1) || Input.GetKeyDown(key2)) {
            transition = true;
            SaveManager.Spawn = Spawn;
            animator.SetBool("Bool", true);

            StartCoroutine(WaitToLoad(SceneToLoad));
        }   
    }

    void Update(){
        if (boxCollider != null){
            if (boxCollider.IsTouching(playerCollider)){
                TransitionScene();
            }
        }
    }

    IEnumerator WaitToLoad(int s)
    {
        FindObjectOfType<PlayerMovement>().Stop = true;

        yield return new WaitForSeconds(0.51f);

        SceneManager.LoadScene(s);
    }

    IEnumerator Enable() {
        if (boxCollider) boxCollider.enabled = false;

        yield return new WaitForSeconds(.1f);

        if (boxCollider) boxCollider.enabled = true;
    }

    bool facingCheck(){
        if (direction == Direction.Any) { return true; }
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();

        if (playerMovement.IsMoving == false){
            return false;
        }

        if (direction == Direction.Left) {
            return playerMovement.facingX == true;
        }

        if (direction == Direction.Right) {
            return playerMovement.facingX == false;
        }

        if (direction == Direction.Down) {
            return playerMovement.facingY == true;
        }

        if (direction == Direction.Up) {
            return playerMovement.facingY == false;
        }

        return false;
    }
}
