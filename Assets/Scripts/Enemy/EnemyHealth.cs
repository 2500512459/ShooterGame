using UnityEngine;

// 敌人生命值管理系统
public class EnemyHealth : MonoBehaviour
{
    // 公有配置参数
    public int startingHealth = 100;   // 初始生命值
    public int currentHealth;          // 实时更新的当前生命值
    public float sinkSpeed = 2.5f;     // 死亡后下沉速度
    public int scoreValue = 10;        // 击杀获得的分数
    public AudioClip deathClip;        // 死亡音效文件

    // 组件引用
    private Animator anim;             // 动画控制器
    private AudioSource enemyAudio;    // 音频源组件
    private ParticleSystem hitParticles; // 受击粒子效果
    private CapsuleCollider capsuleCollider; // 碰撞体组件

    // 状态标识
    private bool isDead;               // 是否已死亡
    private bool isSinking;            // 是否正在下沉

    // 初始化组件引用
    void Awake()
    {
        // 获取组件引用
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        // 初始化当前生命值
        currentHealth = startingHealth;
    }

    // 每帧更新（主要用于处理下沉）
    void Update()
    {
        if (isSinking)
        {
            // 持续向下移动（负Y轴方向）
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    // 受到伤害的处理方法
    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
            return; // 已死亡不再处理伤害

        enemyAudio.Play(); // 播放受伤音效

        currentHealth -= amount; // 扣除生命值

        // 在受击点播放粒子效果
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        // 检查是否死亡
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    // 死亡处理
    void Death()
    {
        isDead = true;

        // 将碰撞体设为Trigger，避免阻挡其他物体
        capsuleCollider.isTrigger = true;

        // 触发死亡动画
        anim.SetTrigger("Dead");

        // 切换并播放死亡音效
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
    }

    // 开始下沉（由动画事件调用）
    public void StartSinking()
    {
        // 禁用导航组件
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        // 设置刚体为运动学，避免物理影响
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true; // 激活下沉状态

        // 分数系统（示例代码，需根据实际系统实现）
        //ScoreManager.score += scoreValue;

        // 2秒后销毁对象
        Destroy(gameObject, 2f);
    }
}