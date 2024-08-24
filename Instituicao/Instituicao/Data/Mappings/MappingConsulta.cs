using Microsoft.EntityFrameworkCore;
using Instituicao.Models;


namespace Instituicao.Data.Mappings
{
    public static class MappingConsulta
    {
        public static void ConsultaMapeamento(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
            .HasKey(a => a.UsuMatricula);

            modelBuilder.Entity<Orientador>()
            .HasKey(a => a.OrtID);


            modelBuilder.Entity<Periodo>()
            .HasKey(a => a.PerID);

            modelBuilder.Entity<Curso>()
            .HasKey(a => a.CurID);

            modelBuilder.Entity<Disciplina>()
            .HasKey(a => a.DisID);


            modelBuilder.Entity<Trabalho>()
            .HasKey(a => a.TraID);

            //Configurações de herança(TPH como exemplo)
            modelBuilder.Entity<Trabalho>()
            .HasDiscriminator<string>("Tipo de Trabalho")
            .HasValue<TCC>("TCC")
            .HasValue<Artigo>("Artigo")
            .HasValue<Outro>("Outro");

            modelBuilder.Entity<Usuario>()
            .HasDiscriminator<string>("Tipo de Usuario")
            .HasValue<Aluno>("Aluno")
            .HasValue<Professor>("Professor");

            //ALUNO ------------------------------------------------------------------
            // Configuração para o relacionamento muitos-para-1 com Periodo
            modelBuilder.Entity<Aluno>()
                .HasOne(a => a.AluPeriodo) //1Aluno = 1Periodo
                .WithMany(p => p.PerAlunos) //1Periodo = *Alunos
                .HasForeignKey(a => a.PerID); // Aluno tem PerID

            // Configuração para o relacionamento muitos-para-muitos com Disciplina
            modelBuilder.Entity<Aluno>()
                .HasMany(a => a.AluDependencias) //1Aluno = *Disciplinas
                .WithMany(d => d.DisDependentes) //1Disciplina = *Alunos
                .UsingEntity(j => j.ToTable("Dependencia"));

            // Configuração para o relacionamento muitos-para-muitos com Trabalho
            modelBuilder.Entity<Aluno>()
                .HasMany(a => a.AluTrabalhos) //1Aluno = *Trabalhos
                .WithMany(t => t.TraAlunos); //1Trabalho = *Alunos

            //CURSO ------------------------------------------------------------------
            // Configuração para o relacionamento um-para-muitos com Periodo
            modelBuilder.Entity<Curso>()
                .HasMany(c => c.CurPeriodos) //1Curso = *Periodos
                .WithOne(p => p.PerCurso) //1Periodo = 1Curso
                .HasForeignKey(p => p.CurID) //Periodo tem CurID
                .IsRequired();

            //PERIODO ------------------------------------------------------------------
            // Configuração para o relacionamento muitos-para-muitos com Disciplina
            modelBuilder.Entity<Periodo>()
                .HasMany(p => p.PerDisciplinas) //1Periodo = *Disciplinas
                .WithMany(d => d.DisPeriodos); //1Disciplina = *Periodos

            //ORIENTADOR ------------------------------------------------------------------
            // Configuração para o relacionamento de 1-para-1 com Professor
            modelBuilder.Entity<Orientador>()
                .HasOne(o => o.OrtProfessor)   //1Orientador = 1Professor
                .WithOne(p => p.ProOrientador) //1Professor = 1Orientador
                .HasForeignKey<Orientador>(o => o.ProMatricula) //Orientador tem ProMatricula
                .IsRequired();

            // Configuração para o relacionamento de 1-para-muitos com Trabalho
            modelBuilder.Entity<Orientador>()
                .HasMany(o => o.OrtTrabalhos) //1Orientador = *Trabalhos
                .WithOne(t => t.TraOrientador) //1Trabalho = 1Orientador
                .HasForeignKey(t => t.OrtID); //Trabalho tem OrtID

            //DISCIPLINA ------------------------------------------------------------------
            // Configuração para o relacionamento de muitos-para-um com Professor
            modelBuilder.Entity<Disciplina>()
                .HasOne(d => d.DisProfessor) //1Disciplina = 1Professor
                .WithMany(p => p.ProDisciplinas) //1Professor = *Disciplinas
                .HasForeignKey(d => d.ProMatricula); //Disciplina tem ProMatricula

            // Configuração para o relacionamento de um-para-muitos com Trabalhos
            modelBuilder.Entity<Disciplina>()
                .HasMany(d => d.DisTrabalhos) //1Disciplina = *Trabalhos
                .WithOne(t => t.TraDisciplina) //1Trabalho = 1Disciplina
                .HasForeignKey(t => t.DisID); //Trabalho tem DisID
        }
    }
}
