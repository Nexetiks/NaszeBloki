using Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PawnController : MonoBehaviour
{
    public LayerMask LeftLayer = default;
    public LayerMask RightLayer = default;
    public LayerMask LayerToAttack = default;
    public LayerMask PawnLayer = default;
    public GameManager.PlayerSideEnum side;
    private Transform objTransform;
    private float speed = 0;
    private bool isColliding = false;

    private Rigidbody rigidbody = null;

    public UnitsData UnitDataScriptableORGINAL = null;
    public UnitsData unitDataUSINGABLE = null;

    public IntChangeGameEvent unitDeathGoldChangeEvent;
    public IntChangeGameEvent unitDeathExpChangeEvent;

    public HealthBarController healthBarController;
    public Collider collider;


    public Animator animator;

    public bool isDead = false;

    public CoinSpawner coinSpawner;
    public Vector3 spawnPosition;

    public PawnKnockback KnockbackController;
    public float knockbackForce = 1.5f;
    public float knockbackDuration = 0.25f;

    void Start()
    {
        spawnPosition = transform.position;
        this.objTransform = gameObject.GetComponent(typeof(Transform)) as Transform;
        rigidbody = GetComponent<Rigidbody>();
        KnockbackController = GetComponent<PawnKnockback>();
        this.speed = objTransform.forward.z > 0 ? 2f : -2f;
    }

    private void OnEnable()
    {
        if (UnitDataScriptableORGINAL != null && unitDataUSINGABLE == null)
        {
            unitDataUSINGABLE = new UnitsData(UnitDataScriptableORGINAL);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (unitDataUSINGABLE == null && UnitDataScriptableORGINAL != null)
        {
            unitDataUSINGABLE = new UnitsData(UnitDataScriptableORGINAL);

            Transform[] _transofrms = GetComponentsInChildren<Transform>();

            if (side == GameManager.PlayerSideEnum.Left)
            {
                LayerToAttack = RightLayer;

                foreach (Transform _transform in _transofrms)
                {
                    _transform.gameObject.layer = 7;
                }
            }
            else
            {
                LayerToAttack = LeftLayer;

                foreach (Transform _transform in _transofrms)
                {
                    _transform.gameObject.layer = 8;
                }
            }

            healthBarController.Initialize(UnitDataScriptableORGINAL.MaxHP, UnitDataScriptableORGINAL.HP);
        }
    }

    private void OnDestroy()
    {
        ScriptableObject.Destroy(unitDataUSINGABLE);
    }

    void FixedUpdate()
    {
        if (isDead || animator == null)
        {
            return;
        }

        Vector3 fwd = objTransform.TransformDirection(Vector3.forward);

        RaycastHit hit;

        if (Physics.Raycast(objTransform.position, fwd, out hit, unitDataUSINGABLE.AttackRange, LayerToAttack))
        {
            animator.SetInteger("state", 0);
            this.isColliding = true;
            fight(gameObject, hit.transform.gameObject, unitDataUSINGABLE);
        }
        else if (Physics.Raycast(objTransform.position, fwd, out hit, 1))
        {
            this.isColliding = true;
            animator.SetInteger("state", 0);
        }
        else
        {
            animator.SetInteger("state", 1);
            this.isColliding = false;
        }

        if (this.isColliding == false)
        {
            objTransform.position = objTransform.position + Vector3.forward * Time.deltaTime * this.speed * unitDataUSINGABLE.Speed;
        }
    }

    private float timeOfNextAtack = 0;
    private HealthBarController baseHealthBar = null;
    private void fight(GameObject _attacker, GameObject _defender, UnitsData _unitData)
    {
        if (timeOfNextAtack > Time.time || _attacker.tag == _defender.tag)
        {
            return;
        }

        timeOfNextAtack = Time.time + 1 / _unitData.AttackSpeed;
        animator.SetInteger("state", 2);

        float _randomNumber = Random.Range(0, 100);
        float _demage = 0;

        if (unitDataUSINGABLE.PawnType == UnitsData.PawnTypeEnum.Ranged || (unitDataUSINGABLE.PawnType == UnitsData.PawnTypeEnum.Special && unitDataUSINGABLE.Fraction == GameManager.FractionEnum.Kler))
        {
            gameObject.GetComponent<TestThrowing>().ThrowTest(_attacker, _defender, unitDataUSINGABLE.Fraction, unitDataUSINGABLE.PawnType);
        }

        if (_randomNumber >= 0 && _randomNumber <= _unitData.CriticChange)
        {
            _demage = _unitData.AttackDemage * 2f;
        }
        else
        {
            _demage = _unitData.AttackDemage;
        }

        if (_defender.TryGetComponent(out BaseScript _base))
        {
            if (baseHealthBar == null)
            {
                baseHealthBar = _defender.GetComponentInChildren<HealthBarController>();
                baseHealthBar.slider.maxValue = _base.PlayerAttribute.BaseHP;
                baseHealthBar.slider.minValue = 0;
            }

            _base.PlayerAttribute.BaseHP -= _demage;

            if (_base.PlayerAttribute.BaseHP <= 0)
            {
                Debug.Log("koniec kurwa");
                //TODO EVENT HERE PLIS :D
                StartCoroutine(goToFinalStage());
            }

            baseHealthBar.slider.value = _base.PlayerAttribute.BaseHP;
            return;
        }

        var otherPawnController = _defender.GetComponent<PawnController>();
        if (otherPawnController == null || otherPawnController.isDead)
        {
            return;
        }
        UnitsData _defenderData = _defender.GetComponent<PawnController>().unitDataUSINGABLE;



        hitEnemy(_defender, _defenderData, _demage);
    }

    private void hitEnemy(GameObject _defenderGameObject, UnitsData _defenderUnitData, float _demage)
    {
        if (_defenderUnitData.Fraction == GameManager.FractionEnum.Sebix)
        {
            (GameObject.FindGameObjectsWithTag("AudioManager")[0].GetComponent(typeof(AudioController)) as AudioController).playAudio(AudioType.SebixPain);

        }
        else if (_defenderUnitData.Fraction == GameManager.FractionEnum.Moher)
        {
            (GameObject.FindGameObjectsWithTag("AudioManager")[0].GetComponent(typeof(AudioController)) as AudioController).playAudio(AudioType.MoherPain);

        }
        else if (_defenderUnitData.Fraction == GameManager.FractionEnum.Kler)
        {
            (GameObject.FindGameObjectsWithTag("AudioManager")[0].GetComponent(typeof(AudioController)) as AudioController).playAudio(AudioType.KlerPain);
        }

        int _rng = Random.RandomRange(0, 100);

        if (_rng >= 0 && _rng < unitDataUSINGABLE.CriticChange)
        {
            _demage = _demage * 2;
        }

        var otherPawnController = _defenderGameObject.GetComponent<PawnController>();
        otherPawnController.healthBarController.getHit(_demage);
        otherPawnController.KnockbackController.Knockback(knockbackForce, knockbackDuration, side == GameManager.PlayerSideEnum.Left);
        if (_defenderUnitData.getHit(_demage))
        {
            otherPawnController.SpawnCoins(spawnPosition);
            otherPawnController.Eject();
            var onDeathGoldChange = new IntValueChange()
            {
                Side = side,
                Value = (int)otherPawnController.unitDataUSINGABLE.GoldForKilling
            };
            unitDeathGoldChangeEvent.Raise(onDeathGoldChange);
            var onDeathExpChange = new IntValueChange()
            {
                Side = side,
                Value = (int)otherPawnController.unitDataUSINGABLE.ExpForKilling
            };
            unitDeathExpChangeEvent.Raise(onDeathExpChange);
            Destroy(_defenderGameObject, 5f);
        }
    }

    private void Eject()
    {
        isDead = true;
        KnockbackController.isDead = true;
        collider.enabled = false;
        rigidbody.useGravity = true;
        rigidbody.AddForce(Random.Range(-6, 6), Random.Range(2, 6), Random.Range(-6, 6), ForceMode.Impulse);
        rigidbody.AddTorque(Random.Range(-8, 8), Random.Range(-8, 8), Random.Range(-8, 8), ForceMode.Impulse);
        healthBarController.slider.gameObject.SetActive(false);
    }

    public void SpawnCoins(Vector3 target)
    {
        coinSpawner.coinTarget = target;

        if ((UnitDataScriptableORGINAL.GoldForKilling) > 500)
        {
            coinSpawner.Explode((int)(UnitDataScriptableORGINAL.GoldForKilling / 100f));
        }
        else if ((UnitDataScriptableORGINAL.GoldForKilling) > 1000)
        {
            coinSpawner.Explode((int)(UnitDataScriptableORGINAL.GoldForKilling / 200f));
        }
        else
        {
            coinSpawner.Explode((int)(UnitDataScriptableORGINAL.GoldForKilling / 20f));
        }
    }

    IEnumerator goToFinalStage()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("FinalScene");
    }
}