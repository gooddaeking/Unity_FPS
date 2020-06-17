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
    
    public GameObject player;
    public float moveSpeed = 10.0f;
    public float normalSpeed = 3.0f;
    int count = 0;
    bool isForward = true;

    /// 유용한 기능
    #region "Idle 상태에 필요한 변수들"
    float rotateSpeed = 90.0f;
    float enemyY;
    #endregion

    #region "Move 상태에 필요한 변수들"
    #endregion

    #region "Attack 상태에 필요한 변수들"
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
                Hit();
                break;
            case EnemyState.Die:
                Die();
                break;
        }
    }

    private void Idle()
    {
        Vector3 enemyRotate = new Vector3(0, enemyY, 0);
        transform.Translate(transform.forward * normalSpeed * Time.deltaTime);
        transform.eulerAngles = enemyRotate;
        if(count % 500 == 0)
        {
            if(isForward)
            {
                enemyY += rotateSpeed * Time.deltaTime;
            }
            else
            {
                enemyY -= rotateSpeed * Time.deltaTime;
            }
        }
        //1. 플레이어와 일정범위가 되면 이동상태로 변경 (탐지 범위)
        //- 플레이어 찾기 (GameObject.Find("Player"))
        //- 일정거리 20미터 (거리비교 : Distance, magnitude)
        //- 상태 변경 state = EnemyState.Move;
        //- 상태전환 출력
    }
    
    private void Move()
    {
        //1. 플레이어를 향해 이동 후 공격범위 안에 들어오면 공격상태로 변경
        //2. 플레이어를 추격하다 처음위치에서 일정범위 벗어나면 리턴상태로 변경
        //- 플레이어처럼 캐릭터컨트롤러를 이용하기
        //- 공격범위 지정
        //- 상태변경
        //- 상태전환 출력
    }

    private void Attack()
    {
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
    }

    //피격상태 (Any State)
    private void Hit()
    {
        //코루틴을 사용
        //1. 몬스터 체력이 1이상
        //2. 다시 상태를 이전상태로 변경
        //- 상태변경
        //- 상태전환 출력
    }

    //죽음상태 (Any State)
    private void Die()
    {
        //코루틴을 사용
        //1. 체력이 0이하
        //2. 몬스터오브젝트 삭제
        //- 상태변경
        //- 상태전환 출력
    }
}