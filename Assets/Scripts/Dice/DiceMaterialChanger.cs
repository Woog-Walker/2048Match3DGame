using UnityEngine;
using System.Collections.Generic;

namespace DiceGame.SingleDice.Materials
{
    public class DiceMaterialChanger : MonoBehaviour
    {
        [SerializeField] List<Material> listOfMaterials = new List<Material>();
        Renderer _renderer;

        private void Awake() => _renderer = GetComponent<Renderer>(); 

        public void ChangeDiceMaterial(int incIndex) => _renderer.material = listOfMaterials[incIndex];
    }
}