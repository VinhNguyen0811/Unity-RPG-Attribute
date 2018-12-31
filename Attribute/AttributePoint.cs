using UnityEngine;

namespace VinhNguyen
{
    /// <summary>
    /// Thêm tiền thưởng cho thuộc tính dựa trên tăng điểm thuộc tính
    /// </summary>
    [RequireComponent(typeof(Level))]
    [RequireComponent(typeof(Attribute))]
    public class AttributePoint : MonoBehaviour
    {
        [Tooltip("Các thuộc tính nhận tiền thưởng")]
        [SerializeField]
        private AttributePointData data;

        [Tooltip("Điểm thuộc tính được thưởng cho mỗi level")]
        [SerializeField]
        private int pointBonus;

        /// <summary>
        /// Các modifier tương ứng với mỗi thuộc tính
        /// </summary>
        [System.NonSerialized]
        private Modifier[] modifiers;

        /// <summary>
        /// Số điểm đã tăng (không thể hoàn lại) tương ứng với mỗi thuộc tính
        /// </summary>
        [System.NonSerialized]
        private int[] applyPoints;

        /// <summary>
        /// Số điểm đã tăng (có thể hoàn lại) tương ứng với mỗi thuộc tính
        /// </summary>
        [System.NonSerialized]
        private int[] unapplyPoints;

        /// <summary>
        /// Số điểm thuộc tính hiện có
        /// </summary>
        [System.NonSerialized]
        private int currentPoints;

        /// <summary>
        /// Tổng số điểm thuộc tính đã sở hữu
        /// </summary>
        [System.NonSerialized]
        private int totalPoints;
        
        /// <summary>
        /// Thành phần cấp độ
        /// </summary>
        [System.NonSerialized]
        private Level level;

        /// <summary>
        /// Thành phần thuộc tính
        /// </summary>
        [System.NonSerialized]
        private Attribute attribute;

        public int[] ApplyPoints
        {
            get { return applyPoints; }
        }

        private void Awake()
        {
            level = GetComponent<Level>();
            attribute = GetComponent<Attribute>();

            totalPoints = level.CurrentLevel * pointBonus;
            currentPoints = totalPoints;

            modifiers = new Modifier[data.AttributePointCount];
            applyPoints = new int[data.AttributePointCount];
            unapplyPoints = new int[data.AttributePointCount];

            int i = 0;
            foreach (AttributePointData.PointModifier pointModifier in data.PointModifiers)
            {
                AttributeValue attributeValue = attribute.GetAttributeOfType(pointModifier.Type);

                Modifier modifier = new Modifier(0f, ModifierType.Add);
                modifier.Value = applyPoints[i] * pointModifier.Bonus;
                modifiers[i] = modifier;
                currentPoints -= applyPoints[i];

                attributeValue.AddModifier(modifier);

                i++;
            }

            level.OnLevelChanged += LevelComponent_OnCurrentLevelChanged;
        }

        private void OnDisable()
        {
            if (level != null)
            {
                level.OnLevelChanged -= LevelComponent_OnCurrentLevelChanged;
            }

            if (attribute != null)
            {
                int i = 0;
                foreach (AttributePointData.PointModifier pointModifier in data.PointModifiers)
                {
                    AttributeValue attributeValue = attribute.GetAttributeOfType(pointModifier.Type);

                    attributeValue.RemoveModifier(modifiers[i]);

                    i++;
                }
            }
        }

        public void SetApplyPoint(int[] copy)
        {
            for (int i = 0; i < applyPoints.Length; i++)
            {
                if (i >= copy.Length)
                    continue;

                applyPoints[i] = copy[i];
            }
        }

        /// <summary>
        /// Tăng điểm cộng thêm cho thuộc tính chỉ định
        /// </summary>
        /// <param name="type"> Kiểu thuộc tính </param>
        /// <param name="point"> Điểm tăng </param>
        public void AddPoint(AttributeType type, int point)
        {
            if (currentPoints < point)
                return;

            int i = 0;
            foreach (AttributePointData.PointModifier pointModifier in data.PointModifiers)
            {
                if (pointModifier.Type == type)
                {
                    unapplyPoints[i] += point;
                    currentPoints -= point;
                    modifiers[i].Value = pointModifier.Bonus * (applyPoints[i] + unapplyPoints[i]);
                    return;
                }

                i++;
            }
        }

        /// <summary>
        /// Giảm điểm cộng thêm cho thuộc tính chỉ định
        /// </summary>
        /// <param name="type"> Kiểu thuộc tính </param>
        /// <param name="point"> Điểm giảm </param>
        public void RemovePoint(AttributeType type, int point)
        {
            int i = 0;
            foreach (AttributePointData.PointModifier pointModifier in data.PointModifiers)
            {
                if (pointModifier.Type == type)
                {
                    if (unapplyPoints[i] < point)
                        return;

                    unapplyPoints[i] -= point;
                    currentPoints += point;
                    modifiers[i].Value = pointModifier.Bonus * (applyPoints[i] + unapplyPoints[i]);
                    return;
                }

                i++;
            }
        }

        /// <summary>
        /// Áp dụng các điểm đã tăng trước đó
        /// </summary>
        public void ApplyPoint()
        {
            int i = 0;
            foreach (AttributePointData.PointModifier pointModifier in data.PointModifiers)
            {
                applyPoints[i] += unapplyPoints[i];
                unapplyPoints[i] = 0;
                modifiers[i].Value = pointModifier.Bonus * applyPoints[i];

                i++;
            }
        }

        /// <summary>
        /// Hoàn lại các điểm đã tăng trước đó
        /// </summary>
        public void UnapplyPoint()
        {
            int i = 0;
            foreach (AttributePointData.PointModifier pointModifier in data.PointModifiers)
            {
                currentPoints += unapplyPoints[i];
                unapplyPoints[i] = 0;
                modifiers[i].Value = pointModifier.Bonus * applyPoints[i];

                i++;
            }
        }

        /// <summary>
        /// Reset tất cả điểm thuộc tính
        /// </summary>
        public void ResetPoint()
        {
            for (int i = 0; i < applyPoints.Length; i++)
            {
                applyPoints[i] = 0;
                unapplyPoints[i] = 0;
                modifiers[i].Value = 0f;
            }

            currentPoints = totalPoints;
        }

        /// <summary>
        /// Callback gọi lại khi cấp độ nhân vật thay đổi
        /// </summary>
        /// <param name="newLevel"> Cấp độ mới </param>
        private void LevelComponent_OnCurrentLevelChanged(int newLevel)
        {
            int oldTotalPoints = totalPoints;
            totalPoints = pointBonus * newLevel;

            currentPoints += totalPoints - oldTotalPoints;
        }
    }
}