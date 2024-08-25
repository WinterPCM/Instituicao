namespace Instituicao.Models
{
    public class Professor : Usuario
    {
        public int ProMatricula { get; set; }

        //****************************************************************
        // Relacionamento 1-para-muitos com Disciplina. MDisciplinas
        public List<Disciplina> ProDisciplinas { get; set; } = new List<Disciplina>();


        // Relacionamento 1-para-1 com Orientador. 1Orientador
        public virtual Orientador ?ProOrientador { get; set; }

    }
}
