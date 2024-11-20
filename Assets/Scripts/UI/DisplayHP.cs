using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DisplayHP : MonoBehaviour
    {
        float MaxHealth;

        private void Awake()
        {
            MaxHealth = GameObject.FindWithTag("Player").GetComponentInChildren<Health>().currentHealth;
        }

        void Update()
        {
            // Принимаем количество здоровья у игрока
            float hp = GameObject.FindWithTag("Player").GetComponentInChildren<Health>().currentHealth;
        
            // Меняем цвет выводимого текста здоровья игрока
            if(hp > MaxHealth * 0.7f && hp <= MaxHealth)  transform.GetComponent<Text>().color = Color.green;
            if(hp > MaxHealth * 0.3f && hp <= MaxHealth * 0.7f) transform.GetComponent<Text>().color = Color.yellow;
            if(hp > 0 && hp <= MaxHealth * 0.3f) transform.GetComponent<Text>().color = Color.red;

            // Меняем выводимый текст здоровья игрока
            transform.GetComponent<Text>().text = hp.ToString();
        }
    }
}
