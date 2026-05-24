# SEP-G4 CarpentryWorkshop

SEP-G4 CarpentryWorkshop là hệ thống quản lý nhân sự, chấm công, lịch làm việc và lương cho xưởng mộc. Repo gồm một backend ASP.NET Core Web API, một frontend React, và các file database backup dạng `.bacpac`.

Đây là dự án đồ án của nhóm SEP-G4 tại Trường Đại học FPT. Hệ thống được xây dựng dựa trên nghiệp vụ thực tế của Công ty Phú Cầu, một doanh nghiệp tại Bố Hạ, Bắc Giang, có quy mô khoảng 60-80 nhân sự.

## Video

- Video demo hệ thống: <https://www.youtube.com/watch?v=Gxh99iw9Os8>
- Video hướng dẫn chạy local: <https://www.youtube.com/watch?v=gu83N4sbjLE>

## Thiết kế

- Figma: <https://www.figma.com/design/JDdGw4Gs0hPo2jwOi0rPYI/SEP-G4?node-id=0-1&p=f>

## Thành viên nhóm

| Thành viên | Vai trò |
| --- | --- |
| Nghiêm Đức Độ | Team Leader, BA, Main Full-stack Developer, UI/UX Designer |
| Hàn Bình Dương | Frontend Developer, Tester |
| Nguyễn Xuân Hùng | Backend Developer |
| Trần Hoàng | Full-stack Developer |
| Nguyễn Đăng Quân | Business Analyst, Tester |

## Cấu trúc dự án

```text
.
+-- Code/
|   +-- CarpentryWorkshopAPI/      # Backend ASP.NET Core 6 Web API
|   +-- CarpentryWorkshopClient/   # Frontend React
+-- Database/
|   +-- SEP-G4-CCMS.bacpac
|   +-- SEP-G4-CWMS.bacpac
+-- .github/
+-- .gitattributes
+-- .gitignore
+-- README.md
```

## Công nghệ chính

### Backend

- ASP.NET Core 6 Web API.
- Entity Framework Core 6 với SQL Server.
- JWT Bearer Authentication.
- BCrypt để kiểm tra/hash mật khẩu.
- AutoMapper để map Model và DTO.
- Swagger/OpenAPI để kiểm thử API.
- ClosedXML để xuất Excel lương.
- MailKit để gửi email quên mật khẩu/đổi tài khoản.

Backend nằm tại:

```text
Code/CarpentryWorkshopAPI/CarpentryWorkshopAPI
```

Solution:

```text
Code/CarpentryWorkshopAPI/CarpentryWorkshopAPI.sln
```

### Frontend

- React 18.
- React Router DOM 6.
- Axios.
- Ant Design, Material UI, React Bootstrap.
- Sass/SCSS.
- React Toastify.
- ExcelJS, xlsx, file-saver cho xử lý/xuất file Excel.
- Firebase SDK có cấu hình riêng trong `src/fireBase/FireBase.js`.

Frontend nằm tại:

```text
Code/CarpentryWorkshopClient
```

## Chức năng chính

Hệ thống hiện có các nhóm chức năng sau:

- Đăng nhập, đăng xuất, quên mật khẩu, đổi tài khoản/mật khẩu.
- Phân quyền theo page/role thông qua JWT claim và danh sách `userPages`.
- Dashboard và trang chủ.
- Quản lý nhân viên.
- Quản lý phòng ban.
- Quản lý người phụ thuộc.
- Quản lý chức vụ/role.
- Quản lý nhóm/ca/đội làm việc.
- Phân quyền truy cập trang.
- Chấm công.
- Lịch làm việc và phân ca.
- Xem công việc.
- Quản lý ngày nghỉ/ngày lễ.
- Ứng lương.
- Tính lương, phụ cấp, khấu trừ, thưởng cá nhân, thưởng công ty và xuất Excel lương.

## Luồng đăng nhập và phân quyền

Frontend có route công khai:

- `/login`
- `/forget`

Các route còn lại đi qua `ProtectedRoute` trong:

```text
Code/CarpentryWorkshopClient/src/index.js
```

