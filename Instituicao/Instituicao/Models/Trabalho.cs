namespace Instituicao.Models
{
    public abstract class Trabalho
    {
        public int TraID { get; set; }
        public string TraTitulo { get; set; } = "SemTitulo";
        public double ?TraValor { get; set; }
        public double ?TraNota { get; set; }

        //***************************************************************
        // Relacionamento muitos-para-1 com Disciplina. 1Disciplina
        public int ?DisID { get; set; }
        public virtual Disciplina ?TraDisciplina { get; set; }


        // Relacionamento muitos-para-muitos com Aluno. MAlunos
        public List<Aluno> TraAlunos { get; set; } = new List<Aluno>();


        // Relacionamento muitos-para-1 com Orientador. 1Orientador
        public int ?OrtID { get; set; }
        public virtual Orientador ?TraOrientador { get; set; }

    }
}
