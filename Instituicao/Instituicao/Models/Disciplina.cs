namespace Instituicao.Models
{
    public class Disciplina
    {
        public int DisID { get; set; }
        public string ?DisNome { get; set; }
        public int ?DisHoras { get; set; }

        //***************************************************************
        // Relacionamento muitos-para-muitos com Periodos. MPeriodos
        public List<Periodo> DisPeriodos { get; set; } = new List<Periodo>();


        // Relacionamento muitos-para-muitos com Aluno. MAlunos
        public List<Aluno> DisDependentes { get; set; } = new List<Aluno>();


        // Relacionamento 1-para-muitos com Trabalho. MTrabalhos
        public List<Trabalho> DisTrabalhos { get; set; } = new List<Trabalho>();


        // Relacionamento muitos-para-1 com Professor. 1Professor
        public int ?ProMatricula { get; set; }
        public virtual Professor ?DisProfessor { get; set; }

    }
}
