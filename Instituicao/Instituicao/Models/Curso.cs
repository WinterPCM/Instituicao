namespace Instituicao.Models
{
    public class Curso
    {
        public int CurID { get; set; }
        public string ?CurNome { get; set; }
        public int ?CurTotalPeriodos { get; set; }
        public string ?CurTurno { get; set; }

        //*************************************************************
        // Relacionamento 1-para-muitos com Periodo. MPeriodos
        public List<Periodo> CurPeriodos { get; set; } = new List<Periodo>();


    }
}
