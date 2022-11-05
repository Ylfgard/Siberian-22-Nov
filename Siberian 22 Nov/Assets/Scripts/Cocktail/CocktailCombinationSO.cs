using UnityEngine;

namespace Cocktails
{
    [CreateAssetMenu (fileName = "NewCombination", menuName = "ScriptableObject/Combination")]
    public class CocktailCombinationSO : ScriptableObject
    {
        [SerializeField] private CocktailParametersSO _parameters;
        [SerializeField] private bool _ice;
        [SerializeField] private CocktailParametersSO[] _ingridients;
        [SerializeField] private int _anyIngridientCount;
        [SerializeField] private Color _color;

        public CocktailParametersSO Parameters => _parameters;
        public bool Ice => _ice;
        public CocktailParametersSO[] Ingridients => _ingridients;
        public int AnyIngridientCount => _anyIngridientCount;
        public Color Color => _color;
    }
}
