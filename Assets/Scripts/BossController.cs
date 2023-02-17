using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [field: SerializeField] public int Health { get; set; }
    public static BossController Instance { get; set; }
    [field: SerializeField] public List<BossAction> Actions;
    [field: SerializeField] public Rigidbody2D BossBody { get; set; }
    [field: SerializeField] public GameObject DeathEffect { get; set; }
    [field: SerializeField] public GameObject HitEffect { get; set; }
    [field: SerializeField] public GameObject LevelExit { get; set; }
    [field: SerializeField] public Transform EnemySpawns { get; set; }
    [field: SerializeField] public List<BossSequence> Sequences { get; set; }
    
    private int currentSequence;
    private Vector3 moveDirection;

    private int currentAction;

    private float actionCounter;
    private float shotCounter;
    private float spawnCounter;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Sequences[currentSequence].TimeBetweenSpawns = 20f;
        Actions = Sequences[currentSequence].Actions;

        actionCounter = Actions[currentAction].ActionLenght;

        UIController.Instance.BossHealthBar.maxValue = Health;
        UIController.Instance.BossHealthBar.value = Health;
    }

    // Update is called once per frame
    void Update()
    {
        if (actionCounter > 0)
        {
            actionCounter -= Time.deltaTime;


            //handle movement
            moveDirection = Vector3.zero;

            if (Actions[currentAction].ShouldMove)
            {
                if (Actions[currentAction].ShouldChasePlayer)
                {
                    moveDirection = PlayerController.Instance.transform.position - transform.position;
                }
                else if (Actions[currentAction].MoveToPoint && Vector3.Distance(transform.position, Actions[currentAction].PointToMove.position) > .5f)
                {
                    moveDirection = Actions[currentAction].PointToMove.position - transform.position;
                }
            }

            moveDirection.Normalize();
            BossBody.velocity = moveDirection * Actions[currentAction].Speed;

            if (Sequences[currentSequence].ShouldSpawnMinions)
            {
                spawnCounter -= Time.deltaTime;
                if (spawnCounter <= 0)
                {
                    spawnCounter = Sequences[currentSequence].TimeBetweenSpawns;
                    for (int i = 0; i < Sequences[currentSequence].StartNumberOfSpawns; i++)
                    {
                        var position = gameObject.transform.position;
                        position.x += Random.Range(-1, 2) * 2;
                        position.y += Random.Range(-1, 2) * 2;

                        var enemy = Instantiate(Sequences[currentSequence].Enemy, position, transform.rotation);
                        enemy.transform.parent = EnemySpawns;
                    }
                    Sequences[currentSequence].StartNumberOfSpawns++;
                }
            }

            if (Actions[currentAction].ShouldShoot)
            {
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    shotCounter = Actions[currentAction].TimeBetweenShots;

                    Actions[currentAction].ShotPoints.ForEach(x =>
                    {
                        Instantiate(Actions[currentAction].ItemToShoot, x.position, x.rotation);

                    });
                    Actions[currentAction].ShotPointsCenter.gameObject.transform.rotation *= Quaternion.Euler(0f, 0f, 5f);
                }
            }
        }
        //handle shooting
        else
        {
            currentAction = (currentAction + 1) % Actions.Count;

            actionCounter = Actions[currentAction].ActionLenght;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        Health -= damageAmount;

        if (Health <= 0)
        {
            gameObject.SetActive(false);

            Instantiate(DeathEffect, transform.position, transform.rotation);

            if (Vector3.Distance(PlayerController.Instance.transform.position, LevelExit.transform.position) < 2f)
                LevelExit.transform.position += new Vector3(4f, 0f, 0f);

            LevelExit.SetActive(true);

            UIController.Instance.BossHealthBar.gameObject.SetActive(false);

        }
        else if (Health <= Sequences[currentSequence].EndSequenceHealth && currentSequence < Sequences.Count - 1)
        {
            currentSequence++;
            Actions = Sequences[currentSequence].Actions;
            currentAction = 0;
            actionCounter = Actions[currentAction].ActionLenght;
        }


        UIController.Instance.BossHealthBar.value = Health;
    }
}

[System.Serializable]
public class BossAction
{
    [field: SerializeField, Header("Action")] public float ActionLenght { get; set; }
    [field: SerializeField] public bool ShouldMove { get; set; }
    [field: SerializeField] public bool ShouldChasePlayer { get; set; }
    [field: SerializeField] public float Speed { get; set; }
    [field: SerializeField] public bool MoveToPoint { get; set; }
    [field: SerializeField] public Transform PointToMove { get; set; }
    [field: SerializeField] public bool ShouldShoot { get; set; }
    [field: SerializeField] public GameObject ItemToShoot { get; set; }
    [field: SerializeField] public float TimeBetweenShots { get; set; }
    [field: SerializeField] public List<Transform> ShotPoints { get; set; }
    [field: SerializeField] public GameObject ShotPointsCenter { get; set; }
    
}

[System.Serializable]
public class BossSequence
{
    [field: SerializeField, Header("Sequence")] public List<BossAction> Actions { get; set; }
    [field: SerializeField] public int EndSequenceHealth { get; set; }
    [field: SerializeField, Header("Minions")] public bool ShouldSpawnMinions { get; set; }
    [field: SerializeField] public float TimeBetweenSpawns { get; set; }
    [field: SerializeField] public GameObject Enemy { get; set; }
    [field: SerializeField] public int StartNumberOfSpawns { get; set; }
}
