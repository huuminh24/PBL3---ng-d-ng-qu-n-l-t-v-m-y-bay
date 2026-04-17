using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FlightTicketManager.Models;

public partial class FlightTicketManagerDbContext : DbContext
{
    public FlightTicketManagerDbContext()
    {
    }

    public FlightTicketManagerDbContext(DbContextOptions<FlightTicketManagerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<CancelRequest> CancelRequests { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<FlightPrice> FlightPrices { get; set; }

    public virtual DbSet<Passenger> Passengers { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Seat> Seats { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=LAPTOP-IE8TBPPB\\SQLEXPRESS;Database=FlightTicketManagerDB;Trusted_Connection=True;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__349DA586681CDF1B");

            entity.ToTable("Account");

            entity.HasIndex(e => e.Username, "UQ__Account__536C85E43BDCE949").IsUnique();

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Account__RoleID__3B75D760");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951ACD74C25860");

            entity.ToTable("Booking");

            entity.HasIndex(e => e.BookingCode, "UQ__Booking__C6E56BD5ED8F45F4").IsUnique();

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.BookingChannel)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BookingCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BookingStatus)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ContactFullName).HasMaxLength(100);
            entity.Property(e => e.ContactPhone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByProfileId).HasColumnName("CreatedByProfileID");
            entity.Property(e => e.CustomerProfileId).HasColumnName("CustomerProfileID");
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FinalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.HoldExpiredAt).HasColumnType("datetime");
            entity.Property(e => e.OriginalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PromotionId).HasColumnName("PromotionID");

            entity.HasOne(d => d.CreatedByProfile).WithMany(p => p.BookingCreatedByProfiles)
                .HasForeignKey(d => d.CreatedByProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__Created__4F7CD00D");

            entity.HasOne(d => d.CustomerProfile).WithMany(p => p.BookingCustomerProfiles)
                .HasForeignKey(d => d.CustomerProfileId)
                .HasConstraintName("FK__Booking__Custome__4E88ABD4");

            entity.HasOne(d => d.Promotion).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PromotionId)
                .HasConstraintName("FK__Booking__Promoti__5070F446");
        });

        modelBuilder.Entity<CancelRequest>(entity =>
        {
            entity.HasKey(e => e.CancelRequestId).HasName("PK__CancelRe__150669FE46DD0C83");

            entity.ToTable("CancelRequest");

            entity.HasIndex(e => e.TicketId, "UQ__CancelRe__712CC626168B03FB").IsUnique();

            entity.Property(e => e.CancelRequestId).HasColumnName("CancelRequestID");
            entity.Property(e => e.Reason).HasMaxLength(255);
            entity.Property(e => e.RefundAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RequestDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TicketId).HasColumnName("TicketID");

            entity.HasOne(d => d.Ticket).WithOne(p => p.CancelRequest)
                .HasForeignKey<CancelRequest>(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CancelReq__Ticke__619B8048");
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("PK__Flight__8A9E148EA48980E5");

            entity.ToTable("Flight");

            entity.HasIndex(e => e.FlightCode, "UQ__Flight__75575F51C74962FB").IsUnique();

            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.AirlineCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.AirlineName).HasMaxLength(100);
            entity.Property(e => e.ArrivalCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ArrivalLocation).HasMaxLength(100);
            entity.Property(e => e.ArrivalTime).HasColumnType("datetime");
            entity.Property(e => e.DepartureCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DepartureLocation).HasMaxLength(100);
            entity.Property(e => e.DepartureTime).HasColumnType("datetime");
            entity.Property(e => e.FlightCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FlightPrice>(entity =>
        {
            entity.HasKey(e => e.PriceId).HasName("PK__FlightPr__4957584FCAAA2C91");

            entity.ToTable("FlightPrice");

            entity.Property(e => e.PriceId).HasColumnName("PriceID");
            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SeatClass)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Flight).WithMany(p => p.FlightPrices)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FlightPri__Fligh__44FF419A");
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.PassengerId).HasName("PK__Passenge__88915F9076729D83");

            entity.ToTable("Passenger");

            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.DocumentNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DocumentType)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PassengerType)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Booking).WithMany(p => p.Passengers)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Passenger__Booki__5441852A");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A5821AB6316");

            entity.ToTable("Payment");

            entity.HasIndex(e => e.BookingId, "UQ__Payment__73951ACC56F420FE").IsUnique();

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.PaidAt).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TransactionCode)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Booking).WithOne(p => p.Payment)
                .HasForeignKey<Payment>(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment__Booking__5DCAEF64");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.ProfileId).HasName("PK__Profile__290C88842BC60713");

            entity.ToTable("Profile");

            entity.HasIndex(e => e.AccountId, "UQ__Profile__349DA5873B0B8D5F").IsUnique();

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithOne(p => p.Profile)
                .HasForeignKey<Profile>(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Profile__Account__3F466844");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("PK__Promotio__52C42F2FC02AF103");

            entity.ToTable("Promotion");

            entity.HasIndex(e => e.PromotionCode, "UQ__Promotio__A617E4B62B08CEA1").IsUnique();

            entity.Property(e => e.PromotionId).HasColumnName("PromotionID");
            entity.Property(e => e.DiscountType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DiscountValue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.MinOrderAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PromotionCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3AC3AB4A61");

            entity.ToTable("Role");

            entity.HasIndex(e => e.RoleName, "UQ__Role__8A2B6160662AF60B").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => e.SeatId).HasName("PK__Seat__311713D37FFFBFEE");

            entity.ToTable("Seat");

            entity.Property(e => e.SeatId).HasColumnName("SeatID");
            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.SeatClass)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.SeatNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SeatStatus)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Flight).WithMany(p => p.Seats)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Seat__FlightID__47DBAE45");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Ticket__712CC6272EF283D1");

            entity.ToTable("Ticket");

            entity.Property(e => e.TicketId).HasColumnName("TicketID");
            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");
            entity.Property(e => e.PriceId).HasColumnName("PriceID");
            entity.Property(e => e.SeatId).HasColumnName("SeatID");
            entity.Property(e => e.TicketStatus)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Flight).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ticket__FlightID__5812160E");

            entity.HasOne(d => d.Passenger).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.PassengerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ticket__Passenge__571DF1D5");

            entity.HasOne(d => d.Price).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.PriceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ticket__PriceID__59063A47");

            entity.HasOne(d => d.Seat).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SeatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ticket__SeatID__59FA5E80");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
