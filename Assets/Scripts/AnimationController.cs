using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private TaskManager taskManager;
    private Coglings_Attack_AI coglingAttackAI;
    private Elf_Attack_AI sleemasiAttackAI;
    private Orc_Attack_AI graxxianAttackAI;
    
    private Animator animator;

    //private NavMeshAgent navMeshAgent;
    private ParticleSystem miningParticleSystem;
    private LineRenderer attackingLaserLeft;
    private LineRenderer attackingLaserRight;
    private ParticleSystem robotTurretFire;
    private ParticleSystem coglingTurretFire;
    //private ParticleSystem explosion;
    private ParticleSystem sleemasiTurretFire;
    private ParticleSystem graxxianArcherFire;

    private const string isAttacking = "IsAttacking";
    private const string isMining = "IsMining";
    private const string isWalking = "IsWalking";
    private const string isDefeated = "IsDefeated";

    private GameObject target;
    private Transform robotTurretShootBone;
    private Transform coglingTurretShootBone;
    private Transform coglingTurretRotateBone;
    private Transform sleemasiTurretShootBone;


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
        sleemasiAttackAI = GetComponent<Elf_Attack_AI>();
        graxxianAttackAI = GetComponent<Orc_Attack_AI>();

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
        else if (this.gameObject.tag == "EnemyBuilding" || this.gameObject.tag == "Enemy")
        {
            if (currName == Unit_Names.Cogling_Turret)
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
            else if (currName == Unit_Names.Sleemasi_Turret)
            {
                sleemasiTurretShootBone = transform.Find("Sleemasi_Turret/Sleemasi_Turret_Rock_Bone");

                sleemasiTurretFire = Instantiate(shootingEffectPrefab.GetComponent<ParticleSystem>(), sleemasiTurretShootBone);
            }
        }
        else if (this.gameObject.tag == "Graxian")
        {
            if (currName == Unit_Names.Graxxian_Ranged)
            {
                graxxianArcherFire = Instantiate(shootingEffectPrefab.GetComponent<ParticleSystem>(), this.transform);

                var pos = new Vector3(0, 2.02f, 2.34f);

                graxxianArcherFire.transform.localPosition = pos;
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
                graxxianArcherFire.Stop();
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
            case Unit_Names.Sleemasi_Turret:
                sleemasiTurretFire.Stop();
                StartCoroutine(stopSleemasiTurretAttack());
                //sleemasiTurretFire.Clear();

                //animator.Play("Sleemasi_Turret|Sleemasi_Turret_Spin_Idle");

                break;
            case Unit_Names.Graxxian_Turret:
                break;
        }
    }

    IEnumerator stopSleemasiTurretAttack()
    {
        while (sleemasiTurretFire.IsAlive())
        {
            yield return new WaitForEndOfFrame();
        }
        sleemasiTurretFire.Clear();

        if (!animator.enabled)
        {
            animator.enabled = true;
            //animator.StartPlayback();
            //animator.SetBool("IsFiring", false);
            animator.Play("Attacking", -1, 0f);
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
                Vector3 targ = graxxianAttackAI.target.transform.position - this.transform.position;
                targ.y = 0;
                this.transform.rotation = Quaternion.LookRotation(targ);

                graxxianArcherFire.transform.LookAt(graxxianAttackAI.target.transform);
                graxxianArcherFire.Play();

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
            case Unit_Names.Sleemasi_Turret:
                if (animator.enabled)
                {
                    //animator.SetBool("IsFiring", true);
                    //animator.StopPlayback();
                    animator.enabled = false;
                    sleemasiTurretShootBone.rotation = Quaternion.Euler(90, 0, 0);

                    sleemasiTurretShootBone.LookAt(sleemasiAttackAI.target.transform);
                    sleemasiTurretShootBone.Rotate(90, 0, 0, Space.Self);
                }
                else
                {
                    StartCoroutine(lookAtSmooth(0.5f));
                }

                if (!sleemasiTurretFire.isPlaying && !animator.enabled)
                {
                    sleemasiTurretFire.Play();
                }

                break;
            case Unit_Names.Graxxian_Turret:
                break;
        }
    }
    /*
    IEnumerator lookAtSmooth(float aTime)
    {
        Vector3 lookDirection = sleemasiAttackAI.target.transform.position - sleemasiTurretShootBone.position;
        //lookDirection.Normalize();
        //Quaternion startRot = Quaternion.Euler(new Vector3(sleemasiTurretShootBone.rotation.eulerAngles.x + 90, sleemasiTurretShootBone.rotation.eulerAngles.y, sleemasiTurretShootBone.rotation.eulerAngles.z));
        Quaternion startRot = sleemasiTurretShootBone.rotation;
        Quaternion endRot = Quaternion.LookRotation(new Vector3(lookDirection.x + 90, lookDirection.y, lookDirection.z));
        endRot = Quaternion.AngleAxis(90f, Vector3.);

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            sleemasiTurretShootBone.rotation = Quaternion.Slerp(startRot, endRot, t / 1);

            //sleemasiTurretShootBone.Rotate(90, 0, 0, Space.Self);

            yield return new WaitForEndOfFrame();
        }

        sleemasiTurretShootBone.rotation = endRot;
    }*/

    IEnumerator lookAtSmooth(float aTime)
    {
        Vector3 lookDirection = sleemasiAttackAI.target.transform.position - sleemasiTurretShootBone.position;
        Quaternion startRot = sleemasiTurretShootBone.rotation;
        Quaternion targetRot = Quaternion.LookRotation(lookDirection);
        Quaternion additionalRot = Quaternion.Euler(90, 0, 0); // 90-degree rotation around the X-axis
        Quaternion endRot = targetRot * additionalRot;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            sleemasiTurretShootBone.rotation = Quaternion.Slerp(startRot, endRot, t / 1);

            yield return new WaitForEndOfFrame();
        }

        sleemasiTurretShootBone.rotation = endRot;
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
