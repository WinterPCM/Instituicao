namespace Instituicao.Models
{
    public abstract class Usuario
    {
        public int UsuMatricula { get; set; }
        public string UsuNome { get; set; } = string.Empty;
        public string UsuCPF { get; set; } = string.Empty;
        public string UsuEmail { get; set; } = string.Empty;
        public string UsuTelefone { get; set; } = string.Empty;
        public DateTime ?UsuDN { get; set; }


    }
}
