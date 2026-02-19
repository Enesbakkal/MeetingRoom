# Meeting Room API
Room & Reservation REST API (.NET 8).

## Çalıştırma
- Proje kökünde: `dotnet run --project MeetingRoom.Api` (veya IDE ile F5).
- Swagger: `https://localhost:7111/swagger` (veya çıktıdaki port).
- Varsayılan veritabanı: **LocalDB**. `appsettings.json` içindeki `ConnectionStrings:DefaultConnection` kullanılır.
- Uygulama ilk açılışta migration'ları uygular; seed data (örnek odalar ve rezervasyonlar) otomatik eklenir.
- Kendi SQL Server kullanmak için: `MeetingRoom.Infrastructure/Extensions/ServiceCollectionExtensions.cs` içindeki yorum satırını açıp kendi connection string'inizi yazın (repoya göndermeyin).

## Yeni migration eklemek
```bash
dotnet ef migrations add <Adi> --project MeetingRoom.Infrastructure --startup-project MeetingRoom.Api
```