Sau khi đăng nhập thành công, frontend lưu các thông tin sau vào `localStorage` hoặc `sessionStorage`:

- `userToken`
- `userPages`
- `department`
- thông tin nhân viên/tài khoản khác do màn hình login xử lý

Danh sách page trong `userPages` quyết định menu và route nào được hiển thị trong:

```text
Code/CarpentryWorkshopClient/src/App.js
Code/CarpentryWorkshopClient/src/view/component/MenuComponent.js
```

Backend tạo JWT tại:

```text
Code/CarpentryWorkshopAPI/CarpentryWorkshopAPI/Controllers/AccountsController.cs
```

Endpoint login chính:

```text
POST /CWMSapi/Accounts/GetToken/gettoken
```

Token chứa các role claim tương ứng với page mà nhân viên được phép truy cập. Nhiều controller/action backend dùng `[Authorize(Roles = "...")]` để khóa quyền theo page.

## Route frontend chính

Các route nghiệp vụ được khai báo trong `src/App.js`:

| Route | Màn hình |
| --- | --- |
| `/` | Home |
| `/dashboard` | Dashboard |
| `/list-employee` | Danh sách nhân viên |
| `/list-department` | Danh sách phòng ban |
| `/dependent-person` | Người phụ thuộc |
| `/list-group` | Nhóm/đội làm việc |
| `/role` | Chức vụ/role |
| `/decentralization` | Phân quyền |
| `/timekeeping` | Chấm công |
| `/calendar` | Lịch làm việc |
| `/payroll` | Lương |
| `/seeWork` | Xem công việc |
| `/holiday` | Ngày nghỉ/ngày lễ |
| `/advance` | Ứng lương |

## API backend

Đa số controller dùng route pattern:

```text
CWMSapi/[controller]/[action]
```

Riêng `HourWorkDayController` dùng:

```text
api/[controller]
```

Các controller chính:

- `AccountsController`: đăng nhập, tạo tài khoản, quên mật khẩu, đổi tài khoản/mật khẩu.
- `EmployeeController`: danh sách, tìm kiếm, chi tiết, tạo/cập nhật nhân viên.
- `DepartmentsController`: phòng ban và nhân viên trong phòng ban.
- `DependentsController`: người phụ thuộc.
- `RoleController`: chức vụ, nhân viên theo role, role theo phòng ban.
- `PagesController`, `AccessCotroller`: page và phân quyền role-page.
- `TeamController`, `TeamWorksController`: nhóm/đội, trưởng nhóm, thành viên, công việc theo ca.
- `CheckInOutController`: chấm công, check-in/check-out, dữ liệu công theo ngày.
- `WorksController`, `WorkAreasController`, `WorkScheduleController`: công việc, khu vực làm việc, lịch làm việc.
- `ShiftTypeController`: loại ca làm việc.
- `HolidayController`: ngày nghỉ/ngày lễ.
- `AdvanceSalaryController`: ứng lương.
- `SalaryController`, `RewardController`, `ExcelSalaryController`: tính lương, thưởng và xuất Excel.
- Các controller lịch sử trạng thái: `ContractStatusHistoriesController`, `DepartmentsStatusHistoriesController`, `EmployeeStatusHistoryController`, `RoleStatusHistoryController`, `UnitCostStatusHistoriesController`, `WorkScheduleStatusHistoryController`, ...

Swagger được bật trong `Program.cs`. Khi chạy local backend, Swagger mở tại:

```text
https://localhost:7003/swagger
http://localhost:5013/swagger
```

## Database

Database chính dùng SQL Server và được scaffold thành EF Core context:

```text
Code/CarpentryWorkshopAPI/CarpentryWorkshopAPI/Models/SEPG4CWMSContext.cs
```

Context có nhiều `DbSet` cho các bảng nghiệp vụ như:

- `Employees`, `Departments`, `Roles`, `Pages`, `UserAccounts`.
- `Contracts`, `ContractTypes`, lịch sử trạng thái hợp đồng.
- `Teams`, `EmployeeTeams`, `TeamWorks`.
- `CheckInOuts`, `WorkSchedules`, `ShiftTypes`, `Works`, `WorkAreas`.
- `Holidays`, `HolidaysDetails`.
- `Salaries`, `AdvancesSalaries`, `Allowance`, `Deductions`, `BonusDetails`.

