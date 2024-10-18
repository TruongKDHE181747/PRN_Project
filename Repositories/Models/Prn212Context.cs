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

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server =(local); database = PRN212;uid=sa;pwd=123;TrustServerCertificate=True;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__Activity__A145B1FFA28976B3");

            entity.ToTable("Activity_History");

            entity.Property(e => e.HistoryId).HasColumnName("History_id");
            entity.Property(e => e.Action).HasMaxLength(50);
            entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");
            entity.Property(e => e.Target).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithMany(p => p.ActivityHistories)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Activity___Emplo__59FA5E80");
        });

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__57FB453CABE0576B");

            entity.ToTable("Attendance");

            entity.Property(e => e.AttendanceId).HasColumnName("Attendance_id");
            entity.Property(e => e.AttendanceDate).HasColumnName("Attendance_date");
            entity.Property(e => e.AttendanceStatusId).HasColumnName("Attendance_status_id");
            entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");
            entity.Property(e => e.HoursWorked).HasColumnName("Hours_worked");
            entity.Property(e => e.OvertimeHour).HasColumnName("Overtime_hour");
            entity.Property(e => e.TimeIn).HasColumnName("Time_in");
            entity.Property(e => e.TimeOut).HasColumnName("Time_out");

            entity.HasOne(d => d.AttendanceStatus).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.AttendanceStatusId)
                .HasConstraintName("FK__Attendanc__Atten__6B24EA82");

            entity.HasOne(d => d.Employee).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Attendanc__Emplo__6A30C649");
        });

        modelBuilder.Entity<AttendanceStatus>(entity =>
        {
            entity.HasKey(e => e.AttendanceStatusId).HasName("PK__Attendan__BB5C0C6D716AD8F8");

            entity.ToTable("Attendance_Status");

            entity.Property(e => e.AttendanceStatusId).HasColumnName("Attendance_status_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .HasColumnName("Status_name");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__151571C98547B94C");

            entity.Property(e => e.DepartmentId).HasColumnName("Department_id");
            entity.Property(e => e.DepartmentAddress)
                .HasMaxLength(50)
                .HasColumnName("Department_address");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .HasColumnName("Department_name");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__781228D91CEEAFD6");

            entity.HasIndex(e => e.UserName, "UQ__Employee__5F1A108669CAE229").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "UQ__Employee__7E87EC670BA1549B").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Employee__A9D105348423B379").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.AvailableLeaveDays).HasColumnName("Available_leave_days");
            entity.Property(e => e.DepartmentId).HasColumnName("Department_id");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.EndDate).HasColumnName("End_date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("First_name");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
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
            entity.Property(e => e.StatusId).HasColumnName("Status_id");
            entity.Property(e => e.TotalLeaveDays).HasColumnName("Total_leave_days");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("User_name");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Employees__Depar__5535A963");

            entity.HasOne(d => d.JobPosition).WithMany(p => p.Employees)
                .HasForeignKey(d => d.JobPositionId)
                .HasConstraintName("FK__Employees__Job_p__5441852A");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Employees__Role___571DF1D5");

            entity.HasOne(d => d.Status).WithMany(p => p.Employees)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Employees__Statu__5629CD9C");
        });

        modelBuilder.Entity<JobPosition>(entity =>
        {
            entity.HasKey(e => e.JobPositionId).HasName("PK__Job_Posi__ADB60CA388E00F6C");

            entity.ToTable("Job_Positions");

            entity.Property(e => e.JobPositionId).HasColumnName("Job_position_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.JobPositionName)
                .HasMaxLength(50)
                .HasColumnName("Job_position_name");
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Leave_Re__E9C0AF0B470F65B5");

            entity.ToTable("Leave_Request");

            entity.Property(e => e.RequestId).HasColumnName("Request_id");
            entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");
            entity.Property(e => e.EndDate).HasColumnName("End_date");
            entity.Property(e => e.LeaveTypeId).HasColumnName("Leave_type_id");
            entity.Property(e => e.RequestStatusId).HasColumnName("Request_status_id");
            entity.Property(e => e.StartDate).HasColumnName("Start_date");
            entity.Property(e => e.SubmitedOn).HasColumnName("Submited_on");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Leave_Req__Emplo__60A75C0F");

            entity.HasOne(d => d.LeaveType).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.LeaveTypeId)
                .HasConstraintName("FK__Leave_Req__Leave__619B8048");

            entity.HasOne(d => d.RequestStatus).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.RequestStatusId)
                .HasConstraintName("FK__Leave_Req__Reque__628FA481");
        });

        modelBuilder.Entity<LeaveType>(entity =>
        {
            entity.HasKey(e => e.LeaveTypeId).HasName("PK__Leave_ty__357FFCA8C20FD058");

            entity.ToTable("Leave_type");

            entity.Property(e => e.LeaveTypeId).HasColumnName("Leave_type_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LeaveTypeName)
                .HasMaxLength(50)
                .HasColumnName("Leave_type_name");
        });

        modelBuilder.Entity<RequestStatus>(entity =>
        {
            entity.HasKey(e => e.RequestStatusId).HasName("PK__Request___A614906223E3AEF1");

            entity.ToTable("Request_Status");

            entity.Property(e => e.RequestStatusId).HasColumnName("Request_status_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.RequestStatusName)
                .HasMaxLength(50)
                .HasColumnName("Request_status_name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__D80BB093F9B384BA");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("Role_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("Role_name");
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.HasKey(e => e.SalaryId).HasName("PK__Salary__D64F722C0C49F3C9");

            entity.ToTable("Salary");

            entity.Property(e => e.SalaryId).HasColumnName("Salary_id");
            entity.Property(e => e.BasicSalary).HasColumnName("Basic_salary");
            entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");
            entity.Property(e => e.PaymentDate).HasColumnName("Payment_date");
            entity.Property(e => e.TotalIncome).HasColumnName("Total_income");

            entity.HasOne(d => d.Employee).WithMany(p => p.Salaries)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Salary__Employee__6754599E");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Status__5191052449D459FC");

            entity.ToTable("Status");

            entity.Property(e => e.StatusId).HasColumnName("Status_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.StatusDescription)
                .HasMaxLength(50)
                .HasColumnName("Status_description");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
