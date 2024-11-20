using Common;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DisplayHP : MonoBehaviour
    {
        private float _maxHealth;

        private void Awake()
        {
            _maxHealth = GameObject.FindWithTag(Tags.PlayerTag).GetComponentInChildren<Health>().CurrentHealth;
        }

        void Update()
        {
            // Принимаем количество здоровья у игрока
            float hp = GameObject.FindWithTag(Tags.PlayerTag).GetComponentInChildren<Health>().CurrentHealth;
        
            // Меняем цвет выводимого текста здоровья игрока
            if(hp > _maxHealth * 0.7f && hp <= _maxHealth)  transform.GetComponent<Text>().color = Color.green;
            if(hp > _maxHealth * 0.3f && hp <= _maxHealth * 0.7f) transform.GetComponent<Text>().color = Color.yellow;
            if(hp > 0 && hp <= _maxHealth * 0.3f) transform.GetComponent<Text>().color = Color.red;

            // Меняем выводимый текст здоровья игрока
            transform.GetComponent<Text>().text = hp.ToString();
        }
    }
}
