namespace Instituicao.Models
{
    public class TrabalhoViewModel
    {
        public int TraID { get; set; }
        public string TraTitulo { get; set; } = "SemTitulo";
        public double? TraValor { get; set; }
        public double? TraNota { get; set; }

        // Propriedade para o Discriminator
        public string TipoTrabalho { get; set; }

        public int DisID { get; set; }
        public int OrtID { get; set; }

    }
}
