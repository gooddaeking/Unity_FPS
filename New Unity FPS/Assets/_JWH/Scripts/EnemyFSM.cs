using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditorInternal;
using UnityEngine;

//몬스터 유한 상태 머신
public class EnemyFSM : MonoBehaviour
{
    //몬스터 상태이넘문
    enum EnemyState
    {
        Idle, Move, Attack, Return, Hit, Die
    }

    EnemyState state;
    
    //public GameObject player;
    Transform player;               // 플레이어를 찾기위해
    CharacterController enemy;      // 몬스터 이동을 위해

    //애니메이션을 위해 
    Animator anim;

    int hp = 100;
    int att = 5;
    public float moveSpeed = 10.0f;
    public float normalSpeed = 3.0f;
    int count = 0;
    float attTime = 2f;
    float timer = 0f;
    bool isForward = true;

    /// 유용한 기능
    #region "Idle 상태에 필요한 변수들"
    Vector3 startPoint;             // 몬스터 시작위치
    float rotateSpeed = 90.0f;
    float enemyY;
    float findRange = 15f;          // 플레이어 탐색 범위
    float moveRange = 30f;          // 최대 이동 가능범위
    #endregion

    #region "Move 상태에 필요한 변수들"
    #endregion

    #region "Attack 상태에 필요한 변수들"
    float attackRange = 2f;
    #endregion

    #region "Return 상태에 필요한 변수들"
    #endregion

    #region "Hit 상태에 필요한 변수들"
    #endregion

