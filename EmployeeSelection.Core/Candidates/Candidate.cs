namespace EmployeeSelection.Core.Candidates
{
    /// <summary>
    /// Представляет информацию о кандидате на вакансию.
    /// </summary>
    public class Candidate
    {
        /// <summary>
        /// Имя кандидата.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Основной навык/специализация кандидата.
        /// </summary>
        public string Skill { get; set; }

        /// <summary>
        /// Опыт работы кандидата в годах.
        /// </summary>
        public int Experience { get; set; }
    }
}
