namespace ASSSK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComplexDataModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        EmployeQuantity = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        TeacherID = c.Int(),
                    })
                .PrimaryKey(t => t.DepartmentID)
                .ForeignKey("dbo.Teacher", t => t.TeacherID)
                .Index(t => t.TeacherID);
            
            CreateTable(
                "dbo.Teacher",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        HireDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OfficeAssignment",
                c => new
                    {
                        TeacherID = c.Int(nullable: false),
                        Location = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.TeacherID)
                .ForeignKey("dbo.Teacher", t => t.TeacherID)
                .Index(t => t.TeacherID);
            
            CreateTable(
                "dbo.CourseTeacher",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        TeacherID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseID, t.TeacherID })
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Teacher", t => t.TeacherID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.TeacherID);


            //create a department for cause to point to..
            Sql("INSERT INTO dbo.Department (Name, EmployeQuantity, StartDate) VALUES ('Temp', 0, GETDATE())");
            //default value for FK points to department created above...
            AddColumn("dbo.Course", "DepartmentID", c => c.Int(nullable: false, defaultValue: 1));
            //AddColumn("dbo.Course", "DepartmentID", c => c.Int(nullable: false));
            AlterColumn("dbo.Course", "Title", c => c.String(maxLength: 50));
            AlterColumn("dbo.Student", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Student", "FirstName", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Course", "DepartmentID");
            AddForeignKey("dbo.Course", "DepartmentID", "dbo.Department", "DepartmentID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseTeacher", "TeacherID", "dbo.Teacher");
            DropForeignKey("dbo.CourseTeacher", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Course", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Department", "TeacherID", "dbo.Teacher");
            DropForeignKey("dbo.OfficeAssignment", "TeacherID", "dbo.Teacher");
            DropIndex("dbo.CourseTeacher", new[] { "TeacherID" });
            DropIndex("dbo.CourseTeacher", new[] { "CourseID" });
            DropIndex("dbo.OfficeAssignment", new[] { "TeacherID" });
            DropIndex("dbo.Department", new[] { "TeacherID" });
            DropIndex("dbo.Course", new[] { "DepartmentID" });
            AlterColumn("dbo.Student", "FirstName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Student", "LastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Course", "Title", c => c.String());
            DropColumn("dbo.Course", "DepartmentID");
            DropTable("dbo.CourseTeacher");
            DropTable("dbo.OfficeAssignment");
            DropTable("dbo.Teacher");
            DropTable("dbo.Department");
        }
    }
}
