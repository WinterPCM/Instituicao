using System.ComponentModel.DataAnnotations.Schema;

namespace Instituicao.Models
{
    public class Aluno : Usuario
    {
        //************************************************************************************
        // Relacionamento muitos-para-1 com Periodo. 1Periodo
        public int ?PerID { get; set; }
        public virtual Periodo ?AluPeriodo { get; set; }


        // Relacionamento muitos-para-muitos com Disciplina. MDisciplinas
        public List<Disciplina> AluDependencias { get; set; } = new List<Disciplina>();


        // Relacionamento muitos-para-muitos com Trabalho. MTrabalhos
        public List<Trabalho> AluTrabalhos { get; set; } = new List<Trabalho>();

    }
}
