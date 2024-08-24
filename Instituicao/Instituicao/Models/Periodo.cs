namespace Instituicao.Models
{
    public class Periodo
    {
        public int PerID { get; set; }
        public int ?PerNumero { get; set; }
        public string ?PerSala { get; set; }

        //**********************************************************************
        // Relacionamento muitos-para-1 com Curso. 1Curso
        public int CurID { get; set; }
        public virtual Curso PerCurso { get; set; }

        // Relacionamento 1-para-muitos com Aluno. MAlunos
        public List<Aluno> PerAlunos { get; set; } = new List<Aluno>();

        // Relacionamento muitos-para-muitos com Disciplina. MDisciplinas
        public List<Disciplina> PerDisciplinas { get; set; } = new List<Disciplina>();

    }
}
