using UnityEngine;

namespace VinhNguyen
{
    [RequireComponent(typeof(Level))]
    public class Experience : MonoBehaviour
    {
        #region Fields
        private const int MIN_EXP = 0;
        private const int MAX_EXP = 999999999;

        /// <summary>
        /// 3 hằng số cho phương trình bậc 2 tính exp yêu cầu
        /// </summary>
        private const int A_CONSTANT = 10;
        private const int B_CONSTANT = 50;
        private const int C_CONSTANT = 40;
        
        [Tooltip("Kinh nghiệm level hiện tại tích lũy")]
        [SerializeField]
        private int currentExp;

        public delegate void ExperienceEventHandler(int currentExp, int requiredExp);

        /// <summary>
        /// Được gọi mỗi khi exp hiện tại có sự thay đổi
        /// </summary>
        public event ExperienceEventHandler OnCurrentExpChanged;

        [SerializeField, ReadOnly]
        private int requiredExp;

        [System.NonSerialized]
        private Level levelComponent;
        #endregion

        #region Properties

        /// <summary>
        /// Exp tích lũy hiện tại
        /// </summary>
        public int ExpCurrent
        {
            get { return currentExp; }
            set
            {
                int newExp = Mathf.Clamp(value, MIN_EXP, MAX_EXP);
                if (currentExp != newExp)
                {
                    currentExp = newExp;
                    OnCurrentExpChanged?.Invoke(currentExp, requiredExp);

                    if (!levelComponent.IsMaxLevel)
                    {
                        CheckCurrentExp();
                    }
                }
            }
        }
        
        #endregion

        private void Awake()
        {
            levelComponent = GetComponent<Level>();

            levelComponent.OnLevelChanged += LevelComponent_OnCurrentLevelChanged;

            int currentLevel = levelComponent.CurrentLevel;
            requiredExp = currentLevel * (currentLevel * A_CONSTANT + B_CONSTANT) + C_CONSTANT;
        }
        
        private void OnDestroy()
        {
            if (levelComponent != null)
                levelComponent.OnLevelChanged -= LevelComponent_OnCurrentLevelChanged;
        }
        
        /// <summary>
        /// Kiểm tra Exp hiện tại để tăng level
        /// </summary>
        private void CheckCurrentExp()
        {
            if (ExpCurrent >= requiredExp)
            {
                ExpCurrent -= requiredExp;
                levelComponent.LevelUp();
            }
        }
        
        private void LevelComponent_OnCurrentLevelChanged(int newValue)
        {
            requiredExp = newValue * (newValue * A_CONSTANT + B_CONSTANT) + C_CONSTANT;
        }
    }
}