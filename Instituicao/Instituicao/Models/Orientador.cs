namespace Instituicao.Models
{
    public class Orientador
    {
        public int OrtID { get; set; }

        //******************************************************
        // Relacionamento 1-para-1 com Professor. 1Professor
        public int ProMatricula { get; set; }
        public virtual Professor ?OrtProfessor { get; set; }

        // Relacionamento 1-para-muitos com Trabalho. MTrabalhos
        public List<Trabalho> OrtTrabalhos { get; set; } = new List<Trabalho>();

    }
}