File backup database nằm trong:

```text
Database/SEP-G4-CWMS.bacpac
Database/SEP-G4-CCMS.bacpac
```

## Cấu hình quan trọng

Backend đọc connection string và JWT config từ:

```text
Code/CarpentryWorkshopAPI/CarpentryWorkshopAPI/appsettings.json
Code/CarpentryWorkshopAPI/CarpentryWorkshopAPI/appsettings.Development.json
```

Frontend đang cấu hình Axios base URL tại:

```text
Code/CarpentryWorkshopClient/src/sevices/customize-axios.js
```

Giá trị hiện tại trỏ tới API Azure:

```text
https://sep-g4-api.azurewebsites.net
```

Nếu chạy backend local, cần đổi `baseURL` sang một URL local như:

```text
https://localhost:7003
```

hoặc:

```text
http://localhost:5013
```

Lưu ý: repo hiện có cấu hình nhạy cảm trong source như connection string, JWT secret và thông tin SMTP. Khi triển khai thật, nên chuyển các giá trị này sang user secrets, biến môi trường hoặc secret store của môi trường deploy.

## Cách chạy backend local

Yêu cầu:

- .NET SDK 6.x.
- SQL Server hoặc Azure SQL tương thích với database trong cấu hình.

Chạy backend:

```powershell
cd Code\CarpentryWorkshopAPI
dotnet restore .\CarpentryWorkshopAPI.sln
dotnet run --project .\CarpentryWorkshopAPI\CarpentryWorkshopAPI.csproj
```

Build kiểm tra:

```powershell
cd Code\CarpentryWorkshopAPI
dotnet build .\CarpentryWorkshopAPI.sln
```

Backend local mặc định theo `launchSettings.json`:

```text
https://localhost:7003
http://localhost:5013
```

## Cách chạy frontend local

Yêu cầu:

- Node.js.
- npm.

Cài package và chạy:

```powershell
cd Code\CarpentryWorkshopClient
npm install
npm start
```

Build production:

```powershell
cd Code\CarpentryWorkshopClient
npm run build
```

Frontend dùng Create React App, mặc định chạy tại:

```text
http://localhost:3000
```

## Ghi chú triển khai

- Frontend có `public/web.config`, phù hợp khi deploy lên IIS/Azure App Service để hỗ trợ client-side routing.
- Backend bật CORS `AllowAnyOrigin`, `AllowAnyHeader`, `AllowAnyMethod`.
- Backend bật Swagger cả khi chạy app.
- Session backend được cấu hình `IdleTimeout = 10` giây, nhưng luồng chính vẫn dựa vào JWT.
- Frontend gọi API qua folder `src/sevices` (tên thư mục hiện tại là `sevices`, không phải `services`).
- Một số service frontend có URL hardcoded hoặc base URL riêng, ví dụ phần export lương có axios instance trỏ `http://localhost:3001`; cần kiểm tra lại khi deploy hoặc chạy local.

## Các file nên đọc khi phát triển tiếp

- Backend startup/DI/auth: `Code/CarpentryWorkshopAPI/CarpentryWorkshopAPI/Program.cs`
- EF Core context: `Code/CarpentryWorkshopAPI/CarpentryWorkshopAPI/Models/SEPG4CWMSContext.cs`
- DTO mapping: `Code/CarpentryWorkshopAPI/CarpentryWorkshopAPI/Mapper/MapperProfile.cs`
- API controllers: `Code/CarpentryWorkshopAPI/CarpentryWorkshopAPI/Controllers`
- Backend services: `Code/CarpentryWorkshopAPI/CarpentryWorkshopAPI/Services`
- Frontend routes: `Code/CarpentryWorkshopClient/src/index.js` và `Code/CarpentryWorkshopClient/src/App.js`
- Frontend API client: `Code/CarpentryWorkshopClient/src/sevices/customize-axios.js`
- Frontend screen components: `Code/CarpentryWorkshopClient/src/view/component`
- Frontend styles: `Code/CarpentryWorkshopClient/src/view/scss`
