namespace ASSSK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DepartmentStoredProcedure : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.Department_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 50),
                        EmployeQuantity = p.Int(),
                        StartDate = p.DateTime(),
                        TeacherID = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Department]([Name], [EmployeQuantity], [StartDate], [TeacherID])
                      VALUES (@Name, @EmployeQuantity, @StartDate, @TeacherID)
                      
                      DECLARE @DepartmentID int
                      SELECT @DepartmentID = [DepartmentID]
                      FROM [dbo].[Department]
                      WHERE @@ROWCOUNT > 0 AND [DepartmentID] = scope_identity()
                      
                      SELECT t0.[DepartmentID]
                      FROM [dbo].[Department] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[DepartmentID] = @DepartmentID"
            );
            
            CreateStoredProcedure(
                "dbo.Department_Update",
                p => new
                    {
                        DepartmentID = p.Int(),
                        Name = p.String(maxLength: 50),
                        EmployeQuantity = p.Int(),
                        StartDate = p.DateTime(),
                        TeacherID = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Department]
                      SET [Name] = @Name, [EmployeQuantity] = @EmployeQuantity, [StartDate] = @StartDate, [TeacherID] = @TeacherID
                      WHERE ([DepartmentID] = @DepartmentID)"
            );
            
            CreateStoredProcedure(
                "dbo.Department_Delete",
                p => new
                    {
                        DepartmentID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Department]
                      WHERE ([DepartmentID] = @DepartmentID)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Department_Delete");
            DropStoredProcedure("dbo.Department_Update");
            DropStoredProcedure("dbo.Department_Insert");
        }
    }
}
