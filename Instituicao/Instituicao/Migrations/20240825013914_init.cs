using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instituicao.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    CurID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurNome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurTotalPeriodos = table.Column<int>(type: "int", nullable: true),
                    CurTurno = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.CurID);
                });

            migrationBuilder.CreateTable(
                name: "Professores",
                columns: table => new
                {
                    ProMatricula = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuCPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuTelefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuDN = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professores", x => x.ProMatricula);
                });

            migrationBuilder.CreateTable(
                name: "Periodos",
                columns: table => new
                {
                    PerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerNumero = table.Column<int>(type: "int", nullable: true),
                    PerSala = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periodos", x => x.PerID);
                    table.ForeignKey(
                        name: "FK_Periodos_Cursos_CurID",
                        column: x => x.CurID,
                        principalTable: "Cursos",
                        principalColumn: "CurID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Disciplinas",
                columns: table => new
                {
                    DisID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisNome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisHoras = table.Column<int>(type: "int", nullable: true),
                    ProMatricula = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas", x => x.DisID);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Professores_ProMatricula",
                        column: x => x.ProMatricula,
                        principalTable: "Professores",
                        principalColumn: "ProMatricula");
                });

            migrationBuilder.CreateTable(
                name: "Orientadores",
                columns: table => new
                {
                    OrtID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProMatricula = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orientadores", x => x.OrtID);
                    table.ForeignKey(
                        name: "FK_Orientadores_Professores_ProMatricula",
                        column: x => x.ProMatricula,
                        principalTable: "Professores",
                        principalColumn: "ProMatricula",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    AluMatricula = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerID = table.Column<int>(type: "int", nullable: true),
                    UsuNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuCPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuTelefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuDN = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.AluMatricula);
                    table.ForeignKey(
                        name: "FK_Alunos_Periodos_PerID",
                        column: x => x.PerID,
                        principalTable: "Periodos",
                        principalColumn: "PerID");
                });

            migrationBuilder.CreateTable(
                name: "DisciplinaPeriodo",
                columns: table => new
                {
                    DisPeriodosPerID = table.Column<int>(type: "int", nullable: false),
                    PerDisciplinasDisID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplinaPeriodo", x => new { x.DisPeriodosPerID, x.PerDisciplinasDisID });
                    table.ForeignKey(
                        name: "FK_DisciplinaPeriodo_Disciplinas_PerDisciplinasDisID",
                        column: x => x.PerDisciplinasDisID,
                        principalTable: "Disciplinas",
                        principalColumn: "DisID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplinaPeriodo_Periodos_DisPeriodosPerID",
                        column: x => x.DisPeriodosPerID,
                        principalTable: "Periodos",
                        principalColumn: "PerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trabalhos",
                columns: table => new
                {
                    TraID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TraTitulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TraValor = table.Column<double>(type: "float", nullable: true),
                    TraNota = table.Column<double>(type: "float", nullable: true),
                    DisID = table.Column<int>(type: "int", nullable: true),
                    OrtID = table.Column<int>(type: "int", nullable: true),
                    TipoTrabalho = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabalhos", x => x.TraID);
                    table.ForeignKey(
                        name: "FK_Trabalhos_Disciplinas_DisID",
                        column: x => x.DisID,
                        principalTable: "Disciplinas",
                        principalColumn: "DisID");
                    table.ForeignKey(
                        name: "FK_Trabalhos_Orientadores_OrtID",
                        column: x => x.OrtID,
                        principalTable: "Orientadores",
                        principalColumn: "OrtID");
                });

            migrationBuilder.CreateTable(
                name: "Dependencia",
                columns: table => new
                {
                    AluDependenciasDisID = table.Column<int>(type: "int", nullable: false),
                    DisDependentesAluMatricula = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependencia", x => new { x.AluDependenciasDisID, x.DisDependentesAluMatricula });
                    table.ForeignKey(
                        name: "FK_Dependencia_Alunos_DisDependentesAluMatricula",
                        column: x => x.DisDependentesAluMatricula,
                        principalTable: "Alunos",
                        principalColumn: "AluMatricula",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dependencia_Disciplinas_AluDependenciasDisID",
                        column: x => x.AluDependenciasDisID,
                        principalTable: "Disciplinas",
                        principalColumn: "DisID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlunoTrabalho",
                columns: table => new
                {
                    AluTrabalhosTraID = table.Column<int>(type: "int", nullable: false),
                    TraAlunosAluMatricula = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoTrabalho", x => new { x.AluTrabalhosTraID, x.TraAlunosAluMatricula });
                    table.ForeignKey(
                        name: "FK_AlunoTrabalho_Alunos_TraAlunosAluMatricula",
                        column: x => x.TraAlunosAluMatricula,
                        principalTable: "Alunos",
                        principalColumn: "AluMatricula",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunoTrabalho_Trabalhos_AluTrabalhosTraID",
                        column: x => x.AluTrabalhosTraID,
                        principalTable: "Trabalhos",
                        principalColumn: "TraID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_PerID",
                table: "Alunos",
                column: "PerID");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoTrabalho_TraAlunosAluMatricula",
                table: "AlunoTrabalho",
                column: "TraAlunosAluMatricula");

            migrationBuilder.CreateIndex(
                name: "IX_Dependencia_DisDependentesAluMatricula",
                table: "Dependencia",
                column: "DisDependentesAluMatricula");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaPeriodo_PerDisciplinasDisID",
                table: "DisciplinaPeriodo",
                column: "PerDisciplinasDisID");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_ProMatricula",
                table: "Disciplinas",
                column: "ProMatricula");

            migrationBuilder.CreateIndex(
                name: "IX_Orientadores_ProMatricula",
                table: "Orientadores",
                column: "ProMatricula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Periodos_CurID",
                table: "Periodos",
                column: "CurID");

            migrationBuilder.CreateIndex(
                name: "IX_Trabalhos_DisID",
                table: "Trabalhos",
                column: "DisID");

            migrationBuilder.CreateIndex(
                name: "IX_Trabalhos_OrtID",
                table: "Trabalhos",
                column: "OrtID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunoTrabalho");

            migrationBuilder.DropTable(
                name: "Dependencia");

            migrationBuilder.DropTable(
                name: "DisciplinaPeriodo");

            migrationBuilder.DropTable(
                name: "Trabalhos");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Disciplinas");

            migrationBuilder.DropTable(
                name: "Orientadores");

            migrationBuilder.DropTable(
                name: "Periodos");

            migrationBuilder.DropTable(
                name: "Professores");

            migrationBuilder.DropTable(
                name: "Cursos");
        }
    }
}
