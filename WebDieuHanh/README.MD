
# 1 thang
### 3 thang
* 1 hoa thi
* 1 hoa thi
* 1 hoa thi
## 2 thang
### 3 thang
### 3 thang


Install-Package Microsoft.EntityFrameworkCore -version 5.0.17
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 5.0.17
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 5.0.17
Install-Package Microsoft.EntityFrameworkCore.Design -Version 5.0.17

Install-Package Microsoft.aspnetcore.mvc.razor.runtimecompilation -Version 5.0.17

Scaffold-DbContext "Data Source=.  ;  Initial Catalog=webdieuhanh  ;  User ID= webdieuhanh ;  Password= webdieuhanh  ;"  Microsoft.EntityFrameworkCore.SqlServer -OutputDir Areas\Admin\Data -force


###


  public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllersWithViews();
            services.AddDbContext<VNETCContext>(options => options.UseSqlServer(Configuration.GetConnectionString("strConn")));
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            //==============SESSION ============================================
            services.AddSession();
            //=================COOKIES==========================================
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath  = "/Admin/user/login";  // Home/index gọi đây trước.
                options.LogoutPath = "/Admin/user/logout";
                options.AccessDeniedPath = "/Nofound";
            });
        }




 
###

USE [WEBDIEUHANH]
GO

/****** Object:  Table [dbo].[T_Donvi]    Script Date: 05/02/2024 8:18:43 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_Donvi](
	[MaDvId] [int] IDENTITY(1,1) NOT NULL,
	[MaDv] [varchar](6) NOT NULL,
	[TenDv] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_T_Donvi] PRIMARY KEY CLUSTERED 
(
	[MaDvId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO




CREATE TABLE [dbo].[T_User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[MaDv] [varchar](10) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Password] [varchar](50) NOT NULL,
 CONSTRAINT [PK_T_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



CREATE TABLE [dbo].[T_Laixe](
	[LaixeId] [int] IDENTITY(1,1) NOT NULL,
	[Hoten] [nvarchar](50) NOT NULL,
	[Email] [varchar](50) NULL,
	[Phone1] [varchar](50) NOT NULL,
	[Phone2] [varchar](50) NULL,
	[Phone3] [varchar](50) NULL,
	[DOB] [date] NULL,
	[MaDv] [varchar](50) NOT NULL,
 CONSTRAINT [PK_T_Laixe] PRIMARY KEY CLUSTERED 
(
	[LaixeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[T_Loaicongtrinh](
	[LoaicongtrinhId] [int] IDENTITY(1,1) NOT NULL,
	[Tenloaicongtrinh] [nvarchar](50) NOT NULL,
	[Ghichu] [nvarchar](500) NULL,
 CONSTRAINT [PK_T_Loaicongtrinh] PRIMARY KEY CLUSTERED 
(
	[LoaicongtrinhId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[T_Loaixe](
	[LoaixeId] [int] IDENTITY(1,1) NOT NULL,
	[Tenloaixe] [nvarchar](50) NOT NULL,
	[Ghichu] [nvarchar](500) NULL,
 CONSTRAINT [PK_T_Loaixe] PRIMARY KEY CLUSTERED 
(
	[LoaixeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[T_Xe](
	[XeId] [int] IDENTITY(1,1) NOT NULL,
	[Soxe] [varchar](50) NOT NULL,
	[Loaixe] [nvarchar](50) NOT NULL,
	[NgayDangKiem] [date] NULL,
	[HanBaoHiem] [date] NULL,
	[NgayDaiTuGanNhat] [date] NULL,
	[Tinhtrang] [varchar](50) NOT NULL,
	[MaDv] [nvarchar](50) NOT NULL,
	[LaixeId] [nvarchar](50) NOT NULL,
	[Ghichu] [nvarchar](500) NULL,
 CONSTRAINT [PK_T_Xe] PRIMARY KEY CLUSTERED 
(
	[XeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
