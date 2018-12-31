using UnityEngine;

namespace VinhNguyen
{
    /// <summary>
    /// Thêm tiền thưởng cho thuộc tính dựa trên cấp độ nhân vật
    /// </summary>
    [RequireComponent(typeof(Level))]
    [RequireComponent(typeof(Attribute))]
    public class AttributeLevel : MonoBehaviour
    {
        [Tooltip("Các thuộc tính nhận tiền thưởng")]
        [SerializeField]
        private AttributeLevelData data;

        /// <summary>
        /// Các modifier tương ứng mỗi thuộc tính
        /// </summary>
        [System.NonSerialized]
        private Modifier[] modifiers;

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

        private void Awake()
        {
            level = GetComponent<Level>();
            attribute = GetComponent<Attribute>();

            modifiers = new Modifier[data.AttributeLevelCount];

            int i = 0;
            foreach (AttributeLevelData.LevelModifier attributeLevel in data.LevelModifiers)
            {
                AttributeValue attributeValue = attribute.GetAttributeOfType(attributeLevel.Type);
                Modifier modifier = new Modifier(0f, ModifierType.Add);

                attributeValue.AddModifier(modifier);
                modifiers[i] = modifier;

                i++;
            }

            level.OnLevelChanged += LevelComponent_OnLevelUpped;
            LevelComponent_OnLevelUpped(level.CurrentLevel);
        }

        private void OnDestroy()
        {
            if (level != null)
            {
                level.OnLevelChanged -= LevelComponent_OnLevelUpped;
            }

            if (attribute != null)
            {
                int i = 0;
                foreach (AttributeLevelData.LevelModifier attributeLevel in data.LevelModifiers)
                {

                    AttributeValue attributeValue = attribute.GetAttributeOfType(attributeLevel.Type);

                    attributeValue.RemoveModifier(modifiers[i]);

                    i++;
                }
            }
        }

        private void LevelComponent_OnLevelUpped(int newLevel)
        {
            int i = 0;
            foreach (AttributeLevelData.LevelModifier attributeLevel in data.LevelModifiers)
            {
                modifiers[i].Value = attributeLevel.Bonus * newLevel;
            }
        }
    }
}