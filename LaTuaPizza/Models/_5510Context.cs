using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace LaTuaPizza.Models
{
    public partial class _5510Context : DbContext
    {
        public _5510Context()
        {
        }

        public _5510Context(DbContextOptions<_5510Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<CardDetails> CardDetails { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<LoginCred> LoginCred { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuItem> MenuItem { get; set; }
        public virtual DbSet<OrdStatus> OrdStatus { get; set; }
        public virtual DbSet<OrderInfo> OrderInfo { get; set; }
        public virtual DbSet<SignUp> SignUps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:5510inclass.database.windows.net,1433;Initial Catalog=5510;Persist Security Info=False;User ID=dipesh;Password=gamingmouser8!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.AddrId);

                entity.ToTable("address");

                entity.Property(e => e.AddrId).HasColumnName("addr_id");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasColumnName("postal_code")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Province)
                    .IsRequired()
                    .HasColumnName("province")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasColumnName("street")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Unit).HasColumnName("unit");

                entity.HasOne(d => d.PhoneNavigation)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.Phone)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__address__phone__6477ECF3");
            });

            modelBuilder.Entity<CardDetails>(entity =>
            {
                entity.HasKey(e => e.CardNo);

                entity.ToTable("card_details");

                entity.Property(e => e.CardNo)
                    .HasColumnName("card_no")
                    .ValueGeneratedNever();

                entity.Property(e => e.CardName)
                    .IsRequired()
                    .HasColumnName("card_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CardType)
                    .IsRequired()
                    .HasColumnName("card_type")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Expiry).HasColumnName("expiry");

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.HasOne(d => d.PhoneNavigation)
                    .WithMany(p => p.CardDetails)
                    .HasForeignKey(d => d.Phone)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__card_deta__phone__6754599E");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Phone)
                    .HasName("PK__customer__B43B145ED136CD91");

                entity.ToTable("customer");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasColumnName("fname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lname)
                    .HasColumnName("lname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmailNavigation)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.Email)
                    .HasConstraintName("FK__customer__email__619B8048");
            });

            modelBuilder.Entity<LoginCred>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK__login_cr__AB6E61657B330417");

                entity.ToTable("login_cred");

                entity.HasIndex(e => e.Email)
                    .HasDatabaseName("uc_Email")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("menu");

                entity.Property(e => e.MenuId).HasColumnName("menu_id");

                entity.Property(e => e.MenuDesc)
                    .HasColumnName("menu_desc")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MenuName)
                    .IsRequired()
                    .HasColumnName("menu_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(5, 2)");
            });

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.HasKey(e => new { e.OrdId, e.MenuId })
                    .HasName("PK__menu_ite__08F3D872ABC7B669");

                entity.ToTable("menu_item");

                entity.Property(e => e.OrdId).HasColumnName("ord_id");

                entity.Property(e => e.MenuId).HasColumnName("menu_id");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MenuItem)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__menu_item__menu___1EA48E88");

                entity.HasOne(d => d.Ord)
                    .WithMany(p => p.MenuItem)
                    .HasForeignKey(d => d.OrdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__menu_item__ord_i__1DB06A4F");
            });

            modelBuilder.Entity<OrdStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__ord_stat__3683B531874FF784");

                entity.ToTable("ord_status");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasColumnName("status_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderInfo>(entity =>
            {
                entity.HasKey(e => e.OrdId)
                    .HasName("PK__order_in__DC39D7DF337D9B89");

                entity.ToTable("order_info");

                entity.Property(e => e.OrdId).HasColumnName("ord_id");

                entity.Property(e => e.AddrId).HasColumnName("addr_id");

                entity.Property(e => e.CardNo).HasColumnName("card_no");

                entity.Property(e => e.CnfNo)
                    .IsRequired()
                    .HasColumnName("cnf_no")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.Property(e => e.PriceAfterTax)
                    .IsRequired()
                    .HasColumnName("price_after_tax")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PriceBeforeTax)
                    .IsRequired()
                    .HasColumnName("price_before_tax")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.HasOne(d => d.Addr)
                    .WithMany(p => p.OrderInfo)
                    .HasForeignKey(d => d.AddrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__order_inf__addr___18EBB532");

                entity.HasOne(d => d.CardNoNavigation)
                    .WithMany(p => p.OrderInfo)
                    .HasForeignKey(d => d.CardNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__order_inf__card___19DFD96B");

                entity.HasOne(d => d.PhoneNavigation)
                    .WithMany(p => p.OrderInfo)
                    .HasForeignKey(d => d.Phone)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__order_inf__phone__17F790F9");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.OrderInfo)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__order_inf__statu__1AD3FDA4");
            });

            modelBuilder.Entity<SignUp>(entity =>
            {
                entity.ToTable("sign_up");

                entity.Property(e => e.SignupId)
                    .ValueGeneratedNever()
                    .HasColumnName("Signup_ID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.ConfirmPass)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Confirm_Pass");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("First_Name")
                    .IsFixedLength(true);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Last_Name")
                    .IsFixedLength(true);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber).HasColumnName("Phone_Number");

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Postal_Code")
                    .IsFixedLength(true);

                entity.Property(e => e.Province)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
