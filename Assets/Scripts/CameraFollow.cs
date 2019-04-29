using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _maxDistance;
    private float _distance;
    private float _leftBound;
    private float _rightBound;
    private float _topBound;
    private float _bottomBound;

    private void Start() {
        float vertExtent = Camera.main.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;
        SpriteRenderer mapBounds = GameObject.FindWithTag("Map").GetComponent<SpriteRenderer>();
        _leftBound = horzExtent - mapBounds.sprite.bounds.size.x / 2.0f;
        _rightBound = mapBounds.sprite.bounds.size.x / 2.0f - horzExtent;
        _bottomBound = mapBounds.sprite.bounds.size.y / 2.0f - vertExtent;
        _topBound = vertExtent - mapBounds.sprite.bounds.size.y / 2.0f;
    }

    // Update is called once per frame
    void Update () {
        _distance = Vector3.Distance(transform.position, _target.position + Vector3.back);

        Vector3 pos = Vector3.MoveTowards(transform.position, _target.position + Vector3.back, _speed * (_distance / _maxDistance) * Time.deltaTime);
        if (pos.x < _leftBound)
            pos.x = _leftBound;

        if (pos.x > _rightBound)
            pos.x = _rightBound;

        if (pos.y < _topBound)
            pos.y = _topBound;

        if (pos.y > _bottomBound)
            pos.y = _bottomBound;

        transform.position = pos;
    }
}
