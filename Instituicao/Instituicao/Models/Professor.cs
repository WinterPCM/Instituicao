namespace Instituicao.Models
{
    public class Professor : Usuario
    {
        //****************************************************************
        // Relacionamento 1-para-muitos com Disciplina. MDisciplinas
        public List<Disciplina> ProDisciplinas { get; set; } = new List<Disciplina>();


        // Relacionamento 1-para-1 com Orientador. 1Orientador
        public int ?OrtID { get; set; }
        public virtual Orientador ?ProOrientador { get; set; }

    }
}
