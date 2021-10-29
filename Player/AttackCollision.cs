using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    public bool forceStop;

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.layer == 9 && !forceStop){
            EnemyCollision col = collider.GetComponent<EnemyCollision>();
            PlayerAttack.AttackProperties stat = FindObjectOfType<PlayerAttack>().attacks[FindObjectOfType<PlayerAttack>().currentAttackIndex - 1];
            PlayerAttack attack = FindObjectOfType<PlayerAttack>();
            EnemyStat eStat = collider.GetComponent<EnemyStat>();

            float d = stat.damage;
            int r = Random.Range(0, 100);
            bool c = false;
            bool b = false;
            if (r < stat.critChance)
            {
                d *= 2;
                c = true;
            }

            if (eStat.Defend / d > 0.9f)
            {
                d *= 0.1f;
                b = true;
            }

            else if (eStat.Defend / d < 0.9f)
                d *=  1 - eStat.Defend / d;

            d += Mathf.FloorToInt(Random.Range(-stat.damage * 0.1f, stat.damage * 0.1f));

            attack.lastDamage = Mathf.FloorToInt(d);
            col.TakeDamage(Mathf.FloorToInt(d), c, b);
        }
    }
}
