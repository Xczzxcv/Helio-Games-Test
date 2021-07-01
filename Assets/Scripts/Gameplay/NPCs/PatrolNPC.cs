using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolNPC : NPC
{
    [SerializeField] private NavMeshAgent _navAgent;

    // const params
    private Vector3 _anchorPoint;
    private float _radius;
    private float _waitTime;
    private Vector2 _groundSize;

    // flexible params
    private bool _isProcessing;

    // for debugging purposes
    void DrawCircle(Vector3 center, float radius, Color color, int num = 5, float time = 5)
	{
        float angleAdd = 360f / num;
        for (int i = 0; i < num; i++)
        {
            float angle = i * angleAdd * Mathf.Deg2Rad;
            Vector3 dirV = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            Debug.DrawLine(center, center + dirV * radius, color, time);
        }
    }

    public override void Init(Config config)
    {
        _luck = config.PatrolLuck;
        _radius = config.PatrolRadius;
        _waitTime = config.PatrolWaitTime;
        _groundSize = config.GroundSize;
        _navAgent.speed = config.PatrolSpeed;

        _rollDiceCollider.radius = config.RollDiceRange / 2;

        _anchorPoint = transform.position;
        ChooseNewTarget();

        DrawCircle(_anchorPoint, _radius, Color.red, 5);

        initialized = true;
    }

    protected void FixedUpdate()
    {
        if (!initialized) return;

        DrawCircle(transform.position, 0.3f, Color.green, 5, 0);
        DrawCircle(transform.position, _rollDiceCollider.radius, Color.green, 5, 0);
        if (_navAgent.remainingDistance == 0)
        {
            if (_isProcessing) return;
            else StartCoroutine(EndPointProcessing());
        }
    }

    private IEnumerator EndPointProcessing()
    {
        _isProcessing = true;

        if (Random.value < 0.5f)
        {
            _rigidbody.velocity = Vector3.zero;
            yield return new WaitForSeconds(_waitTime);
        }

        ChooseNewTarget();
        _isProcessing = false;
    }

    private void ChooseNewTarget()
    {
        float randAngleRad = Random.value * 360 * Mathf.Deg2Rad;
        var dirVector = new Vector3(
            Mathf.Cos(randAngleRad),
            0,
            Mathf.Sin(randAngleRad)
            );

        var nextTarget = _anchorPoint + dirVector * _radius * Random.value;

        // if '_nextTarget' will be out of real ground we return it to the real ground
        nextTarget.Set(
            Mathf.Min(Mathf.Max(-_groundSize.x / 2, nextTarget.x), _groundSize.x / 2),
            0,
            Mathf.Min(Mathf.Max(-_groundSize.y / 2, nextTarget.z), _groundSize.y / 2)
            );
        _navAgent.SetDestination(nextTarget);
        //_nextTargetDir = (_nextTarget - transform.position).normalized;
        //_rigidbody.velocity = _nextTargetDir * _speed;
    }
}
