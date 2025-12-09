using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Core.Models
{
    public class Attempts
    {
        public long Id { get; set; }
        public long EvaluationId { get; set; }
        public long StudentId { get; set; }
        public DateTime StartedAt { get; set; }

        // Columna real en la BD
        public DateTime? SubmittedAt { get; set; }

        // Columna real en la BD
        public decimal? Score { get; set; }

        // Navegación
        public Evaluations Evaluation { get; set; }
        public Users Student { get; set; }
        public ICollection<Answers> Answers { get; set; }

        // ======== ALIAS (solo lógica de negocio, sin cambiar la BD) ========

        // FinishedAt usa internamente SubmittedAt
        [NotMapped]
        public DateTime? FinishedAt
        {
            get => SubmittedAt;
            set => SubmittedAt = value;
        }

        // Número de intento (no existe en la tabla, solo en código)
        [NotMapped]
        public int AttemptNumber { get; set; }

        // ScoreObtained usa internamente Score
        [NotMapped]
        public decimal? ScoreObtained
        {
            get => Score;
            set => Score = value;
        }

        // Si aprobó o no (no existe en la tabla)
        [NotMapped]
        public bool? Passed { get; set; }
    }
}
