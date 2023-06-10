using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public GameObject owner;

    [SerializeField]
    private string animationName;

    [SerializeField]
    private bool magicAttack;

    [SerializeField]
    private float magicCost;

    [SerializeField]
    private float minAttackMultiplier;

    [SerializeField]
    private float maxAttackMultiplier;

    [SerializeField]
    private float minDefenseMultiplier;

    [SerializeField]
    private float maxDefenseMultiplier;

    private FighterStats attackerStats;
    private FighterStats targetStats;
    private float damage = 0.0f;
    public GameObject snowBall;
    public GameObject superAtack;
    public float duration = 0.5f;
    
    public void Attack(GameObject victim)
    {
        attackerStats = owner.GetComponent<FighterStats>();
        targetStats = victim.GetComponent<FighterStats>();
        if (attackerStats.magic >= magicCost && targetStats.health>0)
        {
            float multiplier = Random.Range(minAttackMultiplier, maxAttackMultiplier);

            damage = multiplier * attackerStats.melee;
            if (magicAttack)
            {
                damage = multiplier * attackerStats.magicRange;
            }

            float defenseMultiplier = Random.Range(minDefenseMultiplier, maxDefenseMultiplier);
            damage = Mathf.Max(0, damage - (defenseMultiplier * targetStats.defense));
            owner.GetComponent<Animator>().Play(animationName);
            if(animationName.Equals("range")&&owner.name=="Hero"){
                
                Vector3 defaultPosition = snowBall.transform.position;
                Vector3 newPosition = defaultPosition;
                newPosition.x = 3.8f;
                StartCoroutine(MoveObject(defaultPosition,newPosition));
                snowBall.transform.position = defaultPosition;
                
            }
            if(animationName.Equals("super")&&owner.name=="Hero"){
                StartCoroutine(SuperAcitvation());
            }
            targetStats.ReceiveDamage(Mathf.CeilToInt(damage));
            attackerStats.updateMagicFill(magicCost);
        } else
        {
            Invoke("SkipTurnContinueGame", 1);
        }
    }

    private IEnumerator SuperAcitvation()
    {
        superAtack.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        superAtack.SetActive(false);
    }

    void SkipTurnContinueGame()
    {
        GameObject.Find("GameControllerObject").GetComponent<GameController>().NextTurn();
    }

     private IEnumerator MoveObject(Vector3 startPosition, Vector3 targetPosition)
    {
        snowBall.SetActive(true);

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            // Asegurarse de que el objeto esté en la posición exacta al finalizar la interpolación
            snowBall.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;

        }

            snowBall.transform.position = startPosition;
            snowBall.SetActive(false);
    }
}

