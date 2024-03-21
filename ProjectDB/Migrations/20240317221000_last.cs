using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectDB.Migrations
{
    /// <inheritdoc />
    public partial class last : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DeptID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeptName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DeptID);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    InsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ins_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ins_Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.InsID);
                });

            migrationBuilder.CreateTable(
                name: "ITIBranches",
                columns: table => new
                {
                    BranchID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Branch_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITIBranches", x => x.BranchID);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Student_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Student_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Student_BD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dept_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_Student_Department_Dept_ID",
                        column: x => x.Dept_ID,
                        principalTable: "Department",
                        principalColumn: "DeptID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Course_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseWork = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ins_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK_Course_Instructors_Ins_ID",
                        column: x => x.Ins_ID,
                        principalTable: "Instructors",
                        principalColumn: "InsID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentsInstructors",
                columns: table => new
                {
                    DepartmentsDeptID = table.Column<int>(type: "int", nullable: false),
                    InstructorsInsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentsInstructors", x => new { x.DepartmentsDeptID, x.InstructorsInsID });
                    table.ForeignKey(
                        name: "FK_DepartmentsInstructors_Department_DepartmentsDeptID",
                        column: x => x.DepartmentsDeptID,
                        principalTable: "Department",
                        principalColumn: "DeptID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DepartmentsInstructors_Instructors_InstructorsInsID",
                        column: x => x.InstructorsInsID,
                        principalTable: "Instructors",
                        principalColumn: "InsID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentsITIBranches",
                columns: table => new
                {
                    DepartmentsDeptID = table.Column<int>(type: "int", nullable: false),
                    ITIBranchesBranchID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentsITIBranches", x => new { x.DepartmentsDeptID, x.ITIBranchesBranchID });
                    table.ForeignKey(
                        name: "FK_DepartmentsITIBranches_Department_DepartmentsDeptID",
                        column: x => x.DepartmentsDeptID,
                        principalTable: "Department",
                        principalColumn: "DeptID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DepartmentsITIBranches_ITIBranches_ITIBranchesBranchID",
                        column: x => x.ITIBranchesBranchID,
                        principalTable: "ITIBranches",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Course_Topics",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    Topics = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course_Topics", x => new { x.CourseID, x.Topics });
                    table.ForeignKey(
                        name: "FK_Course_Topics_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    ExamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total_Marks = table.Column<int>(type: "int", nullable: false),
                    CrsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.ExamID);
                    table.ForeignKey(
                        name: "FK_Exams_Course_CrsID",
                        column: x => x.CrsID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question_Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question_Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CrsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_Questions_Course_CrsID",
                        column: x => x.CrsID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Student_Courses",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    Student_Grade = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_Courses", x => new { x.StudentID, x.CourseID });
                    table.ForeignKey(
                        name: "FK_Student_Courses_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Student_Courses_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Choices",
                columns: table => new
                {
                    Choice_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Choice1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Choice2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Choice3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Q_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choices", x => x.Choice_ID);
                    table.ForeignKey(
                        name: "FK_Choices_Questions_Q_ID",
                        column: x => x.Q_ID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "examQuestionResponses",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    ExamID = table.Column<int>(type: "int", nullable: false),
                    QuestionID = table.Column<int>(type: "int", nullable: false),
                    StudentResponse = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_examQuestionResponses", x => new { x.StudentID, x.QuestionID, x.ExamID });
                    table.ForeignKey(
                        name: "FK_examQuestionResponses_Exams_ExamID",
                        column: x => x.ExamID,
                        principalTable: "Exams",
                        principalColumn: "ExamID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_examQuestionResponses_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_examQuestionResponses_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Choices_Q_ID",
                table: "Choices",
                column: "Q_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Course_Ins_ID",
                table: "Course",
                column: "Ins_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentsInstructors_InstructorsInsID",
                table: "DepartmentsInstructors",
                column: "InstructorsInsID");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentsITIBranches_ITIBranchesBranchID",
                table: "DepartmentsITIBranches",
                column: "ITIBranchesBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_examQuestionResponses_ExamID",
                table: "examQuestionResponses",
                column: "ExamID");

            migrationBuilder.CreateIndex(
                name: "IX_examQuestionResponses_QuestionID",
                table: "examQuestionResponses",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_CrsID",
                table: "Exams",
                column: "CrsID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CrsID",
                table: "Questions",
                column: "CrsID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Dept_ID",
                table: "Student",
                column: "Dept_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Courses_CourseID",
                table: "Student_Courses",
                column: "CourseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Choices");

            migrationBuilder.DropTable(
                name: "Course_Topics");

            migrationBuilder.DropTable(
                name: "DepartmentsInstructors");

            migrationBuilder.DropTable(
                name: "DepartmentsITIBranches");

            migrationBuilder.DropTable(
                name: "examQuestionResponses");

            migrationBuilder.DropTable(
                name: "Student_Courses");

            migrationBuilder.DropTable(
                name: "ITIBranches");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Instructors");
        }
    }
}
