using Microsoft.EntityFrameworkCore;
using Instituicao.Models;


namespace Instituicao.Data.Mappings
{
    public static class MappingConsulta
    {
        public static void ConsultaMapeamento(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>()
            .HasKey(a => a.AluMatricula);

            modelBuilder.Entity<Professor>()
            .HasKey(a => a.ProMatricula);

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

            //Configurações de herança(TPH)
            modelBuilder.Entity<Trabalho>()
            .HasDiscriminator<string>("TipoTrabalho")
            .HasValue<TCC>("TCC")
            .HasValue<Artigo>("Artigo")
            .HasValue<Outro>("Outro");

            //ALUNO ------------------------------------------------------------------
            // Configuração para o relacionamento muitos-para-1 Periodo
            modelBuilder.Entity<Aluno>()
                .HasOne(a => a.AluPeriodo) // 1Aluno = 1Periodo
                .WithMany(p => p.PerAlunos) // 1Periodo = *Alunos
                .HasForeignKey(a => a.PerID); // Aluno tem PerID

            // Configuração para o relacionamento muitos-para-muitos Disciplina
            modelBuilder.Entity<Aluno>()
                .HasMany(a => a.AluDependencias) // 1Aluno = *Disciplinas
                .WithMany(d => d.DisDependentes) // 1Disciplina = *Alunos
                .UsingEntity(j => j.ToTable("Dependencia"));

            // Configuração para o relacionamento muitos-para-muitos Trabalho
            modelBuilder.Entity<Aluno>()
                .HasMany(a => a.AluTrabalhos) // 1Aluno = *Trabalhos
                .WithMany(t => t.TraAlunos); // 1Trabalho = *Alunos

            //PERIODO ------------------------------------------------------------------
            // Configuração para o relacionamento muitos-para-um Curso
            modelBuilder.Entity<Periodo>()
                .HasOne(p => p.PerCurso) // 1Periodo = 1 Curso
                .WithMany(c => c.CurPeriodos) // 1Curso = *Periodos
                .HasForeignKey(p => p.CurID); // Periodo tem CurID

            // Configuração para o relacionamento muitos-para-muitos Disciplina
            modelBuilder.Entity<Periodo>()
                .HasMany(p => p.PerDisciplinas) //1Periodo = *Disciplinas
                .WithMany(d => d.DisPeriodos); //1Disciplina = *Periodos

            //ORIENTADOR ------------------------------------------------------------------
            // Configuração para o relacionamento de 1-para-1 Professor
            modelBuilder.Entity<Orientador>()
                .HasOne(o => o.OrtProfessor)   //1Orientador = 1Professor
                .WithOne(p => p.ProOrientador) //1Professor = 1Orientador
                .HasForeignKey<Orientador>(o => o.ProMatricula); //Orientador tem ProMatricula

            //DISCIPLINA ------------------------------------------------------------------
            // Configuração para o relacionamento de muitos-para-um Professor
            modelBuilder.Entity<Disciplina>()
                .HasOne(d => d.DisProfessor) //1Disciplina = 1Professor
                .WithMany(p => p.ProDisciplinas) //1Professor = *Disciplinas
                .HasForeignKey(d => d.ProMatricula); //Disciplina tem ProMatricula

            //TRABALHO ------------------------------------------------------------------
            // Configuração para o relacionamento de muitos-para-um Disciplina
            modelBuilder.Entity<Trabalho>() 
                .HasOne(t => t.TraDisciplina) // 1Trabalho = 1Disciplina
                .WithMany(d => d.DisTrabalhos) // 1Disciplina = *Trabalhos
                .HasForeignKey(t => t.DisID); // Trabalho tem DisID

            // Configuração para o relacionamento de muitos-para-um Orientador
            modelBuilder.Entity<Trabalho>()
                .HasOne(t => t.TraOrientador) // 1Trabalho = 1Orientador
                .WithMany(o => o.OrtTrabalhos) // 1Orientador = *Trabalhos
                .HasForeignKey(t => t.OrtID); // Trabalho tem OrtID

        }
    }
}
