1. Create Project + Install
summary: สร้างโครงสร้างโปรเจกต์
- Install VS Code, SQL Server, SSMS
- Create new project command: dotnet new webapi -o API
- Run command: dotnet watch run

2. Models (Database Table)
summary: ออกแบบ "พิมพ์เขียว" ของข้อมูลที่ต้องการจะเก็บ (เช่น ข้อมูลหุ้น, ข้อมูลความคิดเห็น) ซึ่ง "โมเดล" (Model) นี้จะเป็นตัวกำหนดว่าข้อมูลแต่ละอย่างมีหน้าตาและโครงสร้างอย่างไร เปรียบเสมือนการออกแบบตารางในฐานข้อมูลผ่านโค้ด
- Create Models folder
- Create Stock.cs, Comment.cs 
    - Command: prop

3. Entity Framework (ORM)
summary: เป็นขั้นตอนการนำ "โมเดล" ที่ออกแบบไว้ไปสร้างเป็นตารางในฐานข้อมูลจริง ๆ โดยใช้เครื่องมือที่ชื่อว่า Entity Framework (EF) เป็น "สะพาน" เชื่อมระหว่างโค้ดกับฐานข้อมูล (SQL Server) เพื่อให้แอปพลิเคชันสามารถอ่านและเขียนข้อมูลลงฐานข้อมูลได้ตามโครงสร้างที่วางไว้
- Install Microsoft.EntityFrameworkCore.Design, 
Microsoft.EntityFrameworkCore.SqlServer, 
Microsoft.EntityFrameworkCore.Tools from NuGet Gallery
- Create Data folder
- Create ApplicationDBContext.cs
- appsettings.json: "ConnectionStrings": "DefaultConnection"
- Program.cs: builder.Services.AddDbContext<ApplicationDBContext>
    - Command: dotnet ef migrations add init, dotnet ef database update

4. Controllers
summary: เป็นการสร้าง "ประตูทางเข้า" หรือช่องทางสื่อสารของ API คอนโทรลเลอร์ทำหน้าที่รับคำสั่ง (Request) จากผู้ใช้งาน เช่น "ขอดูข้อมูลหุ้นทั้งหมด" หรือ "สร้างความคิดเห็นใหม่" จากนั้นจะไปดึงข้อมูลหรือสั่งบันทึกข้อมูลลงฐานข้อมูล แล้วส่งผลลัพธ์กลับไปให้ผู้ใช้งาน
- Create Controllers folder
- Create StockController.cs
- Program.cs: builder.Services.AddControllers();, app.MapControllers();

5. Dtos
summary: เป็นการ "จัดระเบียบ" ข้อมูลก่อนที่จะส่งออกไปให้ผู้ใช้ โดย DTOs (Data Transfer Objects) ทำหน้าที่เป็นโมเดลสำหรับส่งข้อมูลโดยเฉพาะ เพื่อที่เราจะสามารถเลือกได้ว่าจะส่งข้อมูลอะไรออกไปบ้าง และช่วยซ่อนโครงสร้างฐานข้อมูลจริงไว้ภายใน ส่วน Mappers คือตัวกลางที่ช่วยแปลงข้อมูลระหว่าง "โมเดลฐานข้อมูล" กับ "DTO" ให้โดยอัตโนมัติ ทำให้โค้ดสะอาดและปลอดภัยยิ่งขึ้น
- Create Dtos folder, Stock folder, Comment folder
- Create StockDto.cs
- Create Mappers folder
- Create StockMappers.cs
- Use mappers in controllers