    #region "Die 상태에 필요한 변수들"
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //몬스터 상태 초기화
        state = EnemyState.Idle;
        enemy = GetComponent<CharacterController>();
        startPoint = transform.position;
        player = GameObject.Find("Player").transform;
        //애니메이터 컴포넌트
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        count++;
        //상태에 따른 행동처리
        switch (state)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Hit:
                //Hit();
                break;
            case EnemyState.Die:
                //Die();
                break;
        }
    }

    private void Idle()
    {
        //Vector3 enemyRotate = new Vector3(0, enemyY, 0);
        //transform.Translate(this.transform.forward * normalSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, enemyY, 0);
        if(count % 500 == 0)
        {
            if (isForward) isForward = false;
            else isForward = true;
        }
        if (isForward)
        {
            enemyY -= rotateSpeed * Time.deltaTime;
            if (enemyY <= 0)
            {
                enemyY = 0;
                enemy.Move(transform.forward * normalSpeed * Time.deltaTime);
            }
        }
        else
        {
            enemyY += rotateSpeed * Time.deltaTime;
            if (enemyY >= 180)
            { 
                enemyY = 180;
                enemy.Move(transform.forward * normalSpeed * Time.deltaTime);
            }
        }

        //Vector3 dir = transform.position - player.position;
        //float distance = dir.magnitude;
        //if(distance < findRange)
        //if(dir.magnitude < findRange)

        if (Vector3.Distance(transform.position, player.position) < findRange)
        {
            state = EnemyState.Move;
            Debug.Log("플레이어를 발견! 쫓아가겠다!!");
            //애니메이션
            anim.SetTrigger("Move");
        }
        //1. 플레이어와 일정범위가 되면 이동상태로 변경 (탐지 범위)
        //- 플레이어 찾기 (GameObject.Find("Player"))
        //- 일정거리 20미터 (거리비교 : Distance, magnitude)
        //- 상태 변경 state = EnemyState.Move;
        //- 상태전환 출력
    }
    
    private void Move()
    {
        if (Vector3.Distance(transform.position, startPoint) > moveRange)
        {
            state = EnemyState.Return;
            Debug.Log("너무 멀리왔다, 돌아간다!!");
            anim.SetTrigger("Return");
        }
        else if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            Vector3 dir = player.position - transform.position;
            dir.Normalize();
            //transform.LookAt(player);
            //transform.forward = dir;
            //transform.forward = Vector3.Lerp(transform.forward);
            //최종적으로 자연스런 회전처리를 하려면 결국 쿼터니온을 사용해야한다.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
            enemy.SimpleMove(dir * moveSpeed);
            //중력문제를 해결하기 위해서 심플무브를 사용한다
            //심플무브는 최소환의 물리가 적용되어 중력문제를 해결할 수 있다.
            //단 내부적으로 시간처리를 하기 때문에 Time.deltaTime을 사용하지 않는다.
        }
        else
        {
            state = EnemyState.Attack;
            Debug.Log("플레이어 공격!!");
            anim.SetTrigger("Attack");
        }
        
        //1. 플레이어를 향해 이동 후 공격범위 안에 들어오면 공격상태로 변경
        //2. 플레이어를 추격하다 처음위치에서 일정범위 벗어나면 리턴상태로 변경
        //- 플레이어처럼 캐릭터컨트롤러를 이용하기
        //- 공격범위 지정
        //- 상태변경
        //- 상태전환 출력
    }

    private void Attack()
    {

        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            timer += Time.deltaTime;
            if (timer > attTime)
            {
                Debug.Log("죽어라!!");
                //플레이어의 필요한 스크립트 컴포넌트를 가져와서 처리한다
                //player.GetComponent<PlayerMove>().hitDamage(power);
                timer = 0f;
                anim.SetTrigger("Attack");
            }
        }
        else
        {
            state = EnemyState.Move;
            Debug.Log("플레이어를 쫓아가겠다!!");
            timer = 0f;
            anim.SetTrigger("Attack");
        }
        
            //1. 플레이어가 공격범위 안에 들어오면 일정한 시간간격으로 플레이어 공격
            //2. 플레이어가 공격범위를 벗어나면 이동상태로 변경
            //- 공격범위 지정
            //- 상태변경
            //- 상태전환 출력
        }

    private void Return()
    {
        //1. 몬스터가 플레이어를 추격하더라도 처음위치에서 일정범위를 벗어나면 복귀
        //- 처음위치에서 일정범위 지정
        //- 상태변경
        //- 상태전환 출력

        //시작위치까지 도달하지 않을때는 이동
        //도착하면 대기상태로 변경
        if(Vector3.Distance(transform.position, startPoint) > 0.1f)
        {
            Vector3 dir = (startPoint - transform.position).normalized;
            anim.SetTrigger("Move");
            enemy.SimpleMove(dir * moveSpeed);
        }
        else
        {
            //위치값을 초기값으로
            transform.position = startPoint;
            state = EnemyState.Idle;
            Debug.Log("돌아왔다. 대기한다!!");
            anim.SetTrigger("Idle");
        }
    }

    //플레이어 쪽에서 충돌감지를 할 수 있으니 이함수는 퍼블릭으로 만든다
    public void hitDamage(int value)
    {
        //예외처리
        //피격상태이거나, 죽은상태일때는 데미지 중첩을 주지 않는다
        if (state == EnemyState.Hit || state == EnemyState.Die) return;

        //체력깍기
        hp -= value;

        //몬스터의 체력이 1이상이면 피격상태
        if(hp > 0)
        {
            state = EnemyState.Hit;
            Debug.Log("으악!!");
            Debug.Log("HP : " + hp);
            anim.SetTrigger("Hit");
            Hit();
        }
        //0이하이면 죽음상태
        else
        {
            state = EnemyState.Die;
            Debug.Log("내.. 내가.. 죽다니..");
            anim.SetTrigger("Die");
            Die();
        }
    }

    //피격상태 (Any State)
    private void Hit()
    {
        //코루틴을 사용
        //1. 몬스터 체력이 1이상
        //2. 다시 상태를 이전상태로 변경
        //- 상태변경
        //- 상태전환 출력

        StartCoroutine(HitProc());
    }

    //피격상태 처리용 코루틴
    IEnumerator HitProc()
    {
        //피격모션 시간만큼 기다리기
        yield return new WaitForSeconds(1.0f);
        //현재 상태를 이동으로 변경
        anim.SetTrigger("Move");
        state = EnemyState.Move;
        
        Debug.Log("이.. 이동한다!");
    }

    //죽음상태 (Any State)
    private void Die()
    {
        //코루틴을 사용
        //1. 체력이 0이하
        //2. 몬스터오브젝트 삭제
        //- 상태변경
        //- 상태전환 출력

        //진행중인 모든 코루틴을 정지한다
        StopAllCoroutines();

        //죽음ㅅ항태를 처리하기 위한 코루틴 실행
        StartCoroutine(DieProc());
    }

    IEnumerator DieProc()
    {
        //캐릭터 컨트롤러 비활성화
        enemy.enabled = false;

        //2초후에 자기자신을 제거한다
        yield return new WaitForSeconds(2.0f);
        Debug.Log("사망");
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        //공격 가능범위
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        //플레이어를 찾을 수 있는 범위
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, findRange);
        //이동가능한 최대 범위
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(startPoint, moveRange);
    }
}