using TMPro;
using UnityEngine;

namespace _Project._Code
{
    public class NewMonoBehaviourScript : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        
        private void Awake()
        {
            Application.logMessageReceived += HandleLog; // Подписываемся на события логов Unity
        }

        private void OnDestroy()
        {
            Application.logMessageReceived -= HandleLog; // Отписываемся при выходе
        }

        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            if (text == null) return; // Проверка на null
            
            // Формируем сообщение
            string logEntry = $"[{type}] {logString}";

            // Добавляем стек-трейс, если это ошибка
            if (type == LogType.Error || type == LogType.Exception)
            {
                logEntry += $"\nStackTrace: {stackTrace}";
            }

            // Выводим в текстовый UI
            text.text += logEntry + "\n";
        }
        
    }
}
