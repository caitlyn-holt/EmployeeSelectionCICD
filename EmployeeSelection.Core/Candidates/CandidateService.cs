using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace EmployeeSelection.Core.Candidates
{
    /// <summary>
    /// Сервис для работы с данными кандидатов: загрузка и фильтрация.
    /// </summary>
    public class CandidateService
    {
        /// <summary>
        /// Загружает список кандидатов из JSON-файла.
        /// </summary>
        /// <param name="path">Путь к JSON-файлу с данными кандидатов.</param>
        /// <returns>Список объектов <see cref="Candidate"/>.</returns>
        public List<Candidate> LoadFromFile(string path)
        {
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<Candidate>>(json);
        }

        /// <summary>
        /// Фильтрует кандидатов по заданному навыку.
        /// </summary>
        /// <param name="candidates">Список кандидатов для фильтрации.</param>
        /// <param name="skill">Навык для фильтрации.</param>
        /// <returns>Отфильтрованный список кандидатов.</returns>
        public List<Candidate> FilterBySkill(List<Candidate> candidates, string skill)
        {
            return candidates
                .Where(c => c.Skill == skill)
                .ToList();
        }
    }
}
