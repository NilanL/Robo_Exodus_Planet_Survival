using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private TaskManager taskManager;
    private Coglings_Attack_AI coglingAttackAI;
    private Animator animator;
    //private NavMeshAgent navMeshAgent;
    private ParticleSystem miningParticleSystem;
    private LineRenderer attackingLaserLeft;
    private LineRenderer attackingLaserRight;
    private ParticleSystem robotTurretFire;
    private ParticleSystem coglingTurretFire;
    private ParticleSystem explosion;

    private const string isAttacking = "IsAttacking";
    private const string isMining = "IsMining";
    private const string isWalking = "IsWalking";
    private const string isDefeated = "IsDefeated";

    private GameObject target;
    private Transform robotTurretShootBone;
    private Transform coglingTurretShootBone;
    private Transform coglingTurretRotateBone;


    [SerializeField]
    public GameObject shootingEffectPrefab;

    [SerializeField]
    public GameObject attackingEffectPrefab;

    [SerializeField]
    public GameObject miningEffectPrefab;

    public AudioSource attackSound;
    public AudioSource mineSound;

    private Unit_Names currName;

    // Start is called before the first frame update
    void Start()
    {
        currName = GetComponent<Unit_Name>().unit_Name;
        target = null;
        taskManager = GetComponent<TaskManager>();
        coglingAttackAI = GetComponent<Coglings_Attack_AI>();

        // Get controller and animator
        animator = this.gameObject.GetComponent<Animator>();
        //navMeshAgent = GetComponent<NavMeshAgent>();

        if (this.gameObject.tag == "Selectable")
        {
            if (currName == Unit_Names.Miner || currName == Unit_Names.Robot_Melee || currName == Unit_Names.Robot_Ranged)
            {
                // Find attachment location for laser effects
                Transform centralBone;
                if (currName == Unit_Names.Robot_Melee)
                    centralBone = transform.Find("Robot_Melee_Armature/Central_Bone");
                else if (currName == Unit_Names.Robot_Ranged)
                    centralBone = transform.Find("Robot_Ranged_Armature/Central_Bone");
                else
                    centralBone = transform.Find("Robot_Miner_Armature/Central_Bone");

                var leftLaser = centralBone.Find("Left_Arm_Bone1/Left_Arm_Bone2/Left_Arm_Bone3");
                var rightLaser = centralBone.Find("Right_Arm_Bone1/Right_Arm_Bone2/Right_Arm_Bone3");

                // Get laser effect
                var tempLaser = attackingEffectPrefab.GetComponent<LineRenderer>();
                tempLaser.enabled = false;

                // Set left arm laser effect/position
                if (leftLaser != null)
                {
                    var rotL = transform.rotation.eulerAngles;
                    rotL.x += 12;
                    rotL.y += 25;

                    attackingLaserLeft = Instantiate(tempLaser, leftLaser.position, Quaternion.Euler(rotL), leftLaser);
                }

                // Set right arm laser effect/position
                if (rightLaser != null)
                {
                    var rotR = transform.rotation.eulerAngles;
                    rotR.x -= 12;
                    rotR.y -= 25;

                    attackingLaserRight = Instantiate(tempLaser, rightLaser.position, Quaternion.Euler(rotR), rightLaser);
                }

                if (currName == Unit_Names.Miner)
                {
                    // Set mining sparks effects
                    miningParticleSystem = Instantiate(miningEffectPrefab.GetComponent<ParticleSystem>(), transform.position, transform.rotation, transform);
                }
            }
        }
        else if (this.gameObject.tag == "Building")
        {
            if (this.gameObject.GetComponent<Unit_Name>().unit_Name == Unit_Names.Robot_Turret)
            {
                robotTurretShootBone = transform.Find("Turret_Building_Model/robot_turret/Robot_Turret_Armature/Robot_Turret_Base_Bone/Robot_Turret_Gun_Bone");
                var pos = new Vector3(0f, 0.0525f, 0.0011f);
                var rot = new Vector3(-90f, 0, 0);
                robotTurretFire = Instantiate(shootingEffectPrefab.GetComponent<ParticleSystem>(), robotTurretShootBone);
                robotTurretFire.transform.localPosition = pos;
                robotTurretFire.transform.localRotation = Quaternion.Euler(rot);
                //robotTurretFire.transform.localScale = new Vector3(robotTurretFire.transform.localScale.x * 2, robotTurretFire.transform.localScale.y * 2, robotTurretFire.transform.localScale.z * 2);
            }
        }
        else if (this.gameObject.tag == "EnemyBuilding")
        {
            if (this.gameObject.GetComponent<Unit_Name>().unit_Name == Unit_Names.Cogling_Turret)
            {
                coglingTurretRotateBone = transform.Find("cogling_turret/Cogling_Turret_Armature/Base_Bone/Rotate_Bone");
                coglingTurretShootBone = transform.Find("cogling_turret/Cogling_Turret_Armature/Base_Bone/Rotate_Bone/Tilt_Bone");

                var pos = new Vector3(1.765081e-09f, 0.01912951f, 6.053597e-07f);
                var rot = new Vector3(-90f, 0, 0);
                var scale = new Vector3(0.01313561f, 0.01313561f, 0.01313561f);

                coglingTurretFire = Instantiate(attackingEffectPrefab.GetComponent<ParticleSystem>(), coglingTurretShootBone);
                coglingTurretFire.transform.localPosition = pos;
                coglingTurretFire.transform.localRotation = Quaternion.Euler(rot);
                coglingTurretFire.transform.localScale = scale;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Initialize()
    {

    }

    IEnumerator ShootLaser(Transform _transform)
    {
        GameObject laser = Instantiate(shootingEffectPrefab, _transform.position, transform.rotation);

        Vector3 startingPos = _transform.position;
        Vector3 finalPos = taskManager.target.transform.position;
        float elapsedTime = 0;

        while (elapsedTime < 0.2f)
        {
            laser.transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / 0.2f));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Destroy(laser);
    }

    public void IsNotMoving()
    {
        var unit = this.gameObject.GetComponent<Unit_Name>().unit_Name;
        switch (unit)
        {
            case Unit_Names.Miner:
                break;
            case Unit_Names.Wolf:
                break;
            case Unit_Names.Robot_Melee:
                animator.SetBool(isWalking, false);
                break;
            case Unit_Names.Robot_Ranged:
                animator.SetBool(isWalking, false);
                break;
            case Unit_Names.Cogling_Melee:
                animator.SetBool(isWalking, false);
                break;
            case Unit_Names.Cogling_Range:
                animator.SetBool(isWalking, false);
                break;
            case Unit_Names.Sleemasi_Melee:
                animator.SetBool(isWalking, false);
                break;
            case Unit_Names.Sleemasi_Ranged:
                animator.SetBool(isWalking, false);
                break;
            case Unit_Names.Sleemasi_Miner:
                animator.SetBool(isWalking, false);
                break;
            case Unit_Names.Graxxian_Melee:
                animator.SetBool(isWalking, false);
                break;
            case Unit_Names.Graxxian_Ranged:
                animator.SetBool(isWalking, false);
                break;
            case Unit_Names.Graxxian_Miner:
                animator.SetBool(isWalking, false);
                break;
            default:
                break;
        }
    }


    public void IsMoving()
    {
        var unit = this.gameObject.GetComponent<Unit_Name>().unit_Name;
        switch (unit)
        {
            case Unit_Names.Miner:
                break;
            case Unit_Names.Wolf:
                break;
            case Unit_Names.Robot_Melee:
                animator.SetBool(isWalking, true);
                break;
            case Unit_Names.Robot_Ranged:
                animator.SetBool(isWalking, true);
                break;
            case Unit_Names.Cogling_Melee:
                animator.SetBool(isWalking, true);
                break;
            case Unit_Names.Cogling_Range:
                animator.SetBool(isWalking, true);
                break;
            case Unit_Names.Sleemasi_Melee:
                animator.SetBool(isWalking, true);
                break;
            case Unit_Names.Sleemasi_Ranged:
                animator.SetBool(isWalking, true);
                break;
            case Unit_Names.Sleemasi_Miner:
                animator.SetBool(isWalking, true);
                break;
            case Unit_Names.Graxxian_Melee:
                animator.SetBool(isWalking, true);
                break;
            case Unit_Names.Graxxian_Ranged:
                animator.SetBool(isWalking, true);
                break;
            case Unit_Names.Graxxian_Miner:
                animator.SetBool(isWalking, true);
                break;
            default:
                break;
        }
    }

    public void IsNotMining()
    {
        var unit = this.gameObject.GetComponent<Unit_Name>().unit_Name;
        switch (unit)
        {
            case Unit_Names.Miner:
                var movement1 = this.gameObject.GetComponent<Robot_Miner_Controller_Mouse>();
                movement1.IsNotMining();
                break;
            case Unit_Names.Wolf:
                break;
            case Unit_Names.Cogling_Miner:
                animator.SetBool(isMining, false);
                break;
            case Unit_Names.Sleemasi_Miner:
                animator.SetBool(isMining, false);
                break;
            case Unit_Names.Graxxian_Miner:
                animator.SetBool(isMining, false);
                break;
            default:
                break;
        }
    }

    public void IsNotAttacking()
    {
        target = null;
        var unit = this.gameObject.GetComponent<Unit_Name>().unit_Name;
        switch (unit)
        {
            case Unit_Names.Miner:
                var movement = this.gameObject.GetComponent<Robot_Miner_Controller_Mouse>();
                movement.IsNotAttacking();
                break;
            case Unit_Names.Robot_Melee:
                animator.SetBool(isAttacking, false);

                attackingLaserLeft.enabled = false;
                attackingLaserRight.enabled = false;

                attackSound.Stop();
                break;
            case Unit_Names.Robot_Ranged:
                animator.SetBool(isAttacking, false);
                break;
            case Unit_Names.Cogling_Melee:
                animator.SetBool(isAttacking, false);
                break;
            case Unit_Names.Cogling_Range:
                animator.SetBool(isAttacking, false);
                break;
            case Unit_Names.Sleemasi_Melee:
                animator.SetBool(isAttacking, false);
                break;
            case Unit_Names.Sleemasi_Ranged:
                animator.SetBool(isAttacking, false);
                break;
            case Unit_Names.Sleemasi_Miner:
                animator.SetBool(isAttacking, false);
                break;
            case Unit_Names.Graxxian_Melee:
                animator.SetBool(isAttacking, false);
                break;
            case Unit_Names.Graxxian_Ranged:
                animator.SetBool(isAttacking, false);
                break;
            case Unit_Names.Graxxian_Miner:
                animator.SetBool(isAttacking, true);
                break;
            case Unit_Names.Wolf:
                break;
            case Unit_Names.Robot_Turret:
                robotTurretFire.Stop();
                break;
            case Unit_Names.Cogling_Turret:
                coglingTurretFire.Stop();
                break;
        }
    }

    public void IsAttacking()
    {
        var unit = this.gameObject.GetComponent<Unit_Name>().unit_Name;
        switch (unit)
        {
            case Unit_Names.Miner:
                var movement = this.gameObject.GetComponent<Robot_Miner_Controller_Mouse>();
                movement.IsAttacking();
                break;
            case Unit_Names.Robot_Melee:
                animator.SetBool(isAttacking, true);

                attackingLaserLeft.enabled = true;
                attackingLaserRight.enabled = true;

                if (attackSound.isPlaying != true)
                {
                    attackSound.Play();
                }
                break;
            case Unit_Names.Robot_Ranged:
                animator.SetBool(isAttacking, true);
                StartCoroutine(ShootLaser(transform));
                break;
            case Unit_Names.Cogling_Melee:
                animator.SetBool(isAttacking, true);
                break;
            case Unit_Names.Cogling_Range:
                animator.SetBool(isAttacking, true);
                break;
            case Unit_Names.Sleemasi_Melee:
                animator.SetBool(isAttacking, true);
                break;
            case Unit_Names.Sleemasi_Ranged:
                animator.SetBool(isAttacking, true);
                break;
            case Unit_Names.Sleemasi_Miner:
                animator.SetBool(isAttacking, true);
                break;
            case Unit_Names.Graxxian_Melee:
                animator.SetBool(isAttacking, true);
                break;
            case Unit_Names.Graxxian_Ranged:
                animator.SetBool(isAttacking, true);
                break;
            case Unit_Names.Graxxian_Miner:
                animator.SetBool(isAttacking, true);
                break;
            case Unit_Names.Wolf:
                break;
            case Unit_Names.Robot_Turret:
                robotTurretShootBone.LookAt(taskManager.target.transform);
                robotTurretShootBone.Rotate(90, 0, 0, Space.Self);

                if (!robotTurretFire.isPlaying)
                {
                    robotTurretFire.Play();
                }
                break;
            case Unit_Names.Cogling_Turret:
                Vector3 targetRot = coglingAttackAI.target.transform.position - coglingTurretRotateBone.position;
                targetRot.y = 0;
                coglingTurretRotateBone.rotation = Quaternion.LookRotation(targetRot);

                Vector3 targetTilt = coglingAttackAI.target.transform.position - coglingTurretShootBone.position;
                coglingTurretShootBone.rotation = Quaternion.LookRotation(targetTilt);
                coglingTurretShootBone.Rotate(90, 0, 0, Space.Self);

                if (!coglingTurretFire.isPlaying)
                {
                    coglingTurretFire.Play();
                }
                break;
        }
    }

    public void IsMining()
    {
        var unit = this.gameObject.GetComponent<Unit_Name>().unit_Name;
        switch (unit)
        {
            case Unit_Names.Miner:
                var movement = this.gameObject.GetComponent<Robot_Miner_Controller_Mouse>();
                movement.IsMining();
                break;
            case Unit_Names.Wolf:
                break;
            case Unit_Names.Cogling_Miner:
                animator.SetBool(isMining, false);
                break;
            case Unit_Names.Sleemasi_Miner:
                animator.SetBool(isMining, false);
                break;
            case Unit_Names.Graxxian_Miner:
                animator.SetBool(isMining, false);
                break;
            default:
                break;
        }
    }

    public void InRange()
    {
        var unit = this.gameObject.GetComponent<Unit_Name>().unit_Name;
        switch (unit)
        {
            case Unit_Names.Miner:
                break;
            case Unit_Names.Wolf:
                break;
            case Unit_Names.Robot_Ranged:
                var movement = this.gameObject.GetComponent<MovementScript>();
                movement.InRange();
                break;
        }
    }
}
