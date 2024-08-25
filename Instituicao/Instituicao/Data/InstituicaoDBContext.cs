using Microsoft.EntityFrameworkCore;
using Instituicao.Models;
using Instituicao.Data.Mappings;


namespace Instituicao.Data
{
    public class InstituicaoDBContext : DbContext
    {
        public InstituicaoDBContext(DbContextOptions<InstituicaoDBContext> options) : base(options) { }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Orientador> Orientadores { get; set; }

        public DbSet<Periodo> Periodos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }

        public DbSet<Trabalho> Trabalhos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConsultaMapeamento();
        }

    }
}
