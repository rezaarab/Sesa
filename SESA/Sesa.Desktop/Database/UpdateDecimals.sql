﻿-- sqlcecmd -d "data source=D:\REZA\Project\SESA\output\Debug\Database\AppDbFile.sdf; password=computer" -q "Alter Table Product Alter Column ValidInputPercent decimal(18,2)"
-- sqlcecmd -d "data source=D:\REZA\Project\SESA\output\Debug\Database\AppDbFile.sdf; password=computer" -q "Alter Table Product Alter Column AddedValuePercent decimal(18,2)"
-- sqlcecmd -d "data source=D:\REZA\Project\SESA\output\Debug\Database\AppDbFile.sdf; password=computer" -q "Alter Table InternalProductMaterial Alter Column Value decimal(18,2)"
-- sqlcecmd -d "data source=D:\REZA\Project\SESA\output\Debug\Database\AppDbFile.sdf; password=computer" -q "Alter Table InternalProductMaterial Alter Column Pert decimal(18,2)"
-- sqlcecmd -d "data source=D:\REZA\Project\SESA\output\Debug\Database\AppDbFile.sdf; password=computer" -q "Alter Table WarehouseBillDetail Alter Column Value decimal(18,2)"
-- sqlcecmd -d "data source=D:\REZA\Project\SESA\output\Debug\Database\AppDbFile.sdf; password=computer" -q "Alter Table WarehouseBillDetail Alter Column Weight decimal(18,2)"
-- sqlcecmd -d "data source=D:\REZA\Project\SESA\output\Debug\Database\AppDbFile.sdf; password=computer" -q "Alter Table Testimony Alter Column PureWeight decimal(18,2)"
-- sqlcecmd -d "data source=D:\REZA\Project\SESA\output\Debug\Database\AppDbFile.sdf; password=computer" -q "Alter Table Testimony Alter Column RealWeight decimal(18,2)"
-- sqlcecmd -d "data source=D:\REZA\Project\SESA\output\Debug\Database\AppDbFile.sdf; password=computer" -q "Alter Table TestimonyDetail Alter Column Value decimal(18,2)"
-- sqlcecmd -d "data source=D:\REZA\Project\SESA\output\Debug\Database\AppDbFile.sdf; password=computer" -q "Alter Table TestimonyDetail Alter Column Weight decimal(18,2)"
-- sqlcecmd -d "data source=D:\REZA\Project\SESA\output\Debug\Database\AppDbFile.sdf; password=computer" -q "Alter Table ExternalProductMaterial Alter Column Value decimal(18,2)"
-- sqlcecmd -d "data source=D:\REZA\Project\SESA\output\Debug\Database\AppDbFile.sdf; password=computer" -q "Alter Table ExternalProductMaterial Alter Column Pert decimal(18,2)"


Alter Table Product Alter Column ValidInputPercent decimal(18,2);
Alter Table Product Alter Column AddedValuePercent decimal(18,2);
Alter Table InternalProductMaterial Alter Column Value decimal(18,2);
Alter Table InternalProductMaterial Alter Column Pert decimal(18,2);
Alter Table WarehouseBillDetail Alter Column Value decimal(18,2);
Alter Table WarehouseBillDetail Alter Column Weight decimal(18,2);
Alter Table Testimony Alter Column PureWeight decimal(18,2);
Alter Table Testimony Alter Column RealWeight decimal(18,2);
Alter Table TestimonyDetail Alter Column Value decimal(18,2);
Alter Table TestimonyDetail Alter Column Weight decimal(18,2);
Alter Table ExternalProductMaterial Alter Column Value decimal(18,2);
Alter Table ExternalProductMaterial Alter Column Pert decimal(18,2);

