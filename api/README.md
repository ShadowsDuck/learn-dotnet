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

6. POST (Create)
summary: รับข้อมูลจากผู้ใช้ผ่าน API, แปลงข้อมูลเป็น entity ของฐานข้อมูลด้วย DTO + Mapper, แล้วบันทึกลงฐานข้อมูล → ส่งผลลัพธ์กลับเป็นข้อมูลที่ถูกสร้างใหม่ให้ผู้ใช้ 
- Create CreateStockRequestDto.cs
- Create ToStockFromCreateDto() method in StockMappers.cs
- HttpPost in StockController.cs
- Use mappers in HttpPost controllers

7. PUT (Update)
summary: รับข้อมูลการแก้ไขจากผู้ใช้ผ่าน API, หา entity ที่มีอยู่ในฐานข้อมูล, อัปเดตค่า property ที่เปลี่ยน, บันทึกการเปลี่ยนแปลง → ส่งผลลัพธ์กลับเป็นข้อมูลที่อัปเดตแล้ว
- Create UpdateStockRequestDto.cs
- HttpPut in StockController.cs

8. DELETE
summary: รับคำสั่งลบจากผู้ใช้ผ่าน API, หา entity ที่ต้องการลบในฐานข้อมูล, ลบ entity นั้นออกจากฐานข้อมูล → ส่งผลลัพธ์ยืนยันการลบกลับผู้ใช้
- HttpDelete in StockController.cs

9. Async/Await
summary: เป็นการเขียนโค้ดให้ controller ทำงานแบบ asynchronous เพื่อไม่ให้บล็อกการทำงานระหว่างรอฐานข้อมูลหรือ I/O ทำให้ API รองรับผู้ใช้พร้อมกันได้มากขึ้นและทำงานได้มีประสิทธิภาพกว่าแบบ synchronous
- Using async/await in controllers

10. Repository Pattern + DI
summary: เป็นการสร้างเลเยอร์กลางระหว่าง Controller กับ Database โดยใช้ Repository และ Interface เพื่อแยกความรับผิดชอบ
- Create Interfaces folder
- Create IStockRepository.cs
- Create Repository folder
- Create StockRepository.cs
- Using IStockRepository in StockController.cs
- Add Scoped service in Program.cs to use IStockRepository, StockRepository