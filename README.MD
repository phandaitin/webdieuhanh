
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

Scaffold-DbContext "Data Source=.;Initial Catalog=VNETC;User ID= vnetc ; Password= vnetc ;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Areas\Admin\Data -force
