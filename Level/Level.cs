using UnityEngine;

namespace VinhNguyen
{
    public class Level : MonoBehaviour
    {
        #region Fields

        private const int MIN_LEVEL = 1;
        private const int MAX_LEVEL = 99;

        [Tooltip("Level hiện tại")]
        [SerializeField]
        private int currentLevel;

        public delegate void LevelDelegateHandler(int newLevel);

        /// <summary>
        /// Được gọi mỗi khi level hiện tại có sự thay đổi
        /// </summary>
        public event LevelDelegateHandler OnLevelChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Đã đạt đẳng cấp cao nhất
        /// </summary>
        public bool IsMaxLevel
        {
            get { return currentLevel == MAX_LEVEL; }
        }
        
        /// <summary>
        /// Level hiện tại
        /// </summary>
        public int CurrentLevel
        {
            get { return currentLevel; }
            set
            {
                int newLevel = Mathf.Clamp(value, MIN_LEVEL, MAX_LEVEL);
                if (currentLevel != newLevel)
                {
                    currentLevel = newLevel;
                    OnLevelChanged?.Invoke(newLevel);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Nâng lên 1 cấp
        /// </summary>
        public void LevelUp()
        {
            if (IsMaxLevel)
                return;

            CurrentLevel++;
        }

        #endregion
    }
}