using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour {
    // staticメンバにインスタンスを代入して各所からアクセスできるようにする
    public static CameraManager Instance { get; private set; }

    /// <summary>注視対象</summary>
    private Transform target;
    
    /// <summary>カメラと注視対象の相対的な座標</summary>
    private Vector3 offsetPosition;

    /// <summary>注視対象を追いかける最大速度(m/s)</summary>
    [SerializeField] private float maxSpeed = 10f;

    /// <summary>注視対象を追いかける速度が最大になる距離</summary>
    [SerializeField] private float maxSpeedDistance = 2f;

    private Vector3 shakeOffset;

    private Sequence shakeSeq;

    /// <summary>起動時の処理</summary>
    private void Awake() {
        Instance = this;

        if (target == null) {
            return;
        }
    }

    /// <summary>更新処理</summary>
    private void Update() {
        if (target == null) {
            return;
        }

        var currentPos = transform.position;
        // 移動目標座標
        var targetPos = target.position + offsetPosition;
        // カメラ座標と目標座標の距離
        var distance = Vector3.Distance(currentPos, targetPos);

        // 今回のフレームでのカメラの移動量
        // distanceが0と_maxSpeedDistance(2)の中のどこかを 0.0～1.0 の値をリターンしてくる
        // この場合 distance が2以上であればフルスピード（1.0）でカメラが移動し、目標に近づくとスピードが徐々に遅くなる
        var maxDistanceDelta = maxSpeed * Mathf.InverseLerp(0f, maxSpeedDistance, distance) * Time.deltaTime;

        // カメラを目標座標に向かって、計算した移動量分だけ移動させる
        transform.position = Vector3.MoveTowards(currentPos, targetPos, maxDistanceDelta);

        // 振動によるオフセットを反映
        transform.position += shakeOffset;
    }

    /// <summary>画面の振動演出</summary>
    public void Shake(float duration = 0.3f, float strength = 0.02f, int vibrato = 30) {
        // 前回の_shakeSeqがまだ再生中だった場合を考慮して、演出の強制終了メソッドを呼び出し
        shakeSeq?.Kill();
        
        // 0.3秒間、ランダムな方向に0.02mの幅で30回振動する演出を作成・再生
        shakeSeq = DOTween.Sequence()
            .SetLink(gameObject)
            .Append(DOTween.Shake(() => Vector3.zero, offset => shakeOffset = offset, duration, strength, vibrato));
    }

    /// <summary>カメラの注視対象をセットする</summary>
    public void SetCameraTarget(Transform cameraTarget)
    {
        target = cameraTarget;

        // ゲーム開始時のカメラと注視対象の相対的な座標を計算
        offsetPosition = transform.position - target.position;
    }
}
