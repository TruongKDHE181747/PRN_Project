using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Models;

public partial class Prn212Context : DbContext
{
    public Prn212Context()
    {
    }

    public Prn212Context(DbContextOptions<Prn212Context> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityHistory> ActivityHistories { get; set; }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<AttendanceStatus> AttendanceStatuses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<JobPosition> JobPositions { get; set; }

    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }

    public virtual DbSet<LeaveType> LeaveTypes { get; set; }

    public virtual DbSet<RequestStatus> RequestStatuses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server =(local); database = PRN212;uid=sa;pwd=123;TrustServerCertificate=True;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__Activity__A145B1FFE78DC77A");

            entity.ToTable("Activity_History");

            entity.Property(e => e.HistoryId).HasColumnName("History_id");
            entity.Property(e => e.Action).HasMaxLength(50);
            entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");
            entity.Property(e => e.Target).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithMany(p => p.ActivityHistories)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Activity___Emplo__571DF1D5");
        });

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__57FB453C0410F47D");

            entity.ToTable("Attendance");

            entity.Property(e => e.AttendanceId).HasColumnName("Attendance_id");
            entity.Property(e => e.AttendanceDate).HasColumnName("Attendance_date");
            entity.Property(e => e.AttendanceStatusId).HasColumnName("Attendance_status_id");
            entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");
            entity.Property(e => e.HoursWorked).HasColumnName("Hours_worked");
            entity.Property(e => e.OvertimeHour).HasColumnName("Overtime_hour");

            entity.HasOne(d => d.AttendanceStatus).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.AttendanceStatusId)
                .HasConstraintName("FK__Attendanc__Atten__68487DD7");

            entity.HasOne(d => d.Employee).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Attendanc__Emplo__6754599E");
        });

        modelBuilder.Entity<AttendanceStatus>(entity =>
        {
            entity.HasKey(e => e.AttendanceStatusId).HasName("PK__Attendan__BB5C0C6D98EA2961");

            entity.ToTable("Attendance_Status");

            entity.Property(e => e.AttendanceStatusId).HasColumnName("Attendance_status_id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .HasColumnName("Status_name");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__151571C9E4F31C1D");

            entity.Property(e => e.DepartmentId).HasColumnName("Department_id");
            entity.Property(e => e.DepartmentAddress)
                .HasMaxLength(50)
                .HasColumnName("Department_address");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .HasColumnName("Department_name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__781228D904B76B80");

            entity.HasIndex(e => e.UserName, "UQ__Employee__5F1A10865CA9DAAD").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "UQ__Employee__7E87EC6754C3100E").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Employee__A9D1053403075EC9").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.AvailableLeaveDays).HasColumnName("Available_leave_days");
            entity.Property(e => e.DepartmentId).HasColumnName("Department_id");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.EndDate).HasColumnName("End_date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("First_name");
            entity.Property(e => e.JobPositionId).HasColumnName("Job_position_id");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("Last_name");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("Phone_number");
            entity.Property(e => e.Photo).HasMaxLength(50);
            entity.Property(e => e.RoleId).HasColumnName("Role_id");
            entity.Property(e => e.StartDate).HasColumnName("Start_date");
            entity.Property(e => e.TotalLeaveDays).HasColumnName("Total_leave_days");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("User_name");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Employees__Depar__534D60F1");

            entity.HasOne(d => d.JobPosition).WithMany(p => p.Employees)
                .HasForeignKey(d => d.JobPositionId)
                .HasConstraintName("FK__Employees__Job_p__52593CB8");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Employees__Role___5441852A");
        });

        modelBuilder.Entity<JobPosition>(entity =>
        {
            entity.HasKey(e => e.JobPositionId).HasName("PK__Job_Posi__ADB60CA3EFFB36E3");

            entity.ToTable("Job_Positions");

            entity.Property(e => e.JobPositionId).HasColumnName("Job_position_id");
            entity.Property(e => e.JobPositionName)
                .HasMaxLength(50)
                .HasColumnName("Job_position_name");
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Leave_Re__E9C0AF0BC8CE9961");

            entity.ToTable("Leave_Request");

            entity.Property(e => e.RequestId).HasColumnName("Request_id");
            entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");
            entity.Property(e => e.EndDate).HasColumnName("End_date");
            entity.Property(e => e.LeaveTypeId).HasColumnName("Leave_type_id");
            entity.Property(e => e.RequestStatusId).HasColumnName("Request_status_id");
            entity.Property(e => e.StartDate).HasColumnName("Start_date");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Leave_Req__Emplo__5DCAEF64");

            entity.HasOne(d => d.LeaveType).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.LeaveTypeId)
                .HasConstraintName("FK__Leave_Req__Leave__5EBF139D");

            entity.HasOne(d => d.RequestStatus).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.RequestStatusId)
                .HasConstraintName("FK__Leave_Req__Reque__5FB337D6");
        });

        modelBuilder.Entity<LeaveType>(entity =>
        {
            entity.HasKey(e => e.LeaveTypeId).HasName("PK__Leave_ty__357FFCA87B57F826");

            entity.ToTable("Leave_type");

            entity.Property(e => e.LeaveTypeId).HasColumnName("Leave_type_id");
            entity.Property(e => e.LeaveTypeName)
                .HasMaxLength(50)
                .HasColumnName("Leave_type_name");
        });

        modelBuilder.Entity<RequestStatus>(entity =>
        {
            entity.HasKey(e => e.RequestStatusId).HasName("PK__Request___A61490626951DFE1");

            entity.ToTable("Request_Status");

            entity.Property(e => e.RequestStatusId).HasColumnName("Request_status_id");
            entity.Property(e => e.RequestStatusName)
                .HasMaxLength(50)
                .HasColumnName("Request_status_name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__D80BB093F87A4F76");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("Role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("Role_name");
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.HasKey(e => e.SalaryId).HasName("PK__Salary__D64F722C937EC30C");

            entity.ToTable("Salary");

            entity.Property(e => e.SalaryId).HasColumnName("Salary_id");
            entity.Property(e => e.BasicSalary).HasColumnName("Basic_salary");
            entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");
            entity.Property(e => e.PaymentDate).HasColumnName("Payment_date");
            entity.Property(e => e.TotalIncome).HasColumnName("Total_income");

            entity.HasOne(d => d.Employee).WithMany(p => p.Salaries)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Salary__Employee__6477ECF3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
