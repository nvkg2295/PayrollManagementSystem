USE [PayrollDB];
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_RunPayroll]
(
    @Month INT,
    @Year INT
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        -- 1. Check if payroll already exists
        IF EXISTS
        (
            SELECT 1
            FROM PayrollRuns
            WHERE PayrollMonth = @Month
              AND PayrollYear = @Year
        )
        BEGIN
            THROW 50001, 'Payroll already generated for the selected month and year.', 1;
        END;

        -- 2. Check if attendance exists
        IF NOT EXISTS
        (
            SELECT 1
            FROM Attendance
            WHERE AttendanceMonth = @Month
              AND AttendanceYear = @Year
        )
        BEGIN
            THROW 50002, 'No attendance records found for the selected month and year.', 1;
        END;

        -- 3. Insert payroll run
        INSERT INTO PayrollRuns (PayrollMonth, PayrollYear)
        VALUES (@Month, @Year);

        DECLARE @PayrollRunId INT = SCOPE_IDENTITY();

        -- 4. Generate payroll details
        INSERT INTO PayrollDetails
        (
            PayrollRunId,
            EmployeeId,
            GrossPay,
            PfDeduction,
            ProfessionalTax,
            NetPay
        )
        SELECT
            @PayrollRunId,
            e.EmployeeId,

            -- Gross Pay calculation
            ROUND((e.BasicSalary / a.Workingdays) * a.DaysPresent, 2) AS GrossPay,

            -- PF (12%)
            ROUND(e.BasicSalary * 0.12, 2) AS PfDeduction,

            -- Fixed Professional Tax
            200 AS ProfessionalTax,

            -- Net Pay
            ROUND(
                ((e.BasicSalary / a.Workingdays) * a.DaysPresent)
                - (e.BasicSalary * 0.12)
                - 200,
                2
            ) AS NetPay

        FROM Employees e
        INNER JOIN Attendance a
            ON e.EmployeeId = a.EmployeeId
        WHERE a.AttendanceMonth = @Month
          AND a.AttendanceYear = @Year;

    END TRY

    BEGIN CATCH

        -- Return error to application
        THROW;

    END CATCH
END;
GO