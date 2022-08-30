using UnityEngine;

public class Controller2D : MonoBehaviour
{
    // Fileds
    protected virtual float Speed => 10f;
    
    protected virtual float MagnitudeVelocityX => 10f;
    protected virtual float MagnitudeVelocityY => Mathf.Infinity;

    protected virtual float DistanceCheckGround => .2f;
    protected virtual bool IsDebug => false;

    protected virtual float OffsetHeightRaycast => -.1f;
    
    protected bool IsGrounded;
    
    private Collider2D _collider2D;
    private Rigidbody2D _rigidbody2D;
    
    
    // Functions
    private void Awake()
    {
        if (!this.TryGetComponent(out _collider2D)) Error($"Not found: {nameof(Collider2D)}");
        if (!this.TryGetComponent(out _rigidbody2D)) Error($"Not found: {nameof(Rigidbody2D)}");
    }
    
    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        Vector2 positionMin = position + new Vector2(0, position.y - _collider2D.bounds.max.y + OffsetHeightRaycast);
        
        IsGrounded = Physics2D.Raycast(positionMin, Vector2.down, DistanceCheckGround);
        if (HasDebuging()) Debug.DrawRay(positionMin, Vector2.down * DistanceCheckGround, IsGrounded ? Color.green : Color.red);
        
        _rigidbody2D.velocity = new Vector2(
            Vector2.ClampMagnitude(_rigidbody2D.velocity, MagnitudeVelocityX).x,
            Vector2.ClampMagnitude(_rigidbody2D.velocity, MagnitudeVelocityY).y
        );
    }
    
    protected void Move(Vector2 direction)
        => _rigidbody2D.AddForce(direction * Speed);
    
    protected bool HasDebuging()
        => IsDebug && Application.isPlaying;
    
    private void Error(string message)
    {
        Debug.LogError($"[{name}]: Error: {message}");
        Destroy(gameObject);
    }
    
    private void OnDrawGizmos()
    {
        if(!HasDebuging()) return;

        Vector2 position = transform.position;
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(position, _rigidbody2D.velocity.normalized);
    }
}
